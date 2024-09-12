namespace AnonKey_Backend.ApiEndpoints.Uuid;

public static class NewUuid
{

    /// <summary>
    /// Returns a new UUID for several endpoints.
    /// </summary>
    public static Microsoft.AspNetCore.Http.HttpResults.Results<
        Ok<string>,
        BadRequest<AnonKey_Backend.ApiDatastructures.Error.ErrorResponseBody>>
            GetNewUuid()
    {
        Guid newGuid = System.Guid.NewGuid();
        return TypedResults.Ok(newGuid.ToString());
    }
}
