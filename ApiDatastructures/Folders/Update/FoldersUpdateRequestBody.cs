namespace AnonKey_Backend.ApiDatastructures.Folders.Update;

/// <summary>
/// The body of a folder update request.
/// </summary>
public class FoldersUpdateRequestBody
{
    /// <summary>
    /// The folder to update.
    /// </summary>
    public FoldersUpdateFolder? Folder { get; set; }
}
