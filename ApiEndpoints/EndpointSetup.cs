namespace AnonKey_Backend.ApiEndpoints;

public class EndpointSetup
{
    public static void Initialize(WebApplication app)
    {
        Service.ServiceEndpoints.MapEndpoints(app);
    }
}
