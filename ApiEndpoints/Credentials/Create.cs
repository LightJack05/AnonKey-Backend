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

        AnonKey_Backend.Models.Credential NewCredential = new Models.Credential()
        {
            // Not sure about the Uuids, in the comments of RequsetBody it is written, the the Uuid is the Uuid of the Credential, but I supose it should be the Uuid of the user
            Uuid = Guid.NewGuid().ToString(),
            UserUuid = requestBody.Credential.Uuid,
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
