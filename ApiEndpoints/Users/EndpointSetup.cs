namespace AnonKey_Backend.ApiEndpoints.Users;

public static class EndpointSetup 
{
    public static void MapEndpoints(WebApplication app)
    {
        app.MapPost("/user/create", null);
        app.MapGet("/user/get", null);
        app.MapPut("/user/update", null);
        app.MapDelete("/user/delete", null);
    }
}
