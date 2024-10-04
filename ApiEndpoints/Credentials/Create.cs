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
        if (requestBody.Credential is null)
        {
            return TypedResults.BadRequest(new ApiDatastructures.Error.ErrorResponseBody()
            {
                Message = "The credential provieded in the requestBody is null",
                Detail = "The credential provieded in the requestBody is null. This is not allowed, please fill in all parameters.",
                InternalCode = 0x4
            });
        }
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
