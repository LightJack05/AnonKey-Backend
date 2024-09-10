namespace AnonKey_Backend.ApiEndpoints.Users;

public static class Create
{
    public static Microsoft.AspNetCore.Http.HttpResults.Results<
        Ok<string>,
        BadRequest<ApiDatastructures.Error.ErrorResponseBody>>
           PostCreate(ApiDatastructures.Users.Create.RequestBody requestBody)
    {
        throw new NotImplementedException();
    }
}
