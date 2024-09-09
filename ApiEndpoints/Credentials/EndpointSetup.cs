namespace AnonKey_Backend.ApiEndpoints.Credentials;

public static class EndpointSetup 
{
    /// <summary>
    /// Maps the endpoint paths to the appropriate methods for the Credentials endpoint  
    /// </summary>
    /// <param name="app">The web app instance to initialize with the mapping</param>
    public static void MapEndpoints(WebApplication app)
    {
        app.MapPost("/credentials/create", Create.PostCreate);
        app.MapGet("/credentials/get", Get.GetGet);
        app.MapGet("/credentials/getAll", GetAll.GetAllGet);
        app.MapPut("/credentials/update", Update.PutUpdate);
        app.MapDelete("/credentials/delete", Delete.DeleteDelete);
    }
}
