namespace AnonKey_Backend.ApiEndpoints.Folders;

public static class Update
{
    public static Microsoft.AspNetCore.Http.HttpResults.Results<
        Ok<ApiDatastructures.Folder.Update.ResponseBody>,
        BadRequest<ApiDatastructures.Error.ErrorResponseBody>>
            PutUpdate(ApiDatastructures.Folder.Update.RequestBody requestBody)
    {
        throw new NotImplementedException();
        
    }
}
