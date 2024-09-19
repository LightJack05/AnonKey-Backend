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
        if (String.IsNullOrEmpty(requestBody.Credential.Uuid) || String.IsNullOrEmpty(requestBody.Credential.Password) || String.IsNullOrEmpty(requestBody.Credential.PasswordSalt) || String.IsNullOrEmpty(requestBody.Credential.Username) || String.IsNullOrEmpty(requestBody.Credential.UsernameSalt) || String.IsNullOrEmpty(requestBody.Credential.WebsiteUrl) || String.IsNullOrEmpty(requestBody.Credential.Note) || String.IsNullOrEmpty(requestBody.Credential.DisplayName) || String.IsNullOrEmpty(requestBody.Credential.FolderUuid))
        {
            return TypedResults.BadRequest(new ApiDatastructures.Error.ErrorResponseBody()
            {
                Message = "A parameter in the request was null or an empty string",
                Detail = "One of the parameters in the request was null or an empty string. This is not allowed, please fill in all parameters.",
                InternalCode = 0x4
            });
        }

        if (!databaseHandle.Credentials.Any(c => c.Uuid == requestBody.Credential.Uuid))
        {
            return TypedResults.NotFound(new ApiDatastructures.Error.ErrorResponseBody()
            {
                Message = "No credetial with this Uuid was not found in the database",
                Detail = "No credetial with the provided credentialUuid was not found. Please make sure the correct credentialUuid is provided.",
                InternalCode = 0x6
            });
        }

        if (databaseHandle.Users.Single(u => u.Username == user.Identity.Name).Uuid != databaseHandle.Credentials.Single(c => c.Uuid == requestBody.Credential.Uuid).UserUuid)
        {
            return TypedResults.NotFound(new ApiDatastructures.Error.ErrorResponseBody()
            {
                Message = "This user does not have access to this credential",
                Detail = "The credential with the provided uuid does not belong to this user. This is not allowed, please make sure to provide a valid input.",
                InternalCode = 0x7
            });
        }

        if (databaseHandle.Credentials.Single(c => c.Uuid == requestBody.Credential.Uuid).DeletedTimestamp is not null)
        {
            return TypedResults.BadRequest(new ApiDatastructures.Error.ErrorResponseBody()
            {
                Message = "This credential was deleted and can not be edited anymore.",
                Detail = "This credential was deleted and can not be accessed in order to edit anymore. Please provide a valid uuid in oder to change an existing credetntial.",
                InternalCode = 0x8
            });
        }

        UpdateCredential(requestBody, user, databaseHandle);
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
                Note = NewCredential.Note,
                DisplayName = NewCredential.DisplayName,
                CreatedTimestamp = NewCredential.CreatedTimestamp,
                ChangedTimestamp = (long)DateTime.UtcNow.Subtract(DateTime.UnixEpoch).TotalSeconds,
                DeletedTimestamp = NewCredential.DeletedTimestamp
            }
        };
    }

    private static void UpdateCredential(CredentialsUpdateRequestBody requestBody, ClaimsPrincipal user, DatabaseHandle databaseHandle)
    {
        AnonKey_Backend.Models.Credential FetchedCredential = databaseHandle.Credentials.Single(c => c.Uuid == requestBody.Credential.Uuid);
        FetchedCredential.Uuid = requestBody.Credential.Uuid;
        FetchedCredential.UserUuid = databaseHandle.Users.SingleOrDefault(u => u.Username == user.Identity.Name).Uuid;
        FetchedCredential.FolderUuid = requestBody.Credential.FolderUuid;
        FetchedCredential.Password = requestBody.Credential.Password;
        FetchedCredential.PasswordSalt = requestBody.Credential.PasswordSalt;
        FetchedCredential.Username = requestBody.Credential.Username;
        FetchedCredential.UsernameSalt = requestBody.Credential.UsernameSalt;
        FetchedCredential.WebsiteUrl = requestBody.Credential.WebsiteUrl;
        FetchedCredential.Note = requestBody.Credential.Note;
        FetchedCredential.DisplayName = requestBody.Credential.DisplayName;
        FetchedCredential.CreatedTimestamp = databaseHandle.Credentials.Single(c => c.Uuid == requestBody.Credential.Uuid).CreatedTimestamp;
        FetchedCredential.ChangedTimestamp = (long)DateTime.UtcNow.Subtract(DateTime.UnixEpoch).TotalSeconds;
        FetchedCredential.DeletedTimestamp = databaseHandle.Credentials.Single(c => c.Uuid == requestBody.Credential.Uuid).DeletedTimestamp;
    }
}
