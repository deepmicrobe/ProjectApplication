using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ProjectApplication.Helpers;
using ProjectApplication.Models;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProjectApplication.Services
{
    public interface IHomeService
    {
        LoginResponse Login(LoginRequest model);
    }

    public class HomeService : IHomeService
    {
        private readonly AppSettings _appSettings;
        private string _connectionString;

        public HomeService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _connectionString = ConnectionHelper.GetConnectionString(_appSettings);
        }

        public LoginResponse Login(LoginRequest model)
        {
            var user = findUser(model.Username, model.Password);

            if (user == null)
            {
                throw new Exception("username or password is incorrect");
            }

            var token = createJwtToken(user);

            return new LoginResponse(token);
        }

        private User? findUser(string username, string password)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var path = Path.Combine(Environment.CurrentDirectory, @"Data\GetUser.sql");

                var getUserQuery = File.ReadAllText(path);

                using (var command = new SqlCommand(getUserQuery, connection))
                {
                    command.Parameters.Add("@username", System.Data.SqlDbType.VarChar);
                    command.Parameters["@username"].Value = username;

                    command.Parameters.Add("@password", System.Data.SqlDbType.VarChar);
                    command.Parameters["@password"].Value = password;

                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return new User
                            {
                                UserId = reader.GetInt32(0),
                            };
                        }
                    }

                    //var count = (int)command.ExecuteScalar();

                    //if (count == 1)
                    //{
                    //    return true;
                    //}
                    //else
                    //{
                    //    return false;
                    //}
                }
            }

            return null;
        }

        private string createJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            { 
                // Help generate payload sub claim
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.UserId.ToString()) }),
                // Help generate payload exp claim
                Expires = DateTime.UtcNow.AddDays(7),
                // Signature made up of encoded header, encoded payload, secret, and algorithm in header
                // Help generate signature (uses key and algorithm)
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
