namespace AnonKey_Backend.ApiEndpoints.Credentials;

public static class Get
{

    /// <summary>
    /// Gets information on a credential object.
    /// </summary>
    public static Microsoft.AspNetCore.Http.HttpResults.Results<
        Ok<ApiDatastructures.Credentials.Get.CredentialsGetResponseBody>,
        NotFound<ApiDatastructures.Error.ErrorResponseBody>,
        BadRequest<ApiDatastructures.Error.ErrorResponseBody>>
            GetGet(string credentialUuid, ClaimsPrincipal user)
    {
        throw new NotImplementedException();
    }
}
