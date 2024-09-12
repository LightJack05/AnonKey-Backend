namespace AnonKey_Backend.ApiEndpoints;

public class EndpointSetup
{
    /// <summary>
    /// Entry point for mapping the endpoint methods to the paths
    /// </summary>
    /// <param name="app">The web app instance to initialize with the mapping</param>
    public static void Initialize(WebApplication app)
    {
        Authentication.EndpointSetup.MapEndpoints(app);
        Credentials.EndpointSetup.MapEndpoints(app);
        Folders.EndpointSetup.MapEndpoints(app);
        Service.EndpointSetup.MapEndpoints(app);
        Users.EndpointSetup.MapEndpoints(app);
        Uuid.EndpointSetup.MapEndpoints(app);
    }
}
