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
        if (requestBody.Credential is null || requestBody.Credential.Uuid is null || requestBody.Credential.Password is null || requestBody.Credential.PasswordSalt is null || requestBody.Credential.Username is null || requestBody.Credential.UsernameSalt is null || requestBody.Credential.WebsiteUrl is null || requestBody.Credential.Note is null || requestBody.Credential.DisplayName is null || requestBody.Credential.FolderUuid is null)
        {
            return TypedResults.BadRequest(new ApiDatastructures.Error.ErrorResponseBody()
            {
                Message = "A parameter in the request was null",
                Detail = "One of the parameters in the request was null. This is not allowed, please fill in all parameters.",
                InternalCode = 0x4
            });
        }

        if (requestBody.Credential.Uuid == "" || requestBody.Credential.Password == "" || requestBody.Credential.PasswordSalt == "" || requestBody.Credential.Username == "" || requestBody.Credential.UsernameSalt == "" || requestBody.Credential.WebsiteUrl == "" || requestBody.Credential.DisplayName == "" || requestBody.Credential.FolderUuid == "")
        {
            return TypedResults.BadRequest(new ApiDatastructures.Error.ErrorResponseBody()
            {
                Message = "A parameter in the request was an empty stirng",
                Detail = "One of the following parameters was an empty string: Uuid, Password, PasswordSalt, Username, UsernameSalt, WebsiteUrl, DisplayName, FolderUuid. This is not allowed, please make sure to provide a valid input.",
                InternalCode = 0x5
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

        AnonKey_Backend.Models.Credential NewCredential = new Models.Credential()
        {
            Uuid = requestBody.Credential.Uuid,
            UserUuid = databaseHandle.Users.SingleOrDefault(u => u.Username == user.Identity.Name).Uuid,
            FolderUuid = requestBody.Credential.FolderUuid,
            Password = requestBody.Credential.Password,
            PasswordSalt = requestBody.Credential.PasswordSalt,
            Username = requestBody.Credential.Username,
            UsernameSalt = requestBody.Credential.UsernameSalt,
            WebsiteUrl = requestBody.Credential.WebsiteUrl,
            Note = requestBody.Credential.Note,
            DisplayName = requestBody.Credential.DisplayName,
            // Not sure about the Timestamps, especially DeletedTimestamp
            CreatedTimestamp = (long)DateTime.UtcNow.Subtract(DateTime.UnixEpoch).TotalSeconds,
            ChangedTimestamp = (long)DateTime.UtcNow.Subtract(DateTime.UnixEpoch).TotalSeconds,
            DeletedTimestamp = 0,
        };
        /*var FetchedCredetial = databaseHandle.Credentials.Single(c => c.Uuid == requestBody.Credential.Uuid);*/
        /*FetchedCredetial = NewCredential;*/
        databaseHandle.Credentials.Update(NewCredential);
        databaseHandle.SaveChanges();
        return TypedResults.Ok(new AnonKey_Backend.ApiDatastructures.Credentials.Update.CredentialsUpdateResponseBody()
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
        });
    }
}
