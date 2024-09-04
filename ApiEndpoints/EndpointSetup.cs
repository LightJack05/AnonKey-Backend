namespace KeyShare_Backend.ApiEndpoints;

public class EndpointSetup
{
    public static void Initialize(WebApplication app)
    {
        Authentication.EndpointSetup.MapEndpoints(app);
        Credentials.EndpointSetup.MapEndpoints(app);
        Folders.EndpointSetup.MapEndpoints(app);
        Service.EndpointSetup.MapEndpoints(app);
        Users.EndpointSetup.MapEndpoints(app);
    }
}
