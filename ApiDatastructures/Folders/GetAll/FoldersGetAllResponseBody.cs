namespace AnonKey_Backend.ApiDatastructures.Folders.GetAll;

/// <summary>
/// Response to a get all folders request.
/// </summary>
public class FoldersGetAllResponseBody
{
    /// <summary>
    /// Folders
    /// </summary>
    public List<FoldersGetAllFolder>? Folder { get; set; }
}
