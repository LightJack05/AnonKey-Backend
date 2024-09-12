namespace AnonKey_Backend.ApiDatastructures.Folders.Create;

/// <summary>
/// Folder contained inside a create folder operation.
/// </summary>
public class FoldersCreateFolder
{
    /// <summary>
    /// Name of the new folder.
    /// </summary>
    public string? Name { get; set; }
    /// <summary>
    /// Icon of the new folder.
    /// </summary>
    public int Icon { get; set; }
}
