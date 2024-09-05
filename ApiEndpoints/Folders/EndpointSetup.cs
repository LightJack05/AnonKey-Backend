namespace AnonKey_Backend.ApiEndpoints.Folders;

public static class EndpointSetup 
{
    public static void MapEndpoints(WebApplication app)
    {
        app.MapPost("/folders/create", null);
        app.MapGet("/folders/get", null);
        app.MapGet("/folders/getAll", null);
        app.MapPut("/folders/update", null);
        app.MapDelete("/folders/delete", null);
    }
}
