namespace AnonKey_Backend.ApiEndpoints.Service;
/// <summary>
/// Handles the ping service endpoint.
/// </summary>
public static class Ping
{
    /// <summary>
    /// Checks the connection to the server.
    /// </summary>
    /// <returns>pong if the server is reachable.</returns>
    public static string GetPing()
    {
        return "pong";
    }
}
