namespace AnonKey_Backend.ApiDatastructures.Users.Request;

public class UserCreateRequestBody
{
  public string UserName { get; set; }
  public string UserDisplayName { get; set; }
  public string KdfPasswordResult { get; set; }
}
