namespace AnonKey_Backend.ApiEndpoints.Credentials;

public static class EndpointSetup 
{
    /// <summary>
    /// Maps the endpoint paths to the appropriate methods for the Credentials endpoint  
    /// </summary>
    /// <param name="app">The web app instance to initialize with the mapping</param>
    public static void MapEndpoints(WebApplication app)
    {
        app.MapPost("/credentials/create", null);
        app.MapGet("/credentials/get", null);
        app.MapGet("/credentials/getAll", null);
        app.MapPut("/credentials/update", null);
        app.MapDelete("/credentials/delete", null);
    }
}
