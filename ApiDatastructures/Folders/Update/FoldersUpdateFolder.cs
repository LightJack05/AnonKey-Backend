namespace AnonKey_Backend.ApiDatastructures.Folders.Update;

/// <summary>
/// Folder returned by the update request.
/// </summary>
public class FoldersUpdateFolder
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
