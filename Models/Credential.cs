using System.ComponentModel.DataAnnotations;
namespace AnonKeyBackend.Models;

/// <summary>
/// A credetial model to be saved in a database
/// </summary>
public class Credential
{

    /// <summary>
    /// The UUID of the credential
    /// </summary>
    [Key]
    public string? Uuid { get; set; }

    /// <summary>
    /// UUID of the associated User 
    /// </summary>
    public string? UserUuid { get; set; }

    /// <summary>
    /// The FolderUuid of the credetial
    /// </summary>
    public string? FolderUuid { get; set; }

    /// <summary>
    /// The password of the credetial
    /// </summary>
    public string? Password { get; set; }

    /// <summary>
    /// The PasswordSalt of the credetial
    /// </summary>
    public string? PasswordSalt { get; set; }

    /// <summary>
    /// The Username of the credetial
    /// </summary>
    public string? Username { get; set; }

    /// <summary>
    /// The UsernameSalt of the credetial
    /// </summary>
    public string? UsernameSalt { get; set; }

    /// <summary>
    /// The WebsiteUrl of the credetial
    /// </summary>
    public string? WebsiteUrl { get; set; }

    /// <summary>
    /// The WebsiteUrlSalt of the credetial
    /// </summary>
    public string? WebsiteUrlSalt { get; set; }

    /// <summary>
    /// The Note of the credetial
    /// </summary>
    public string? Note { get; set; }

    /// <summary>
    /// The NoteSalt  of the credetial
    /// </summary>
    public string? NoteSalt { get; set; }

    /// <summary>
    /// The DisplayName of the credetial
    /// </summary>
    public string? DisplayName { get; set; }

    /// <summary>
    /// The DisplayNameSalt of the credetial
    /// </summary>
    public string? DisplayNameSalt { get; set; }

    /// <summary>
    /// The CreatedTimestamp of the credetial
    /// </summary>
    public long CreatedTimestamp { get; set; }

    /// <summary>
    /// The ChangedTimestamp of the credetial
    /// </summary>
    public long ChangedTimestamp { get; set; }

    /// <summary>
    /// The DeletedTimestamp of the credetial
    /// </summary>
    public long? DeletedTimestamp { get; set; }
}
