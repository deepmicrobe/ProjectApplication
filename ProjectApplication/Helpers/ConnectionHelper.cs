using System.Data.SqlClient;

namespace ProjectApplication.Helpers
{
    public static class ConnectionHelper
    {
        public static string GetConnectionString(AppSettings appSettings)
        {
            var builder = new SqlConnectionStringBuilder
            {
                DataSource = appSettings.DataSource,
                UserID = appSettings.UserID,
                Password = appSettings.Password,
                InitialCatalog = appSettings.InitialCatalog
            };

            return builder.ConnectionString;
        }
    }
}
