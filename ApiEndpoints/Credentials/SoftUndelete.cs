namespace AnonKeyBackend.ApiEndpoints.Credentials;

/// <summary>
/// Handles the credentials soft-undelete endpoint.
/// </summary>
public static class SoftUndelete
{
    /// <summary>
    /// SoftUndeletes an existing credential object.
    /// </summary>
    public static Microsoft.AspNetCore.Http.HttpResults.Results<
        Ok,
        NotFound<ApiDatastructures.RequestError.ErrorResponseBody>,
        BadRequest<ApiDatastructures.RequestError.ErrorResponseBody>,
        UnauthorizedHttpResult>
            PutSoftUndelete(string credentialUuid, ClaimsPrincipal user, Data.DatabaseHandle databaseHandle)
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
        if (String.IsNullOrEmpty(credentialUuid))
        {
            return TypedResults.BadRequest(new ApiDatastructures.RequestError.ErrorResponseBody()
            {
                Message = "A credentialUuid in the request was null or an empty string",
                Detail = "credentialUuid parameter in the request was null or an empty string. This is not allowed, please fill in all parameters."
            });
        }

        AnonKeyBackend.Models.Credential? FetchedCredential = databaseHandle.Credentials.SingleOrDefault(c => c.Uuid == credentialUuid && c.UserUuid == databaseHandle.Users.Single(u => u.Username == user.Identity.Name).Uuid && c.DeletedTimestamp != null);

        if (FetchedCredential is null)
        {
            return TypedResults.NotFound(new ApiDatastructures.RequestError.ErrorResponseBody()
            {
                Message = "No credential with this Uuid was found in the database or the credetial with this Uuid has not been yet soft-deleted.",
                Detail = "No credential with the provided credentialUuid was not found or the credetial with this Uuid has not been yet soft-deleted. Please make sure the correct credentialUuid is provided."
            });
        }

        databaseHandle.Credentials.Single(c => c.Uuid == credentialUuid).DeletedTimestamp = null;
        databaseHandle.SaveChanges();
        return TypedResults.Ok();
    }
}
