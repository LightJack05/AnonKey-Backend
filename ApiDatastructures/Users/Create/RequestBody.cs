namespace AnonKey_Backend.ApiDatastructures.Users.Create;

public class RequestBody
{
  public string UserName { get; set; }
  public string UserDisplayName { get; set; }
  public string KdfPasswordResult { get; set; }
}
