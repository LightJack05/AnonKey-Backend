namespace AnonKey_Backend.ApiEndpoints.Users;

public static class Update
{
    public static Microsoft.AspNetCore.Http.HttpResults.Results<
        Ok,
        BadRequest<ApiDatastructures.Error.ErrorResponseBody>> 
            PutUpdate(ApiDatastructures.Users.Update.RequestBody requestBody)
    {
        throw new NotImplementedException();
    }
}
