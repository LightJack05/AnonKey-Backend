using System.ComponentModel.DataAnnotations;
namespace AnonKey_Backend.Models;

/// <summary>
/// A user model to be saved in a database
/// </summary>
public class UserInfo
{
    /// <summary>
    /// The UUID of the User Info 
    /// </summary>
    [Key]
    public string? Uuid { get; set; }

    /// <summary>
    /// UUID of the associated User 
    /// </summary>
    public string? UserUuid { get; set; }

    /// <summary>
    /// The username to be diplayed
    /// </summary>
    public string? DisplayName { get; set; }
}
