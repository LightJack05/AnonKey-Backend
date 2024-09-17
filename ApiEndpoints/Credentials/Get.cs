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
        databaseHandle.Database.EnsureCreated();
        if (credentialUuid is null)
        {
            return TypedResults.BadRequest(new ApiDatastructures.Error.ErrorResponseBody()
            {
                Message = "A credentialUuid parameter in the request was null",
                Detail = "credentialUuid parameter in the request was null. This is not allowed, please fill in all parameters.",
                InternalCode = 0x4
            });
        }

        if (credentialUuid == "")
        {
            return TypedResults.BadRequest(new ApiDatastructures.Error.ErrorResponseBody()
            {
                Message = "A credentialUuid parameter in the request was an empty stirng",
                Detail = "One of the following parameters was an empty string: credentialUuid. This is not allowed, please make sure to provide a valid input.",
                InternalCode = 0x5
            });
        }

        if (!databaseHandle.Credentials.Any(c => c.Uuid == credentialUuid))
        {
            return TypedResults.NotFound(new ApiDatastructures.Error.ErrorResponseBody()
            {
                Message = "No credetial with this Uuid was not found in the database",
                Detail = "No credetial with the provided credentialUuid was not found. Please make sure the correct credentialUuid is provided.",
                InternalCode = 0x6
            });
        }

        AnonKey_Backend.Models.Credential FetchedCredential = databaseHandle.Credentials.SingleOrDefault(c => c.Uuid == credentialUuid);
        return TypedResults.Ok(new ApiDatastructures.Credentials.Get.CredentialsGetResponseBody()
        {
            Credential = new AnonKey_Backend.ApiDatastructures.Credentials.Get.CredentialsGetCredential()
            {
                Uuid = FetchedCredential.Uuid,
                Password = FetchedCredential.Password,
                PasswordSalt = FetchedCredential.PasswordSalt,
                Username = FetchedCredential.Username,
                UsernameSalt = FetchedCredential.UsernameSalt,
                WebsiteUrl = FetchedCredential.WebsiteUrl,
                Note = FetchedCredential.Note,
                DisplayName = FetchedCredential.DisplayName,
                FolderUuid = FetchedCredential.FolderUuid,
                CreatedTimestamp = FetchedCredential.CreatedTimestamp,
                ChangedTimestamp = FetchedCredential.ChangedTimestamp,
                DeletedTimestamp = FetchedCredential.DeletedTimestamp
            },
        });
    }
}
