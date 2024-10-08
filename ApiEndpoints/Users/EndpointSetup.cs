namespace AnonKey_Backend.ApiEndpoints.Users;

/// <summary>
/// Handles mapping the users endpoint.
/// </summary>
public static class EndpointSetup
{
    /// <summary>
    /// Maps the endpoint paths to the appropriate methods for the Users endpoint  
    /// </summary>
    /// <param name="app">The web app instance to initialize with the mapping</param>
    public static void MapEndpoints(WebApplication app)
    {
        app.MapPost("/user/create", Create.PostCreate).WithTags("Users").WithOpenApi();
        app.MapGet("/user/get", Get.GetGet).WithTags("Users").WithOpenApi().RequireAuthorization("user");
        app.MapPut("/user/update", Update.PutUpdate).WithTags("Users").WithOpenApi().RequireAuthorization("user");
        app.MapDelete("/user/delete", Delete.DeleteDelete).WithTags("Users").WithOpenApi().RequireAuthorization("user");
    }
}
