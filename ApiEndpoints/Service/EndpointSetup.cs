namespace AnonKey_Backend.ApiEndpoints.Service;

public static class EndpointSetup 
{
    public static void MapEndpoints(WebApplication app)
    {
        app.MapGet("/service/ping", Ping.GetPing);
    }

}
