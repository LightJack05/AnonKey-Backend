namespace AnonKeyBackend.ApiDatastructures.Folders.Get;

/// <summary>
/// Folder in a folder get response.
/// </summary>
public class FoldersGetFolder
{
    /// <summary>
    /// UUID of the folder.
    /// </summary>
    public string? Uuid { get; set; }
    /// <summary>
    /// Name of the folder.
    /// </summary>
    public string? Name { get; set; }
    /// <summary>
    /// Icon of the folder.
    /// </summary>
    public int Icon { get; set; }
}
