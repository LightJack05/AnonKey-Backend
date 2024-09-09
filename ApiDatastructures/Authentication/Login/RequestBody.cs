namespace AnonKey_Backend.ApiDatastructures.Authentication.Login;

public class RequestBody
{
  public string UserName { get; set; }
  public string KdfPasswordResult { get; set; }
}

