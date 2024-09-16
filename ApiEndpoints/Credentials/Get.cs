namespace AnonKey_Backend.ApiEndpoints.Credentials;

/// <summary>
/// Handles the credentials get endpoint.
/// </summary>
public static class Get
{

    /// <summary>
    /// Gets information on a credential object.
    /// </summary>
    public static Microsoft.AspNetCore.Http.HttpResults.Results<
        Ok<ApiDatastructures.Credentials.Get.CredentialsGetResponseBody>,
        NotFound<ApiDatastructures.Error.ErrorResponseBody>,
        BadRequest<ApiDatastructures.Error.ErrorResponseBody>>
            GetGet(string credentialUuid, ClaimsPrincipal user, Data.DatabaseHandle databaseHandle)
    {
        throw new NotImplementedException();
    }
}
