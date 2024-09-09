namespace AnonKey_Backend.ApiDatastructures.Users;

public class User
{
  public string UserUuid { set; get; }
  public string DisplayName { set; get; }

  public User(string displayName)
  {
    DisplayName = displayName;
  }
}

