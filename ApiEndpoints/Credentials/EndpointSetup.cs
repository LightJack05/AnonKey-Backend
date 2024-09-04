namespace AnonKey_Backend.ApiEndpoints.Credentials;

public static class EndpointSetup 
{
    public static void MapEndpoints(WebApplication app)
    {
        app.MapPost("/credentials/create", null);
        app.MapPost("/credentials/get", null);
        app.MapPost("/credentials/getAll", null);
        app.MapPost("/credentials/update", null);
        app.MapPost("/credentials/delete", null);
    }
}
