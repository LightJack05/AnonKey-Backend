namespace AnonKey_Backend.ApiDatastructures.Authentication.ChangePassword;

public class AuthenticationChangePasswordRequestBody
{
  public string KdfResultOldPassword { get; set; }
  public string KdfResultNewPassword { get; set; }
}

