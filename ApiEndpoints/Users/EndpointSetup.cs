namespace KeyShare_Backend.ApiEndpoints.Users;

public static class EndpointSetup 
{
    public static void MapEndpoints(WebApplication app)
    {
        app.MapPost("/user/create", null);
        app.MapPost("/user/get", null);
        app.MapPost("/user/update", null);
        app.MapPost("/user/delete", null);
    }
}
