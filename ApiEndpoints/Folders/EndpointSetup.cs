namespace AnonKey_Backend.ApiEndpoints.Folders;

public static class EndpointSetup 
{
    /// <summary>
    /// Maps the endpoint paths to the appropriate methods for the Folders endpoint  
    /// </summary>
    /// <param name="app">The web app instance to initialize with the mapping</param>
    public static void MapEndpoints(WebApplication app)
    {
        app.MapPost("/folders/create", null);
        app.MapGet("/folders/get", null);
        app.MapGet("/folders/getAll", null);
        app.MapPut("/folders/update", null);
        app.MapDelete("/folders/delete", null);
    }
}
