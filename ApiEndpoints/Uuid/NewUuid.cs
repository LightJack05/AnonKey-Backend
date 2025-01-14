namespace AnonKeyBackend.ApiEndpoints.Uuid;

/// <summary>
/// Handles the new uuid new endpoint.
/// </summary>
public static class NewUuid
{

    /// <summary>
    /// Returns a new UUID for several endpoints.
    /// </summary>
    public static Microsoft.AspNetCore.Http.HttpResults.Results<
        Ok<string>,
        BadRequest<AnonKeyBackend.ApiDatastructures.RequestError.ErrorResponseBody>,
        UnauthorizedHttpResult>
            GetNewUuid(ClaimsPrincipal user, Data.DatabaseHandle databaseHandle)
    {
        if (!AnonKeyBackend.Authentication.TokenActions.ValidateClaimsOnRequest(user, databaseHandle))
        {
            return TypedResults.Unauthorized();
        }
        Guid newGuid = System.Guid.NewGuid();
        return TypedResults.Ok(newGuid.ToString());
    }
}
