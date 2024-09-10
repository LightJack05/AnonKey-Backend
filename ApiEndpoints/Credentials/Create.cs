namespace AnonKey_Backend.ApiEndpoints.Credentials;

public static class Create
{

    public static Microsoft.AspNetCore.Http.HttpResults.Results<
        Ok<ApiDatastructures.Credentials.Create.ResponseBody>,
        BadRequest<ApiDatastructures.Error.ErrorResponseBody>>
     PostCreate(ApiDatastructures.Credentials.Create.RequestBody requestBody)
    {
        throw new NotImplementedException(); 
    }
}
