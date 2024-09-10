namespace AnonKey_Backend.ApiEndpoints.Credentials;

public static class GetAll
{
    
    public static Microsoft.AspNetCore.Http.HttpResults.Results<
        Ok<ApiDatastructures.Credentials.GetAll.ResponseBody>,
        BadRequest<ApiDatastructures.Error.ErrorResponseBody>>
            GetAllGet()
    {
        throw new NotImplementedException();    
    }
}
