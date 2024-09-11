namespace AnonKey_Backend.ApiEndpoints.Users;

public static class Update
{
    /// <summary>
    /// API endpoint method that updates an existing user.
    /// </summary>
    public static Microsoft.AspNetCore.Http.HttpResults.Results<
        Ok,
        NotFound<ApiDatastructures.Error.ErrorResponseBody>,
        BadRequest<ApiDatastructures.Error.ErrorResponseBody>> 
            PutUpdate(ApiDatastructures.Users.Update.UsersUpdateRequestBody requestBody)
    {
        throw new NotImplementedException();
    }
}
