namespace AnonKey_Backend.ApiDatastructures.Error;

/// <summary>
/// Structure of an error message.
/// </summary>
public class ErrorResponseBody
{
    /// <summary>
    /// The short message describing the error.
    /// </summary>
    public string? Message { get; set; }
    /// <summary>
    /// A detailed explanation detailing the error.
    /// </summary>
    public string? Detail { get; set; }
    /// <summary>
    /// The error code for error correction purposes.
    /// </summary>
    public int InternalCode { get; set; }
}
