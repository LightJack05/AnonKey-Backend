namespace AnonKey_Backend.ApiEndpoints.Service;

/// <summary>
/// Handles mapping the service endpoints.
/// </summary>
public static class EndpointSetup
{
    /// <summary>
    /// Maps the endpoint paths to the appropriate methods for the Service endpoint  
    /// </summary>
    /// <param name="app">The web app instance to initialize with the mapping</param>
    public static void MapEndpoints(WebApplication app)
    {
        app.MapGet("/service/ping", Ping.GetPing).WithTags("Service").WithOpenApi().RequireAuthorization("user");
    }

}
