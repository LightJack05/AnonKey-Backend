namespace AnonKey_Backend.ApiDatastructures.Folders.Create;

/// <summary>
/// Response to a folder create request.
/// </summary>
public class FoldersCreateResponseBody
{
    /// <summary>
    /// The UUID of the newly created folder.
    /// </summary>
    public string? FolderUuid { get; set; }
}
