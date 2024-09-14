namespace AnonKey_Backend.ApiEndpoints.Authentication;

/// <summary>
/// Handles the authentication logout endpoints.
/// </summary>
public static class Logout
{

    /// <summary>
    /// Invalidates an access token for user logout. 
    /// </summary>
    public static Microsoft.AspNetCore.Http.HttpResults.Results<
        Ok,
        BadRequest<ApiDatastructures.Error.ErrorResponseBody>>
            DeleteLogout(ClaimsPrincipal user)
    {
        throw new NotImplementedException(); 
    }
}
