namespace AnonKey_Backend.ApiEndpoints.Authentication;

public static class ChangePassword
{
    
    public static Microsoft.AspNetCore.Http.HttpResults.Results<
        Ok,
        BadRequest<ApiDatastructures.Error.ErrorResponseBody>>
            PutChangePassword(ApiDatastructures.Authentication.ChangePassword.RequestBody requestBody)
    {
        throw new NotImplementedException(); 
    }
}
