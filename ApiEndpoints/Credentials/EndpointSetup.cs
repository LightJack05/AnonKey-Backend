namespace AnonKey_Backend.ApiEndpoints.Credentials;

/// <summary>
/// Handles mapping the credentials endpoints.
/// </summary>
public static class EndpointSetup
{
    /// <summary>
    /// Maps the endpoint paths to the appropriate methods for the Credentials endpoint  
    /// </summary>
    /// <param name="app">The web app instance to initialize with the mapping</param>
    public static void MapEndpoints(WebApplication app)
    {
        app.MapPost("/credentials/create", Create.PostCreate).WithTags("Credentials").WithOpenApi().RequireAuthorization("user");
        app.MapGet("/credentials/get", Get.GetGet).WithTags("Credentials").WithOpenApi().RequireAuthorization("user");
        app.MapGet("/credentials/getAll", GetAll.GetGetAll).WithTags("Credentials").WithOpenApi().RequireAuthorization("user");
        app.MapPut("/credentials/update", Update.PutUpdate).WithTags("Credentials").WithOpenApi().RequireAuthorization("user");
        app.MapPut("/credentials/soft-delete", SoftDelete.DeleteSoftDelete).WithTags("Credentials").WithOpenApi().RequireAuthorization("user");
        app.MapPut("/credentials/soft-undelete", SoftUndelete.DeleteSoftUndelete).WithTags("Credentials").WithOpenApi().RequireAuthorization("user");
        app.MapDelete("/credentials/delete", Delete.DeleteDelete).WithTags("Credentials").WithOpenApi().RequireAuthorization("user");
    }
}
