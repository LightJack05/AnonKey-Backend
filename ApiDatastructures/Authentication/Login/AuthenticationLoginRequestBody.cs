namespace AnonKey_Backend.ApiDatastructures.Authentication.Login;

public class AuthenticationLoginRequestBody
{
  public string UserName { get; set; }
  public string KdfPasswordResult { get; set; }
}

