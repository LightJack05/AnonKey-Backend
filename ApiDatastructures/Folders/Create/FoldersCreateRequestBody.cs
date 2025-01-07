namespace AnonKeyBackend.ApiDatastructures.Folders.Create;

/// <summary>
/// Body of a folder create request.
/// </summary>
public class FoldersCreateRequestBody
{
    /// <summary>
    /// Folder to create.
    /// </summary>
    public FoldersCreateFolder? Folder { get; set; }
}
