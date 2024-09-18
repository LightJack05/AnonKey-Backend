namespace AnonKey_Backend.ApiEndpoints.Credentials;

/// <summary>
/// Handles the credentials delete endpoint.
/// </summary>
public static class Delete
{
    /// <summary>
    /// Deletes an existing credential object.
    /// </summary>
    public static Microsoft.AspNetCore.Http.HttpResults.Results<
        Ok,
        NotFound<ApiDatastructures.Error.ErrorResponseBody>,
        BadRequest<ApiDatastructures.Error.ErrorResponseBody>>
            DeleteDelete(string credentialUuid, ClaimsPrincipal user, Data.DatabaseHandle databaseHandle)
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


        if (!databaseHandle.Credentials.Any(c => c.Uuid == credentialUuid))
        {
            return TypedResults.NotFound(new ApiDatastructures.Error.ErrorResponseBody()
            {
                Message = "No credential with this Uuid was not found in the database",
                Detail = "No credential with the provided credentialUuid was not found. Please make sure the correct credentialUuid is provided.",
                InternalCode = 0x6
            });
        }

        if (databaseHandle.Users.Single(u => u.Username == user.Identity.Name).Uuid != databaseHandle.Credentials.Single(c => c.Uuid == credentialUuid).UserUuid)
        {
            return TypedResults.BadRequest(new ApiDatastructures.Error.ErrorResponseBody()
            {
                Message = "This user does not have access to this credential",
                Detail = "The credential with the provided uuid does not belong to this user. This is not allowed, please make sure to provide a valid input.",
                InternalCode = 0x7
            });
        }
        databaseHandle.Credentials.Single(c => c.Uuid == credentialUuid).DeletedTimestamp = (long)DateTime.UtcNow.Subtract(DateTime.UnixEpoch).TotalSeconds;
        databaseHandle.SaveChanges();
        return TypedResults.Ok();
    }
}
