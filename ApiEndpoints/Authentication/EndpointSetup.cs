namespace AnonKey_Backend.ApiEndpoints.Authentication;

/// <summary>
/// Maps the endpoint paths to the appropriate methods for the Authentication endpoint  
/// </summary>
public static class EndpointSetup 
{
    /// <summary>
    /// Maps the endpoint paths to the appropriate methods for the Authentication endpoint  
    /// </summary>
    /// <param name="app">The web app instance to initialize with the mapping</param>
    public static void MapEndpoints(WebApplication app)
    {
        app.MapPost("/authentication/login", Login.PostLogin).WithTags("Authentication").WithOpenApi();
        app.MapPut("/authentication/changePassword", ChangePassword.PutChangePassword).WithTags("Authentication").WithOpenApi().RequireAuthorization("user");
    }
}
