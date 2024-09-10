namespace AnonKey_Backend.ApiEndpoints.Authentication;

public static class Login
{
    
    public static Microsoft.AspNetCore.Http.HttpResults.Results<
        Ok<ApiDatastructures.Authentication.Login.AuthenticationLoginResponseBody>,
        BadRequest<ApiDatastructures.Error.ErrorResponseBody>>
            PostLogin(ApiDatastructures.Authentication.Login.AuthenticationLoginRequestBody requestBody)
    {
        throw new NotImplementedException(); 
    }
}
