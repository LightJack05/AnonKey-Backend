using AnonKeyBackend.ApiDatastructures.Credentials.Get;
using AnonKeyBackend.Models;

namespace AnonKeyBackend.ApiEndpoints.Credentials;

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
        if (user.Identity == null)
        {
            return TypedResults.BadRequest(new ApiDatastructures.Error.ErrorResponseBody()
            {
                Message = "The user identity is null",
                Detail = "The user identity is null. Did you provide a valid JWT token?"
            });
        }
        if (String.IsNullOrEmpty(credentialUuid))
        {
            return TypedResults.BadRequest(new ApiDatastructures.Error.ErrorResponseBody()
            {
                Message = "A credentialUuid in the request was null or an empty string",
                Detail = "credentialUuid parameter in the request was null or an empty string. This is not allowed, please fill in all parameters."
            });
        }

        if (!databaseHandle.Credentials.Any(c => c.Uuid == credentialUuid))
        {
            return TypedResults.NotFound(new ApiDatastructures.Error.ErrorResponseBody()
            {
                Message = "No credetial with this Uuid was not found in the database",
                Detail = "No credetial with the provided credentialUuid was not found. Please make sure the correct credentialUuid is provided."
            });
        }

        if (databaseHandle.Users.Single(u => u.Username == user.Identity.Name).Uuid != databaseHandle.Credentials.Single(c => c.Uuid == credentialUuid).UserUuid)
        {
            return TypedResults.BadRequest(new ApiDatastructures.Error.ErrorResponseBody()
            {
                Message = "This user does not have access to this credential",
                Detail = "The credential with the provided uuid does not belong to this user. This is not allowed, please make sure to provide a valid input."
            });
        }

        return TypedResults.Ok(ConstructResponse(databaseHandle.Credentials.SingleOrDefault(c => c.Uuid == credentialUuid)));
    }

    private static CredentialsGetResponseBody ConstructResponse(Credential? FetchedCredential)
    {
        if (FetchedCredential is null) throw new ArgumentNullException();
        return new ApiDatastructures.Credentials.Get.CredentialsGetResponseBody()
        {
            Credential = new AnonKeyBackend.ApiDatastructures.Credentials.Get.CredentialsGetCredential()
            {
                Uuid = FetchedCredential.Uuid,
                Password = FetchedCredential.Password,
                PasswordSalt = FetchedCredential.PasswordSalt,
                Username = FetchedCredential.Username,
                UsernameSalt = FetchedCredential.UsernameSalt,
                WebsiteUrl = FetchedCredential.WebsiteUrl,
                WebsiteUrlSalt = FetchedCredential.WebsiteUrlSalt,
                Note = FetchedCredential.Note,
                NoteSalt = FetchedCredential.NoteSalt,
                DisplayName = FetchedCredential.DisplayName,
                DisplayNameSalt = FetchedCredential.DisplayNameSalt,
                FolderUuid = FetchedCredential.FolderUuid,
                CreatedTimestamp = FetchedCredential.CreatedTimestamp,
                ChangedTimestamp = FetchedCredential.ChangedTimestamp,
                DeletedTimestamp = FetchedCredential.DeletedTimestamp
            },
        };
    }
}
