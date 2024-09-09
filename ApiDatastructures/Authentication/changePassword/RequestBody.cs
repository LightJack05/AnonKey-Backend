namespace AnonKey_Backend.ApiDatastructures.Authentication.ChangePassword;

public class RequestBody
{
  public string KdfResultOldPassword { get; set; }
  public string KdfResultNewPassword { get; set; }
}

