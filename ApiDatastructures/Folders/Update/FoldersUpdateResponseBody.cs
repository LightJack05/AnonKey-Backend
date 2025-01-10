namespace AnonKeyBackend.ApiDatastructures.Folders.Update;

/// <summary>
/// Body of a response to a folder update request.
/// </summary>
public class FoldersUpdateResponseBody
{
    /// <summary>
    /// UUID of the updated folder.
    /// </summary>
    public string? FolderUuid { get; set; }
}
