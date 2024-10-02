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
        databaseHandle.Database.EnsureCreated();
        Models.User user = new()
        {
            Uuid = "7d9d3e99-064d-41bf-a125-ca5951e8a048",
            Username = "anonkey",
            PasswordSalt = Cryptography.Generators.NewRandomString(Configuration.Settings.UserPasswordSaltLength),
            PasswordHash = "I6s1E29KfokPhZ5Y9USNldmJ34f/LFsx3bBOmhkcsrg="
        };

        databaseHandle.Users.Add(user);
        databaseHandle.SaveChanges();
        databaseHandle.UserInfos.Add(new()
        {
            Uuid = Guid.NewGuid().ToString(),
            UserUuid = "7d9d3e99-064d-41bf-a125-ca5951e8a048",
            DisplayName = "AnonKey"
        });
        databaseHandle.SaveChanges();
    }

    private static void CreateSampleFolder(Data.DatabaseHandle databaseHandle)
    {
        databaseHandle.Database.EnsureCreated();
        Models.Folder folder = new()
        {
            Uuid = "09f770c5-b1c1-41c6-bfd2-818b7b443da9",
            UserUuid = "7d9d3e99-064d-41bf-a125-ca5951e8a048",
            DisplayName = "Sample",
            Icon = 58469
        };

        databaseHandle.Folders.Add(folder);
        databaseHandle.SaveChanges();
    }

    private static void CreateSampleCredentialWithoutFolder(Data.DatabaseHandle databaseHandle)
    {
        databaseHandle.Database.EnsureCreated();
        Models.Credential credential = new()
        {
            Uuid = "ebd1ef35-cade-4e2a-8117-3ed58bd13143",
            UserUuid = "7d9d3e99-064d-41bf-a125-ca5951e8a048",
            FolderUuid = null,
            Password = "oC+0SxNMjPz1PWVBgwRmDg==",
            PasswordSalt = "s8Fiylb-ogEnog==",
            Username = "1MtjUgmRffwx8fHEpSVVrw==",
            UsernameSalt = "RnrtPJWS3BlTKQ==",
            WebsiteUrl = "hBdyCOl9c4p+/1YbsmrB8u1uwoiSDKbv6Y44/VQF3wU=",
            WebsiteUrlSalt = "fTqJNLE7Mpa3FQ==",
            Note = "",
            NoteSalt = "lcR5HSDuVmuZWQ==",
            DisplayName = "bKjm+KYwFdJ7ZON7V/LaKA==",
            DisplayNameSalt = "v_rCdCzbNx6QCw==",
            CreatedTimestamp = (long)DateTime.UtcNow.Subtract(DateTime.UnixEpoch).TotalSeconds,
            ChangedTimestamp = (long)DateTime.UtcNow.Subtract(DateTime.UnixEpoch).TotalSeconds,
            DeletedTimestamp = null,
        };

        databaseHandle.Credentials.Add(credential);
        databaseHandle.SaveChanges();
    }

    private static void CreateSampleCredentialInSampleFolder(Data.DatabaseHandle databaseHandle)
    {
        databaseHandle.Database.EnsureCreated();
        Models.Credential credential = new()
        {
            Uuid = "d59bd8a5-e24b-4b97-94f5-2e3dfb9a297e",
            UserUuid = "7d9d3e99-064d-41bf-a125-ca5951e8a048",
            FolderUuid = "09f770c5-b1c1-41c6-bfd2-818b7b443da9",
            Password = "6WwpgFjvwq5LXN1UzhMA8w==",
            PasswordSalt = "0VPBOcbnFiABrQ==",
            Username = "YaqSBWUTMY+AOUjgLt43Hg==",
            UsernameSalt = "qljJ1tSLNJpTFA==",
            WebsiteUrl = "ewhprbFcbIL+RcVcTO9Ms7YIhwiWftUlAwxAIoA83yY=",
            WebsiteUrlSalt = "5ulp537sqL3ipw==",
            Note = "",
            NoteSalt = "WQbAORGrEmwmow==",
            DisplayName = "NW3YSK/R/S6JZtZBbJZZGw==",
            DisplayNameSalt = "QFQU3szsMU-K-g==",
            CreatedTimestamp = (long)DateTime.UtcNow.Subtract(DateTime.UnixEpoch).TotalSeconds,
            ChangedTimestamp = (long)DateTime.UtcNow.Subtract(DateTime.UnixEpoch).TotalSeconds,
            DeletedTimestamp = null,
        };

        databaseHandle.Credentials.Add(credential);
        databaseHandle.SaveChanges();
    }
}
