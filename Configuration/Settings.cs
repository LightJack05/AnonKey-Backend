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
    #region setup
    /// <summary>
    /// Initializes the settings on first access, and saves them again should they be newly generated.
    /// </summary>
    static Settings()
    {
        _instance = ReadSettings();
        SaveSettings();
    }
    /// <summary>
    /// Constructor used for JSON loading, takes all parameters and creates a new settings object.
    /// </summary>
    [JsonConstructor]
    public Settings(byte[] _jwtIssuerSigingKey, string _serverUserPasswordPepper)
    {
        this._jwtIssuerSigingKey = _jwtIssuerSigingKey;
        this._serverUserPasswordPepper = _serverUserPasswordPepper;
        _instance = this;
    }

    /// <summary>
    /// Initialize a new settings object with default values.
    /// </summary>
    public Settings()
    {
        _jwtIssuerSigingKey = Cryptography.Generators.NewRandomByteArray(JwtSigingKeyLength);
        _serverUserPasswordPepper = Cryptography.Generators.NewRandomString(ServerUserPasswordPepperLength);
        _instance = this;
    }

    /// <summary>
    /// Instance of the settings class.
    /// </summary>
    private static Settings _instance;

    #endregion

    #region constants

    /// <summary>
    /// The set length for the JWT signing key.
    /// </summary>
    public const int JwtSigingKeyLength = 256;
    /// <summary>
    /// The length of the pepper applied to user passwords.
    /// </summary>
    public const int ServerUserPasswordPepperLength = 32;
    /// <summary>
    /// The length of the salt applied to user passwords.
    /// </summary>
    public const int UserPasswordSaltLength = 32;
    

    #endregion

    #region settings

    /// <summary>
    /// The key used for signing json web tokens.
    /// </summary>
    public static byte[] JwtIssuerSigningKey { get => _instance._jwtIssuerSigingKey; private set => _instance._jwtIssuerSigingKey = value; }
    /// <summary>
    /// The background property for the jwt signing key.
    /// Should not be directly accessed!
    /// Use <c cref="JwtIssuerSigningKey">Settings.JwtIssuerSigningKey</c> instead!
    /// </summary>
    public byte[] _jwtIssuerSigingKey { get; private set; }

    /// <summary>
    /// The pepper applied to user passwords universally on this server.
    /// </summary>
    public static string ServerUserPasswordPepper { get => _instance._serverUserPasswordPepper; set => _instance._serverUserPasswordPepper = value; }
    /// <summary>
    /// The pepper applied to user passwords universally on this server.
    /// Should not be directly accessed!
    /// Use <c cref="ServerUserPasswordPepper">Settings.ServerUserPasswordPepper</c> instead!
    /// </summary>
    public string _serverUserPasswordPepper { get; private set; }


    #endregion
    #region actions

    /// <summary>
    /// Read the settings from the settings.json file and place them into the singleton instance variable.
    /// Create a default instance if the file can't be read.
    /// </summary>
    public static void LoadSettings()
    {
        _instance = ReadSettings();
    }

    /// <summary>
    /// Read the settings from the settings.json file.
    /// Create a default instance if the file can't be read.
    /// </summary>
    private static Settings ReadSettings()
    {
        Settings? settingsObject;
        if (System.IO.File.Exists("settings.json"))
        {
            using (System.IO.StreamReader streamReader = new System.IO.StreamReader("settings.json"))
            {
                try
                {
                    string fileContent = streamReader.ReadToEnd();
                    settingsObject = JsonConvert.DeserializeObject<Settings>(fileContent);
                    if (settingsObject is null) return new();
                    return isSettingsIntegrityOk(settingsObject) ? new() : settingsObject;
                }
                catch (Newtonsoft.Json.JsonException)
                {
                    settingsObject = new();
                }
            }
        }
        else
        {
            settingsObject = new();
        }
        return settingsObject;
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

    private static bool isSettingsIntegrityOk(Settings settings)
    {
        return !(
            settings._jwtIssuerSigingKey != null &&
            settings._jwtIssuerSigingKey.Length == JwtSigingKeyLength &&
            settings._serverUserPasswordPepper != null &&
            settings._serverUserPasswordPepper.Length == ServerUserPasswordPepperLength
            );

    }
    #endregion
}
