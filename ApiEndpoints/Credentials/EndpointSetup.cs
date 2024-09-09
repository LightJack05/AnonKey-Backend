namespace AnonKey_Backend.ApiEndpoints.Credentials;

public static class EndpointSetup 
{
    public static void MapEndpoints(WebApplication app)
    {
        app.MapPost("/credentials/create", Create.PostCreate);
        app.MapGet("/credentials/get", Get.GetGet);
        app.MapGet("/credentials/getAll", GetAll.GetAllGet);
        app.MapPut("/credentials/update", Update.PutUpdate);
        app.MapDelete("/credentials/delete", Delete.DeleteDelete);
    }
}
