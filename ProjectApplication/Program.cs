using ProjectApplication.Helpers;
using ProjectApplication.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
{
    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

    builder.Services.AddScoped<IHomeService, HomeService>();
    builder.Services.AddScoped<INotesService, NotesService>();
}

var configurationBuilder = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json", optional: false)
        .Build();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
