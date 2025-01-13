namespace AnonKeyBackend.ApiDatastructures.Folders.GetFolder;

/// <summary>
/// Response to a folder get request.
/// </summary>
public class FoldersGetResponseBody
{
    /// <summary>
    /// The requested folder.
    /// </summary>
    public FoldersGetFolder? Folder { get; set; }
}
