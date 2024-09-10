namespace AnonKey_Backend.ApiEndpoints.Credentials;

public static class Get
{

    public static Microsoft.AspNetCore.Http.HttpResults.Results<
        Ok<ApiDatastructures.Credentials.Get.CredentialsGetResponseBody>,
        NotFound<ApiDatastructures.Error.ErrorResponseBody>,
        BadRequest<ApiDatastructures.Error.ErrorResponseBody>>
            GetGet(string credentialUuid)
    {
        throw new NotImplementedException();
    }
}
