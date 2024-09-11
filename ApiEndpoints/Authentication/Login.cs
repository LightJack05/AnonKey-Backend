namespace AnonKey_Backend.ApiEndpoints.Authentication;

public static class Login
{
    
    /// <summary>
    /// API endpoint method that authenticates the user and returns an access token. 
    /// </summary>
    public static Microsoft.AspNetCore.Http.HttpResults.Results<
        Ok<ApiDatastructures.Authentication.Login.AuthenticationLoginResponseBody>,
        BadRequest<ApiDatastructures.Error.ErrorResponseBody>>
            PostLogin(ApiDatastructures.Authentication.Login.AuthenticationLoginRequestBody requestBody)
    {
        throw new NotImplementedException(); 
    }
}
