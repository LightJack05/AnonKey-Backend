namespace AnonKey_Backend.ApiEndpoints.Authentication;

public static class EndpointSetup 
{
    public static void MapEndpoints(WebApplication app)
    {
        app.MapPost("/authentication/login", null);
        app.MapPost("/authentication/logout", null);
        app.MapPost("/authentication/changePassword", null);
    }
}
