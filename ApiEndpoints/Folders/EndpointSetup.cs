namespace AnonKey_Backend.ApiEndpoints.Folders;

public static class EndpointSetup 
{
    public static void MapEndpoints(WebApplication app)
    {
        app.MapPost("/folders/create", null);
        app.MapPost("/folders/get", null);
        app.MapPost("/folders/getAll", null);
        app.MapPost("/folders/update", null);
        app.MapPost("/folders/delete", null);
    }
}
