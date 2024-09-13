using Newtonsoft.Json;
namespace AnonKey_Backend.Configuration;

// NOTE: This class contains public members that are prefixed with a _ and named in camelCase.
//       While not beautiful, but is required for deserialization.

/// <summary>
/// Singleton that holds settings about the server.
/// Automatically loaded and set up by static constructor.
/// </summary>
public class Settings
{
    /// <summary>
    /// Initializes the settings on first access, and saves them again should they be newly generated.
    /// </summary>
    static Settings()
    {
        ReadSettings();
        SaveSettings();
    }
    /// <summary>
    /// Constructor used for JSON loading, takes all parameters and creates a new settings object.
    /// </summary>
    [JsonConstructor]
    public Settings(byte[] _jwtIssuerSigingKey)
    {
        this._jwtIssuerSigingKey = _jwtIssuerSigingKey;
        _instance = this;
    }

    /// <summary>
    /// Initialize a new settings object with default values.
    /// </summary>
    public Settings()
    {
        _jwtIssuerSigingKey = Cryptography.Generators.NewRandomByteArray(256);
        _instance = this;
    }

    /// <summary>
    /// Instance of the settings class.
    /// </summary>
    private static Settings? _instance;

    /// <summary>
    /// The set length for the JWT signing key.
    /// </summary>
    private const int JwtSigingKeyLength = 256;



    /// <summary>
    /// The key used for signing json web tokens.
    /// </summary>
    public static byte[] JwtIssuerSigningKey { get => _instance._jwtIssuerSigingKey; private set => _instance._jwtIssuerSigingKey = value; }
    public byte[] _jwtIssuerSigingKey { get; private set; }



    /// <summary>
    /// Read the settings from the settings.json file and put them into the _instance variable.
    /// Create a default instance if the file can't be read.
    /// </summary>
    public static void ReadSettings()
    {
        if (System.IO.File.Exists("settings.json"))
        {
            using (System.IO.StreamReader streamReader = new System.IO.StreamReader("settings.json"))
            {
                try
                {
                    string fileContent = streamReader.ReadToEnd();
                    Settings? settingsObject = JsonConvert.DeserializeObject<Settings>(fileContent);
                    _instance = isSettingsIntegrityOk(settingsObject) ? new() : settingsObject;
                }
                catch (Newtonsoft.Json.JsonException)
                {
                    _instance = new();
                }
            }
        }
        else
        {
            _instance = new();
        }
    }

    /// <summary>
    /// Save settings to the settings.json file.
    /// </summary>
    public static void SaveSettings()
    {
        using (System.IO.StreamWriter streamWriter = new("settings.json"))
        {
            string jsonString = JsonConvert.SerializeObject(_instance);
            streamWriter.Write(jsonString);
        }
    }

    private static bool isSettingsIntegrityOk(Settings? settings)
    {
        return !(
            settings != null &&
            settings._jwtIssuerSigingKey != null &&
            settings._jwtIssuerSigingKey.Length == JwtSigingKeyLength
            );

    }
}
