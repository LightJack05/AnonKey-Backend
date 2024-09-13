namespace AnonKey_Backend.ApiEndpoints.Folders;

public static class EndpointSetup 
{
    /// <summary>
    /// Maps the endpoint paths to the appropriate methods for the Folders endpoint  
    /// </summary>
    /// <param name="app">The web app instance to initialize with the mapping</param>
    public static void MapEndpoints(WebApplication app)
    {
        app.MapPost("/folders/create", Create.PostCreate).WithTags("Folders").WithOpenApi().RequireAuthorization("user");
        app.MapGet("/folders/get", Get.GetGet).WithTags("Folders").WithOpenApi().RequireAuthorization("user");
        app.MapGet("/folders/getAll", GetAll.GetGetAll).WithTags("Folders").WithOpenApi().RequireAuthorization("user");
        app.MapPut("/folders/update", Update.PutUpdate).WithTags("Folders").WithOpenApi().RequireAuthorization("user");
        app.MapDelete("/folders/delete", Delete.DeleteDelete).WithTags("Folders").WithOpenApi().RequireAuthorization("user");
    }
}
