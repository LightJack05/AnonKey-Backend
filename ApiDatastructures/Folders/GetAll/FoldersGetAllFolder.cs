namespace AnonKeyBackend.ApiDatastructures.Folders.GetAll;

/// <summary>
/// Folder in a response to a get all folders request.
/// </summary>
public class FoldersGetAllFolder
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
