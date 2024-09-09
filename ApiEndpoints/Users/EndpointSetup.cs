namespace AnonKey_Backend.ApiEndpoints.Users;

public static class EndpointSetup 
{
    /// <summary>
    /// Maps the endpoint paths to the appropriate methods for the Users endpoint  
    /// </summary>
    /// <param name="app">The web app instance to initialize with the mapping</param>
    public static void MapEndpoints(WebApplication app)
    {
        app.MapPost("/user/create", null);
        app.MapGet("/user/get", null);
        app.MapPut("/user/update", null);
        app.MapDelete("/user/delete", null);
    }
}
