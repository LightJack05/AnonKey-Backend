namespace AnonKey_Backend.ApiEndpoints.Folders;

public static class Create
{

    public static Microsoft.AspNetCore.Http.HttpResults.Results<
        Ok<ApiDatastructures.Folder.Create.ResponseBody>,
        BadRequest<ApiDatastructures.Error.ErrorResponseBody>>
            PostCreate(ApiDatastructures.Folder.Create.RequestBody requestBody)
    {
        throw new NotImplementedException();
    }
}
