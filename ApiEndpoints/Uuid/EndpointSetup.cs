namespace AnonKey_Backend.ApiEndpoints.Uuid;

/// <summary>
/// Handles mapping the uuid endpoint.
/// </summary>
public static class EndpointSetup 
{
    /// <summary>
    /// Maps the endpoint paths to the appropriate methods for the UUID endpoint  
    /// </summary>
    /// <param name="app">The web app instance to initialize with the mapping</param>
    public static void MapEndpoints(WebApplication app)
    {
        app.MapGet("/uuid/new", NewUuid.GetNewUuid).WithTags("UUID").RequireAuthorization("user");
    }
}
