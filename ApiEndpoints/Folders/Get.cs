namespace AnonKey_Backend.ApiEndpoints.Folders;

public static class Get
{
    
    public static Microsoft.AspNetCore.Http.HttpResults.Results<
        Ok<ApiDatastructures.Folder.Get.ResponseBody>,
        BadRequest<ApiDatastructures.Error.ErrorResponseBody>>
            GetGet()
    {
       throw new NotImplementedException(); 
    }
}
