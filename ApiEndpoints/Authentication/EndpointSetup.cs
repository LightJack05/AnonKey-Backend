namespace AnonKey_Backend.ApiEndpoints.Authentication;

public static class EndpointSetup 
{
    public static void MapEndpoints(WebApplication app)
    {
        app.MapPost("/authentication/login", null);
        app.MapDelete("/authentication/logout", null);
        app.MapPut("/authentication/changePassword", null);
    }
}