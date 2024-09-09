namespace AnonKey_Backend.ApiEndpoints.Folders;

public static class EndpointSetup 
{
    public static void MapEndpoints(WebApplication app)
    {
        app.MapPost("/folders/create", Create.PostCreate);
        app.MapGet("/folders/get", Get.GetGet);
        app.MapGet("/folders/getAll", GetAll.GetAllGet);
        app.MapPut("/folders/update", Update.PutUpdate);
        app.MapDelete("/folders/delete", Delete.DeleteDelete);
    }
}
