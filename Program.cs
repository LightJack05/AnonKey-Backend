using System.IO;
using System.Reflection;


namespace AnonKey_Backend;

#pragma warning disable

public class Program
{
    public static void Main(String[] args)
    {
        //NO-PROD: Output the signing key for debugging purposes.
        byte[] key = Configuration.Settings.JwtIssuerSigningKey;
        System.Console.WriteLine(Convert.ToBase64String(key));
        Configuration.Settings.SaveSettings();

        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        // Initialize the endpoints with the proper mappings
        AnonKey_Backend.ApiEndpoints.EndpointSetup.Initialize(app);

        app.Run();

    }
}
