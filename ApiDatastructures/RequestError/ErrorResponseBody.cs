namespace AnonKeyBackend.ApiDatastructures.RequestError;

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
}
