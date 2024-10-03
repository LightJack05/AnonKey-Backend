namespace AnonKey_Backend.ApiEndpoints.Credentials;

/// <summary>
/// Handles the credentials creation endpoint.
/// </summary>
public static class Create
{

    /// <summary>
    /// Creates a new credential object.
    /// </summary>
    public static Microsoft.AspNetCore.Http.HttpResults.Results<
        Ok,
        Conflict<ApiDatastructures.Error.ErrorResponseBody>,
        BadRequest<ApiDatastructures.Error.ErrorResponseBody>>
     PostCreate(ApiDatastructures.Credentials.Create.CredentialsCreateRequestBody requestBody, ClaimsPrincipal user, Data.DatabaseHandle databaseHandle)
    {
        databaseHandle.Database.EnsureCreated();
        if (user.Identity == null)
        {
            return TypedResults.BadRequest(new ApiDatastructures.Error.ErrorResponseBody()
            {
                Message = "The user identity is null",
                Detail = "The user identity is null. Did you provide a valid JWT token?",
                InternalCode = 0x4
            });
        }
        if (requestBody.Credential is null)
        {
            return TypedResults.BadRequest(new ApiDatastructures.Error.ErrorResponseBody()
            {
                Message = "The credential provieded in the requestBody is null",
                Detail = "The credential provieded in the requestBody is null. This is not allowed, please fill in all parameters.",
                InternalCode = 0x4
            });
        }
        if (String.IsNullOrEmpty(requestBody.Credential.Uuid) || String.IsNullOrEmpty(requestBody.Credential.Password) || String.IsNullOrEmpty(requestBody.Credential.PasswordSalt) || String.IsNullOrEmpty(requestBody.Credential.Username) || String.IsNullOrEmpty(requestBody.Credential.UsernameSalt) || String.IsNullOrEmpty(requestBody.Credential.WebsiteUrl) || String.IsNullOrEmpty(requestBody.Credential.Note) || String.IsNullOrEmpty(requestBody.Credential.DisplayName) || String.IsNullOrEmpty(requestBody.Credential.FolderUuid) || String.IsNullOrEmpty(requestBody.Credential.WebsiteUrlSalt) || String.IsNullOrEmpty(requestBody.Credential.NoteSalt) || String.IsNullOrEmpty(requestBody.Credential.DisplayNameSalt))
        {
            return TypedResults.BadRequest(new ApiDatastructures.Error.ErrorResponseBody()
            {
                Message = "A parameter in the request was null or an empty string",
                Detail = "One of the parameters in the request was null or an empty string. This is not allowed, please fill in all parameters.",
                InternalCode = 0x4
            });
        }

        if (databaseHandle.Credentials.Any(c => c.Uuid == requestBody.Credential.Uuid))
        {
            return TypedResults.Conflict(new ApiDatastructures.Error.ErrorResponseBody()
            {
                Message = "A credential with this uuid already exists.",
                Detail = "There is already a user object for the given uuid in the database. Please try changing the uuid and resending the request.",
                InternalCode = 0x2
            });
        }

        Models.Credential NewCredential = CreateNewCredential(requestBody, user, databaseHandle);

        databaseHandle.Credentials.Add(NewCredential);
        databaseHandle.SaveChanges();
        return TypedResults.Ok();
    }

    private static Models.Credential CreateNewCredential(ApiDatastructures.Credentials.Create.CredentialsCreateRequestBody requestBody, ClaimsPrincipal user, Data.DatabaseHandle databaseHandle)
    {
        if (user.Identity is null) throw new ArgumentNullException();
        return new Models.Credential()
        {
            Uuid = requestBody.Credential.Uuid,
            UserUuid = databaseHandle.Users.SingleOrDefault(u => u.Username == user.Identity.Name).Uuid,
            FolderUuid = requestBody.Credential.FolderUuid,
            Password = requestBody.Credential.Password,
            PasswordSalt = requestBody.Credential.PasswordSalt,
            Username = requestBody.Credential.Username,
            UsernameSalt = requestBody.Credential.UsernameSalt,
            WebsiteUrl = requestBody.Credential.WebsiteUrl,
            WebsiteUrlSalt = requestBody.Credential.WebsiteUrlSalt,
            Note = requestBody.Credential.Note,
            NoteSalt = requestBody.Credential.NoteSalt,
            DisplayName = requestBody.Credential.DisplayName,
            DisplayNameSalt = requestBody.Credential.DisplayNameSalt,
            CreatedTimestamp = (long)DateTime.UtcNow.Subtract(DateTime.UnixEpoch).TotalSeconds,
            ChangedTimestamp = (long)DateTime.UtcNow.Subtract(DateTime.UnixEpoch).TotalSeconds,
            DeletedTimestamp = null,
        };
    }
}
