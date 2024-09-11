namespace AnonKey_Backend.ApiEndpoints.Authentication;

public static class Logout
{

    public static Microsoft.AspNetCore.Http.HttpResults.Results<
        Ok,
        BadRequest<ApiDatastructures.Error.ErrorResponseBody>>
            DeleteLogout()
    {
        throw new NotImplementedException(); 
    }
}
