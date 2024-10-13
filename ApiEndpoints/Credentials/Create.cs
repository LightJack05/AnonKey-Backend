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
                Detail = "The user identity is null. Did you provide a valid JWT token?"
            });
        }
        if (requestBody.Credential is null)
        {
            return TypedResults.BadRequest(new ApiDatastructures.Error.ErrorResponseBody()
            {
                Message = "The credential provided in the requestBody is null",
                Detail = "The credential provided in the requestBody is null. This is not allowed, please fill in all parameters."
            });
        }
        if (requestBody.Credential is null || String.IsNullOrEmpty(requestBody.Credential.Uuid) || String.IsNullOrEmpty(requestBody.Credential.DisplayName) || String.IsNullOrEmpty(requestBody.Credential.DisplayNameSalt))
        {
            return TypedResults.BadRequest(new ApiDatastructures.Error.ErrorResponseBody()
            {
                Message = "A parameter in the request was null or an empty string",
                Detail = "One of the parameters in the request was null or an empty string. This is not allowed, please fill in all parameters."
            });
        }

        // If some fields are not null, check for the salt to be there
        if (!String.IsNullOrEmpty(requestBody.Credential.WebsiteUrl) && String.IsNullOrEmpty(requestBody.Credential.WebsiteUrlSalt))
        {
            return TypedResults.BadRequest(new ApiDatastructures.Error.ErrorResponseBody()
            {
                Message = "WebsiteUrl was provided, but the salt for it was not.",
                Detail = "WebsiteUrlSalt is null or an empty string, but WebsiteUrl is not null. This is not allowed, please fill in all parameters."
            });
        }

        if (!String.IsNullOrEmpty(requestBody.Credential.Username) && String.IsNullOrEmpty(requestBody.Credential.UsernameSalt))
        {
            return TypedResults.BadRequest(new ApiDatastructures.Error.ErrorResponseBody()
            {
                Message = "Username was provided, but the salt for it was not.",
                Detail = "UsernameSalt is null or an empty string, but Username is not null. This is not allowed, please fill in all parameters."
            });
        }

        if (!String.IsNullOrEmpty(requestBody.Credential.Password) && String.IsNullOrEmpty(requestBody.Credential.PasswordSalt))
        {
            return TypedResults.BadRequest(new ApiDatastructures.Error.ErrorResponseBody()
            {
                Message = "Password was provided, but the salt for it was not.",
                Detail = "PasswordSalt is null or an empty string, but Password is not null. This is not allowed, please fill in all parameters."
            });
        }

        if (!String.IsNullOrEmpty(requestBody.Credential.Note) && String.IsNullOrEmpty(requestBody.Credential.NoteSalt))
        {
            return TypedResults.BadRequest(new ApiDatastructures.Error.ErrorResponseBody()
            {
                Message = "Note was provided, but the salt for it was not.",
                Detail = "NoteSalt is null or an empty string, but Note is not null. This is not allowed, please fill in all parameters."
            });
        }

        if (databaseHandle.Credentials.Any(c => c.Uuid == requestBody.Credential.Uuid))
        {
            return TypedResults.Conflict(new ApiDatastructures.Error.ErrorResponseBody()
            {
                Message = "A credential with this uuid already exists.",
                Detail = "There is already a user object for the given uuid in the database. Please try changing the uuid and resending the request."
            });
        }

        Models.Credential NewCredential = CreateNewCredential(requestBody, user, databaseHandle);

        databaseHandle.Credentials.Add(NewCredential);
        databaseHandle.SaveChanges();
        return TypedResults.Ok();
    }

    private static Models.Credential CreateNewCredential(ApiDatastructures.Credentials.Create.CredentialsCreateRequestBody requestBody, ClaimsPrincipal user, Data.DatabaseHandle databaseHandle)
    {
        if (user.Identity is null || requestBody.Credential is null) throw new ArgumentNullException();
        AnonKey_Backend.Models.User? FetchedUser = databaseHandle.Users.SingleOrDefault(u => u.Username == user.Identity.Name);
        if (FetchedUser is null) throw new ArgumentNullException();
        return new Models.Credential()
        {
            Uuid = requestBody.Credential.Uuid,
            UserUuid = FetchedUser.Uuid,
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
