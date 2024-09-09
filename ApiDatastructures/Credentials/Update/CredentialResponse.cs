namespace AnonKey_Backend.ApiDatastructures.Credentials.Update;

public class CredentialResponse
{
  public string Uuid { get; set; }
  public string Password { get; set; }
  public string PasswordSalt { get; set; }
  public string Username { get; set; }
  public string UsernameSalt { get; set; }
  public string WebsiteUrl { get; set; }
  public string Note { get; set; }
  public string DisplayName { get; set; }
  public string FolderUuid { get; set; }
  public long CreatedTimestamp { get; set; }
  public long ChangedTimestamp { get; set; }
  public long DeletedTimestamp { get; set; }
}
