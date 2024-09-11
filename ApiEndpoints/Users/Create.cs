namespace AnonKey_Backend.ApiEndpoints.Users;

public static class Create
{
    /// <summary>
    /// Creates a new user.
    /// </summary>
    public static Microsoft.AspNetCore.Http.HttpResults.Results<
        Ok<ApiDatastructures.Users.Create.UsersCreateResponseBody>,
        BadRequest<ApiDatastructures.Error.ErrorResponseBody>>
           PostCreate(ApiDatastructures.Users.Create.UsersCreateRequestBody requestBody)
    {
        throw new NotImplementedException();
    }
}
