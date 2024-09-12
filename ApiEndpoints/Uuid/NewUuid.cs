namespace AnonKey_Backend.ApiEndpoints.Uuid;

public static class NewUuid
{

    public static Microsoft.AspNetCore.Http.HttpResults.Results<
        Ok<string>,
        BadRequest<AnonKey_Backend.ApiDatastructures.Error.ErrorResponseBody>>
            GetNewUuid()
    {
        Guid newGuid = System.Guid.NewGuid();
        return TypedResults.Ok(newGuid.ToString());
    }
}
