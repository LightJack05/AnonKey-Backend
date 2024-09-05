namespace AnonKey_Backend.ApiEndpoints.Credentials;

public static class EndpointSetup 
{
    public static void MapEndpoints(WebApplication app)
    {
        app.MapPost("/credentials/create", null);
        app.MapGet("/credentials/get", null);
        app.MapGet("/credentials/getAll", null);
        app.MapPut("/credentials/update", null);
        app.MapDelete("/credentials/delete", null);
    }
}
