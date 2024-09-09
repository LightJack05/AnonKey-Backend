namespace AnonKey_Backend.ApiEndpoints.Users;

public static class EndpointSetup 
{
    public static void MapEndpoints(WebApplication app)
    {
        app.MapPost("/user/create", Create.PostCreate);
        app.MapGet("/user/get", Get.GetGet);
        app.MapPut("/user/update", Update.PutUpdate);
        app.MapDelete("/user/delete", Delete.DeleteDelete);
    }
}
