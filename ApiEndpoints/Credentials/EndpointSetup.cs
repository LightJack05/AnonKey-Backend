namespace AnonKey_Backend.ApiEndpoints.Credentials;

public static class EndpointSetup 
{
    /// <summary>
    /// Maps the endpoint paths to the appropriate methods for the Credentials endpoint  
    /// </summary>
    /// <param name="app">The web app instance to initialize with the mapping</param>
    public static void MapEndpoints(WebApplication app)
    {
        app.MapPost("/credentials/create", Create.PostCreate).WithTags("Credentials").WithOpenApi();
        app.MapGet("/credentials/get", Get.GetGet).WithTags("Credentials").WithOpenApi();
        app.MapGet("/credentials/getAll", GetAll.GetGetAll).WithTags("Credentials").WithOpenApi();
        app.MapPut("/credentials/update", Update.PutUpdate).WithTags("Credentials").WithOpenApi();
        app.MapDelete("/credentials/delete", Delete.DeleteDelete).WithTags("Credentials").WithOpenApi();
    }
}
