namespace AnonKey_Backend.ApiEndpoints.Users;

public static class Get
{
    public static Microsoft.AspNetCore.Http.HttpResults.Results<
        Ok<ApiDatastructures.Users.Get.UsersGetResponseBody>,
        NotFound<ApiDatastructures.Error.ErrorResponseBody>>
            GetGet()
    {
        throw new NotImplementedException();
    }
}
