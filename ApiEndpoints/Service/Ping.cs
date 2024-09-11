namespace AnonKey_Backend.ApiEndpoints.Service;
public static class Ping
{
    /// <summary>
    /// API endpoint method that checks the connection to the server.
    /// </summary>
    public static string GetPing()
    {
        return "pong";
    }
}
