namespace AnonKey_Backend.ApiEndpoints.Authentication;

public static class EndpointSetup 
{
    /// <summary>
    /// Maps the endpoint paths to the appropriate methods for the Authentication endpoint  
    /// </summary>
    /// <param name="app">The web app instance to initialize with the mapping</param>
    public static void MapEndpoints(WebApplication app)
    {
        app.MapPost("/authentication/login", null);
        app.MapDelete("/authentication/logout", null);
        app.MapPut("/authentication/changePassword", null);
    }
}
