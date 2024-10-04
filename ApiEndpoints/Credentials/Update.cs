using AnonKey_Backend.ApiDatastructures.Credentials.Update;
using AnonKey_Backend.Data;
using AnonKey_Backend.Models;

namespace AnonKey_Backend.ApiEndpoints.Credentials;

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
        NotFound<ApiDatastructures.Error.ErrorResponseBody>,
        BadRequest<ApiDatastructures.Error.ErrorResponseBody>>
            PutUpdate(ApiDatastructures.Credentials.Update.CredentialsUpdateRequestBody requestBody, ClaimsPrincipal user, Data.DatabaseHandle databaseHandle)
    {
        databaseHandle.Database.EnsureCreated();
        if (requestBody.Credential is null || String.IsNullOrEmpty(requestBody.Credential.Uuid) || String.IsNullOrEmpty(requestBody.Credential.DisplayName) || String.IsNullOrEmpty(requestBody.Credential.DisplayNameSalt))
        {
            return TypedResults.BadRequest(new ApiDatastructures.Error.ErrorResponseBody()
            {
                Message = "A parameter in the request was null or an empty string",
                Detail = "One of the parameters in the request was null or an empty string. This is not allowed, please fill in all parameters.",
                InternalCode = 0x4
            });
        }

        // If some fields are not null, check for the salt to be there
        if (!(requestBody.Credential.WebsiteUrl is null) && (requestBody.Credential.WebsiteUrlSalt is null))
        {
            return TypedResults.BadRequest(new ApiDatastructures.Error.ErrorResponseBody()
            {
                Message = "WebsiteUrl was provieded, but the salt for it was not.",
                Detail = "WebsiteUrlSalt is null, but WebsiteUrl is not null. This is not allowed, please fill in all parameters.",
                InternalCode = 0x4
            });
        }

        if (!(requestBody.Credential.Username is null) && (requestBody.Credential.UsernameSalt is null))
        {
            return TypedResults.BadRequest(new ApiDatastructures.Error.ErrorResponseBody()
            {
                Message = "Username was provieded, but the salt for it was not.",
                Detail = "UsernameSalt is null, but Username is not null. This is not allowed, please fill in all parameters.",
                InternalCode = 0x4
            });
        }

        if (!(requestBody.Credential.Password is null) && (requestBody.Credential.PasswordSalt is null))
        {
            return TypedResults.BadRequest(new ApiDatastructures.Error.ErrorResponseBody()
            {
                Message = "Password was provieded, but the salt for it was not.",
                Detail = "PasswordSalt is null, but Password is not null. This is not allowed, please fill in all parameters.",
                InternalCode = 0x4
            });
        }

        if (!(requestBody.Credential.Note is null) && (requestBody.Credential.NoteSalt is null))
        {
            return TypedResults.BadRequest(new ApiDatastructures.Error.ErrorResponseBody()
            {
                Message = "Note was provieded, but the salt for it was not.",
                Detail = "NoteSalt is null, but Note is not null. This is not allowed, please fill in all parameters.",
                InternalCode = 0x4
            });
        }

        AnonKey_Backend.Models.Credential FetchedCredential = databaseHandle.Credentials.Single(c => c.Uuid == requestBody.Credential.Uuid);
        string UserUuid = databaseHandle.Users.Single(u => u.Username == user.Identity.Name).Uuid;

        if (FetchedCredential is null)
        {
            return TypedResults.NotFound(new ApiDatastructures.Error.ErrorResponseBody()
            {
                Message = "No credetial with this Uuid was not found in the database",
                Detail = "No credetial with the provided credentialUuid was not found. Please make sure the correct credentialUuid is provided.",
                InternalCode = 0x6
            });
        }

        if (UserUuid != FetchedCredential.UserUuid)
        {
            return TypedResults.NotFound(new ApiDatastructures.Error.ErrorResponseBody()
            {
                Message = "This user does not have access to this credential",
                Detail = "The credential with the provided uuid does not belong to this user. This is not allowed, please make sure to provide a valid input.",
                InternalCode = 0x7
            });
        }

        UpdateCredential(requestBody, user, UserUuid, FetchedCredential);
        databaseHandle.SaveChanges();
        AnonKey_Backend.Models.Credential NewCredential = databaseHandle.Credentials.Single(c => c.Uuid == requestBody.Credential.Uuid);
        return TypedResults.Ok(CronstructResponse(NewCredential));
    }

    private static CredentialsUpdateResponseBody CronstructResponse(Credential NewCredential)
    {
        return new AnonKey_Backend.ApiDatastructures.Credentials.Update.CredentialsUpdateResponseBody()
        {
            Credential = new AnonKey_Backend.ApiDatastructures.Credentials.Update.CredentialsUpdateCredentialResponse()
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
                DeletedTimestamp = NewCredential.DeletedTimestamp
            }
        };
    }

    private static void UpdateCredential(CredentialsUpdateRequestBody requestBody, ClaimsPrincipal user, string UserUuid, AnonKey_Backend.Models.Credential FetchedCredential)
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
        FetchedCredential.DeletedTimestamp = requestBody.Credential.DeletedTimestamp;
    }
}
