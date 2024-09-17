namespace AnonKey_Backend.ApiEndpoints.Credentials;

/// <summary>
/// Handles the credentials get all endpoint.
/// </summary>
public static class GetAll
{

    /// <summary>
    /// Gets all available credential objects for this user.
    /// </summary>
    public static Microsoft.AspNetCore.Http.HttpResults.Results<
        Ok<ApiDatastructures.Credentials.GetAll.CredentialsGetAllResponseBody>,
        NotFound<ApiDatastructures.Error.ErrorResponseBody>,
        BadRequest<ApiDatastructures.Error.ErrorResponseBody>>
            GetGetAll(ClaimsPrincipal user, Data.DatabaseHandle databaseHandle)
    {
        databaseHandle.Database.EnsureCreated();
        if (user is null)
        {
            return TypedResults.BadRequest(new ApiDatastructures.Error.ErrorResponseBody()
            {
                Message = "The user is null",
                Detail = "The user is null, therefore no data extraction is possible",
                InternalCode = 0x4
            });
        }
        if (!databaseHandle.Credentials.Any(c => c.UserUuid == databaseHandle.Users.SingleOrDefault(u => u.Username == user.Identity.Name).Uuid))
        {
            return TypedResults.NotFound(new ApiDatastructures.Error.ErrorResponseBody()
            {
                Message = "User has no credentials yet",
                Detail = "There are no data in the credentials table in the databes associated with a given user.",
                InternalCode = 0x6
            });
        }
        List<AnonKey_Backend.Models.Credential> FetchedCredetials = databaseHandle.Credentials.Where(c => c.UserUuid == databaseHandle.Users.SingleOrDefault(u => u.Username == user.Identity.Name).Uuid).ToList();
        AnonKey_Backend.ApiDatastructures.Credentials.GetAll.CredentialsGetAllResponseBody Result = new AnonKey_Backend.ApiDatastructures.Credentials.GetAll.CredentialsGetAllResponseBody();
        Result.Credentials = new List<AnonKey_Backend.ApiDatastructures.Credentials.GetAll.CredentialsGetAllCredential>();
        foreach (var FetchedCredential in FetchedCredetials)
        {
            Result.Credentials.Add(new AnonKey_Backend.ApiDatastructures.Credentials.GetAll.CredentialsGetAllCredential()
            {
                Uuid = FetchedCredential.Uuid,
                Password = FetchedCredential.Password,
                PasswordSalt = FetchedCredential.PasswordSalt,
                Username = FetchedCredential.Username,
                UsernameSalt = FetchedCredential.UsernameSalt,
                WebsiteUrl = FetchedCredential.WebsiteUrl,
                Note = FetchedCredential.Note,
                DisplayName = FetchedCredential.DisplayName,
                FolderUuid = FetchedCredential.FolderUuid,
                CreatedTimestamp = FetchedCredential.CreatedTimestamp,
                ChangedTimestamp = FetchedCredential.ChangedTimestamp,
                DeletedTimestamp = FetchedCredential.DeletedTimestamp
            });
        }
        return TypedResults.Ok(Result);
    }
}
