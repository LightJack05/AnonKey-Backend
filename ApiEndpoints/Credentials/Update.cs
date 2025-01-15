using AnonKeyBackend.ApiDatastructures.Credentials.Update;
using AnonKeyBackend.Models;

namespace AnonKeyBackend.ApiEndpoints.Credentials;

/// <summary>
/// Handles the editing of credentials.
/// </summary>
public static class Update
{

    /// <summary>
    /// Updates a credential object.
    /// </summary>
    public static Microsoft.AspNetCore.Http.HttpResults.Results<
        Ok<ApiDatastructures.Credentials.Update.CredentialsUpdateResponseBody>,
        NotFound<ApiDatastructures.RequestError.ErrorResponseBody>,
        BadRequest<ApiDatastructures.RequestError.ErrorResponseBody>,
        UnauthorizedHttpResult>
            PutUpdate(ApiDatastructures.Credentials.Update.CredentialsUpdateRequestBody requestBody, ClaimsPrincipal user, Data.DatabaseHandle databaseHandle)
    {
        databaseHandle.Database.EnsureCreated();
        if (!AnonKeyBackend.Authentication.TokenActions.ValidateClaimsOnRequest(user, databaseHandle))
        {
            return TypedResults.Unauthorized();
        }
        if (user.Identity == null)
        {
            return TypedResults.BadRequest(new ApiDatastructures.RequestError.ErrorResponseBody()
            {
                Message = "The user identity is null",
                Detail = "The user identity is null. Did you provide a valid JWT token?"
            });
        }

        if (requestBody.Credential is null || String.IsNullOrEmpty(requestBody.Credential.Uuid) || String.IsNullOrEmpty(requestBody.Credential.DisplayName) || String.IsNullOrEmpty(requestBody.Credential.DisplayNameSalt))
        {
            return TypedResults.BadRequest(new ApiDatastructures.RequestError.ErrorResponseBody()
            {
                Message = "A parameter in the request was null or an empty string",
                Detail = "One of the parameters in the request was null or an empty string. This is not allowed, please fill in all parameters."
            });
        }

        // If some fields are not null, check for the salt to be there
        if (!String.IsNullOrEmpty(requestBody.Credential.WebsiteUrl) && String.IsNullOrEmpty(requestBody.Credential.WebsiteUrlSalt))
        {
            return TypedResults.BadRequest(new ApiDatastructures.RequestError.ErrorResponseBody()
            {
                Message = "WebsiteUrl was provided, but the salt for it was not.",
                Detail = "WebsiteUrlSalt is null or an empty string, but WebsiteUrl is not null. This is not allowed, please fill in all parameters."
            });
        }

        if (!String.IsNullOrEmpty(requestBody.Credential.Username) && String.IsNullOrEmpty(requestBody.Credential.UsernameSalt))
        {
            return TypedResults.BadRequest(new ApiDatastructures.RequestError.ErrorResponseBody()
            {
                Message = "Username was provided, but the salt for it was not.",
                Detail = "UsernameSalt is null or an empty string, but Username is not null. This is not allowed, please fill in all parameters."
            });
        }

        if (!String.IsNullOrEmpty(requestBody.Credential.Password) && String.IsNullOrEmpty(requestBody.Credential.PasswordSalt))
        {
            return TypedResults.BadRequest(new ApiDatastructures.RequestError.ErrorResponseBody()
            {
                Message = "Password was provided, but the salt for it was not.",
                Detail = "PasswordSalt is null or an empty string, but Password is not null. This is not allowed, please fill in all parameters."
            });
        }

        if (!String.IsNullOrEmpty(requestBody.Credential.Note) && String.IsNullOrEmpty(requestBody.Credential.NoteSalt))
        {
            return TypedResults.BadRequest(new ApiDatastructures.RequestError.ErrorResponseBody()
            {
                Message = "Note was provided, but the salt for it was not.",
                Detail = "NoteSalt is null or an empty string, but Note is not null. This is not allowed, please fill in all parameters."
            });
        }


        AnonKeyBackend.Models.Credential? FetchedCredential = databaseHandle.Credentials.SingleOrDefault(c => c.Uuid == requestBody.Credential.Uuid);
        AnonKeyBackend.Models.User? FetchedUser = databaseHandle.Users.SingleOrDefault(u => u.Username == user.Identity.Name);

        if (FetchedCredential is null)
        {
            return TypedResults.NotFound(new ApiDatastructures.RequestError.ErrorResponseBody()
            {
                Message = "No credetial with this Uuid was not found in the database",
                Detail = "No credetial with the provided credentialUuid was not found. Please make sure the correct credentialUuid is provided."
            });
        }

        if (FetchedUser is null)
        {
            return TypedResults.NotFound(new ApiDatastructures.RequestError.ErrorResponseBody()
            {
                Message = "No user with this Uuid was not found in the database",
                Detail = "No user with the provided credentialUuid was not found. Please make sure the correct UserUuid is provided."
            });
        }

        string? UserUuid = FetchedUser.Uuid;

        if (UserUuid != FetchedCredential.UserUuid)
        {
            return TypedResults.NotFound(new ApiDatastructures.RequestError.ErrorResponseBody()
            {
                Message = "This user does not have access to this credential",
                Detail = "The credential with the provided uuid does not belong to this user. This is not allowed, please make sure to provide a valid input."
            });
        }

        UpdateCredential(requestBody, user, UserUuid, FetchedCredential);
        databaseHandle.SaveChanges();
        AnonKeyBackend.Models.Credential NewCredential = databaseHandle.Credentials.Single(c => c.Uuid == requestBody.Credential.Uuid);
        return TypedResults.Ok(CronstructResponse(NewCredential));
    }

    private static CredentialsUpdateResponseBody CronstructResponse(Credential NewCredential)
    {
        return new AnonKeyBackend.ApiDatastructures.Credentials.Update.CredentialsUpdateResponseBody()
        {
            Credential = new AnonKeyBackend.ApiDatastructures.Credentials.Update.CredentialsUpdateCredentialResponse()
            {
                Uuid = NewCredential.Uuid,
                FolderUuid = NewCredential.FolderUuid,
                Password = NewCredential.Password,
                PasswordSalt = NewCredential.PasswordSalt,
                Username = NewCredential.Username,
                UsernameSalt = NewCredential.UsernameSalt,
                WebsiteUrl = NewCredential.WebsiteUrl,
                WebsiteUrlSalt = NewCredential.WebsiteUrlSalt,
                Note = NewCredential.Note,
                NoteSalt = NewCredential.NoteSalt,
                DisplayName = NewCredential.DisplayName,
                DisplayNameSalt = NewCredential.DisplayNameSalt,
                CreatedTimestamp = NewCredential.CreatedTimestamp,
                ChangedTimestamp = NewCredential.ChangedTimestamp,
            }
        };
    }

    private static void UpdateCredential(CredentialsUpdateRequestBody requestBody, ClaimsPrincipal user, string? UserUuid, AnonKeyBackend.Models.Credential FetchedCredential)
    {
        if (requestBody.Credential is not null)
        {
            FetchedCredential.Uuid = requestBody.Credential.Uuid;
            FetchedCredential.UserUuid = UserUuid;
            FetchedCredential.FolderUuid = requestBody.Credential.FolderUuid;
            FetchedCredential.Password = requestBody.Credential.Password;
            FetchedCredential.PasswordSalt = requestBody.Credential.PasswordSalt;
            FetchedCredential.Username = requestBody.Credential.Username;
            FetchedCredential.UsernameSalt = requestBody.Credential.UsernameSalt;
            FetchedCredential.WebsiteUrl = requestBody.Credential.WebsiteUrl;
            FetchedCredential.WebsiteUrlSalt = requestBody.Credential.WebsiteUrlSalt;
            FetchedCredential.Note = requestBody.Credential.Note;
            FetchedCredential.NoteSalt = requestBody.Credential.NoteSalt;
            FetchedCredential.DisplayName = requestBody.Credential.DisplayName;
            FetchedCredential.DisplayNameSalt = requestBody.Credential.DisplayNameSalt;
            FetchedCredential.ChangedTimestamp = (long)DateTime.UtcNow.Subtract(DateTime.UnixEpoch).TotalSeconds;
        }
    }
}
