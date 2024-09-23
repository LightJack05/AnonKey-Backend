namespace AnonKey_Backend.ApiEndpoints.Credentials;

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
        NotFound<ApiDatastructures.Error.ErrorResponseBody>,
        BadRequest<ApiDatastructures.Error.ErrorResponseBody>>
            DeleteSoftUndelete(string credentialUuid, ClaimsPrincipal user, Data.DatabaseHandle databaseHandle)
    {
        databaseHandle.Database.EnsureCreated();
        if (String.IsNullOrEmpty(credentialUuid))
        {
            return TypedResults.BadRequest(new ApiDatastructures.Error.ErrorResponseBody()
            {
                Message = "A credentialUuid in the request was null or an empty string",
                Detail = "credentialUuid parameter in the request was null or an empty string. This is not allowed, please fill in all parameters.",
                InternalCode = 0x4
            });
        }

        AnonKey_Backend.Models.Credential FetchedCredential = databaseHandle.Credentials.SingleOrDefault(c => c.Uuid == credentialUuid && c.UserUuid == databaseHandle.Users.Single(u => u.Username == user.Identity.Name).Uuid && c.DeletedTimestamp != null);

        if (FetchedCredential is null)
        {
            return TypedResults.NotFound(new ApiDatastructures.Error.ErrorResponseBody()
            {
                Message = "No credential with this Uuid was found in the database or the credetial with this Uuid has not been yet soft-deleted.",
                Detail = "No credential with the provided credentialUuid was not found or the credetial with this Uuid has not been yet soft-deleted. Please make sure the correct credentialUuid is provided.",
                InternalCode = 0x6
            });
        }

        databaseHandle.Credentials.Single(c => c.Uuid == credentialUuid).DeletedTimestamp = null;
        databaseHandle.SaveChanges();
        return TypedResults.Ok();
    }
}
