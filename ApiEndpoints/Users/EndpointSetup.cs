namespace AnonKey_Backend.ApiEndpoints.Users;

public static class EndpointSetup 
{
    /// <summary>
    /// Maps the endpoint paths to the appropriate methods for the Users endpoint  
    /// </summary>
    /// <param name="app">The web app instance to initialize with the mapping</param>
    public static void MapEndpoints(WebApplication app)
    {
        app.MapPost("/user/create", Create.PostCreate);
        app.MapGet("/user/get", Get.GetGet);
        app.MapPut("/user/update", Update.PutUpdate);
        app.MapDelete("/user/delete", Delete.DeleteDelete);
    }
}
