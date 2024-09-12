namespace AnonKey_Backend.ApiDatastructures.Folders.Get;

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
