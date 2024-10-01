namespace AnonKey_Backend.Development;
using AnonKey_Backend.Data;

/// <summary>
/// Creates some sample database entries, when running in the development mode.
/// </summary>
public class SampleData
{
    /// <summary>
    /// Populates the database with the sample data for testing and development.
    /// </summary>
    public static void PopulateDatabase(Data.DatabaseHandle databaseHandle)
    {
        CreateSampleUser(databaseHandle);
        CreateSampleFolder(databaseHandle);
        CreateSampleCredentialWithoutFolder(databaseHandle);
        CreateSampleCredentialInSampleFolder(databaseHandle);

    }

    private static void CreateSampleUser(Data.DatabaseHandle databaseHandle)
    {
        throw new NotImplementedException();
    }

    private static void CreateSampleFolder(Data.DatabaseHandle databaseHandle)
    {
        throw new NotImplementedException();
    }

    private static void CreateSampleCredentialWithoutFolder(Data.DatabaseHandle databaseHandle)
    {
        throw new NotImplementedException();
    }

    private static void CreateSampleCredentialInSampleFolder(Data.DatabaseHandle databaseHandle)
    {
        throw new NotImplementedException();
    }
}
