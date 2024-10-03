namespace AnonKey_Backend.Development;
using AnonKey_Backend.Data;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Creates some sample database entries, when running in the development mode.
/// </summary>
public static class ModelBuilderExtensions
{
    /// <summary>
    /// Populates the database with the sample data for testing and development.
    /// </summary>
    public static void Seed(this ModelBuilder modelBuilder)
    {
        CreateSampleUser(modelBuilder);
        CreateSampleFolder(modelBuilder);
        CreateSampleCredentialWithoutFolder(modelBuilder);
        CreateSampleCredentialInSampleFolder(modelBuilder);
    }

    private static void CreateSampleUser(this ModelBuilder modelBuilder)
    {
        string passwordSalt = Cryptography.Generators.NewRandomString(Configuration.Settings.UserPasswordSaltLength);
        modelBuilder.Entity<AnonKey_Backend.Models.User>().HasData(new AnonKey_Backend.Models.User
        {
            Uuid = "7d9d3e99-064d-41bf-a125-ca5951e8a048",
            Username = "anonkey",
            PasswordSalt = passwordSalt,
            PasswordHash = Cryptography.PasswordHashing.HashPassword("I6s1E29KfokPhZ5Y9USNldmJ34f/LFsx3bBOmhkcsrg=", passwordSalt)
        }
        );

        modelBuilder.Entity<AnonKey_Backend.Models.UserInfo>().HasData(new AnonKey_Backend.Models.UserInfo
        {
            Uuid = Guid.NewGuid().ToString(),
            UserUuid = "7d9d3e99-064d-41bf-a125-ca5951e8a048",
            DisplayName = "AnonKey"
        });
    }

    private static void CreateSampleFolder(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AnonKey_Backend.Models.Folder>().HasData(new AnonKey_Backend.Models.Folder
        {
            Uuid = "09f770c5-b1c1-41c6-bfd2-818b7b443da9",
            UserUuid = "7d9d3e99-064d-41bf-a125-ca5951e8a048",
            DisplayName = "Sample",
            Icon = 58469
        });
    }

    private static void CreateSampleCredentialWithoutFolder(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AnonKey_Backend.Models.Credential>().HasData(new AnonKey_Backend.Models.Credential
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
        });
    }

    private static void CreateSampleCredentialInSampleFolder(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AnonKey_Backend.Models.Credential>().HasData(new AnonKey_Backend.Models.Credential
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
        });
    }
}
