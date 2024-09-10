namespace AnonKey_Backend.ApiEndpoints.Users;

public static class Get
{
    public static Microsoft.AspNetCore.Http.HttpResults.Results<
        Ok<ApiDatastructures.Users.Get.ResponseBody>,
        NotFound<ApiDatastructures.Error.ErrorResponseBody>>
            GetGet()
    {
        throw new NotImplementedException();
    }
}
