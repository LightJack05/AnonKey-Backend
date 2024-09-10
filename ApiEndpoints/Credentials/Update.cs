namespace AnonKey_Backend.ApiEndpoints.Credentials;

public static class Update
{
    
    public static Microsoft.AspNetCore.Http.HttpResults.Results<
        Ok<ApiDatastructures.Credentials.Update.ResponseBody>,
        BadRequest<ApiDatastructures.Error.ErrorResponseBody>>
            PutUpdate(ApiDatastructures.Credentials.Update.RequestBody requestBody)
    {
        throw new NotImplementedException(); 
    }
}
