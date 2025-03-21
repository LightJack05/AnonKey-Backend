using AnonKeyBackend.ApiDatastructures.Credentials.GetAll;
using AnonKeyBackend.Data;

namespace AnonKeyBackend.ApiEndpoints.Credentials;

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
        NotFound<ApiDatastructures.RequestError.ErrorResponseBody>,
        BadRequest<ApiDatastructures.RequestError.ErrorResponseBody>,
        UnauthorizedHttpResult>
            GetGetAll(ClaimsPrincipal user, Data.DatabaseHandle databaseHandle)
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

        CredentialsGetAllResponseBody Result = GetAllCredetials(user.Identity.Name, databaseHandle);
        return TypedResults.Ok(Result);
    }

    private static CredentialsGetAllResponseBody GetAllCredetials(string? username, DatabaseHandle databaseHandle)
    {
        ArgumentNullException.ThrowIfNull(username);
        AnonKeyBackend.Models.User? FetchedUser = databaseHandle.Users.SingleOrDefault(u => u.Username == username);
        ArgumentNullException.ThrowIfNull(FetchedUser);
        List<AnonKeyBackend.Models.Credential> FetchedCredetials = databaseHandle.Credentials.Where(c => c.UserUuid == FetchedUser.Uuid).ToList();
        AnonKeyBackend.ApiDatastructures.Credentials.GetAll.CredentialsGetAllResponseBody Result = new AnonKeyBackend.ApiDatastructures.Credentials.GetAll.CredentialsGetAllResponseBody();
        Result.Credentials = new List<AnonKeyBackend.ApiDatastructures.Credentials.GetAll.CredentialsGetAllCredential>();
        foreach (AnonKeyBackend.Models.Credential FetchedCredential in FetchedCredetials)
        {
            Result.Credentials.Add(new AnonKeyBackend.ApiDatastructures.Credentials.GetAll.CredentialsGetAllCredential()
            {
                Uuid = FetchedCredential.Uuid,
                Password = FetchedCredential.Password,
                PasswordSalt = FetchedCredential.PasswordSalt,
                Username = FetchedCredential.Username,
                UsernameSalt = FetchedCredential.UsernameSalt,
                WebsiteUrl = FetchedCredential.WebsiteUrl,
                WebsiteUrlSalt = FetchedCredential.WebsiteUrlSalt,
                Note = FetchedCredential.Note,
                NoteSalt = FetchedCredential.NoteSalt,
                DisplayName = FetchedCredential.DisplayName,
                DisplayNameSalt = FetchedCredential.DisplayNameSalt,
                FolderUuid = FetchedCredential.FolderUuid,
                CreatedTimestamp = FetchedCredential.CreatedTimestamp,
                ChangedTimestamp = FetchedCredential.ChangedTimestamp,
                DeletedTimestamp = FetchedCredential.DeletedTimestamp
            });
        }

        return Result;
    }
}
