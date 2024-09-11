namespace AnonKey_Backend.ApiEndpoints.Authentication;

public static class Logout
{

    /// <summary>
    /// API endpoint method that invalidates an access token for user logout. 
    /// </summary>
    public static Microsoft.AspNetCore.Http.HttpResults.Results<
        Ok,
        BadRequest<ApiDatastructures.Error.ErrorResponseBody>>
            DeleteLogout()
    {
        throw new NotImplementedException(); 
    }
}
