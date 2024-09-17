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

        if (databaseHandle.Credentials.Any(c => c.Uuid == requestBody.Credential.Uuid))
        {
            return TypedResults.Conflict(new ApiDatastructures.Error.ErrorResponseBody()
            {
                Message = "A credential with this uuid already exists.",
                Detail = "There is already a user object for the given uuid in the database. Please try changing the uuid and resending the request.",
                InternalCode = 0x2
            });
        }

        AnonKey_Backend.Models.Credential NewCredential = new Models.Credential()
        {
            Uuid = requestBody.Credential.Uuid,
            UserUuid = databaseHandle.Users.Where(u => u.Username == user.Identity.Name).First().Uuid,
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

        databaseHandle.Credentials.Add(NewCredential);
        databaseHandle.SaveChanges();
        return TypedResults.Ok();
    }
}
