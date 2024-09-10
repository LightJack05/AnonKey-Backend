namespace AnonKey_Backend.ApiEndpoints.Folders;

public static class GetAll
{
    public static Microsoft.AspNetCore.Http.HttpResults.Results<
        Ok<ApiDatastructures.Folder.GetAll.ResponseBody>,
        BadRequest<ApiDatastructures.Error.ErrorResponseBody>>
            GetAllGet()
    {
        throw new NotImplementedException();
        
    }
}
