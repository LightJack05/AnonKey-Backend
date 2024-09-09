namespace AnonKey_Backend.ApiEndpoints.Authentication;

public static class EndpointSetup 
{
    public static void MapEndpoints(WebApplication app)
    {
        app.MapPost("/authentication/login", Login.PostLogin);
        app.MapDelete("/authentication/logout", Logout.DeleteLogout);
        app.MapPut("/authentication/changePassword", Logout.DeleteLogout);
    }
}
