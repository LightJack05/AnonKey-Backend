namespace AnonKey_Backend.ApiEndpoints.Authentication;

public static class Login
{
    
    public static Microsoft.AspNetCore.Http.HttpResults.Results<
        Ok<string>,
        BadRequest<ApiDatastructures.Error.ErrorResponseBody>>
            PostLogin(ApiDatastructures.Authentication.Login.RequestBody requestBody)
    {
        throw new NotImplementedException(); 
    }
}
