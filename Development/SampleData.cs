namespace AnonKeyBackend.Development;
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
        modelBuilder.Entity<AnonKeyBackend.Models.User>().HasData(new AnonKeyBackend.Models.User
        {
            Uuid = "7d9d3e99-064d-41bf-a125-ca5951e8a048",
            Username = "anonkey",
            PasswordSalt = passwordSalt,
            PasswordHash = Cryptography.PasswordHashing.HashPassword("iWhH930n2YxEYnGSXr71Lz7U20zu45A//I13Sw3xxrY=", passwordSalt)
        }
        );

        modelBuilder.Entity<AnonKeyBackend.Models.UserInfo>().HasData(new AnonKeyBackend.Models.UserInfo
        {
            Uuid = Guid.NewGuid().ToString(),
            UserUuid = "7d9d3e99-064d-41bf-a125-ca5951e8a048",
            DisplayName = "AnonKey"
        });
    }

    private static void CreateSampleFolder(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AnonKeyBackend.Models.Folder>().HasData(new AnonKeyBackend.Models.Folder
        {
            Uuid = "09f770c5-b1c1-41c6-bfd2-818b7b443da9",
            UserUuid = "7d9d3e99-064d-41bf-a125-ca5951e8a048",
            DisplayName = "Sample",
            Icon = 58469
        });
    }

    private static void CreateSampleCredentialWithoutFolder(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AnonKeyBackend.Models.Credential>().HasData(new AnonKeyBackend.Models.Credential
        {
            Uuid = "ebd1ef35-cade-4e2a-8117-3ed58bd13143",
            UserUuid = "7d9d3e99-064d-41bf-a125-ca5951e8a048",
            FolderUuid = null,
            Password = "XkL5s6BDUY14de3BW5IcDQ==",
            PasswordSalt = "TrVCY_S3SGa5XA==",
            Username = "is4zOsVVvc4P/gdbyzeAlA==",
            UsernameSalt = "y8MA8gqAo7Bm6A==",
            WebsiteUrl = "ApnCDbm6MJVWjzDIvTVGemA1E8hElMm2cFOwLroVTOY=",
            WebsiteUrlSalt = "QEd2OprWNj__8g==",
            Note = "",
            NoteSalt = "qVeBg5clsp1auw==",
            DisplayName = "xFW26xJ+GvhxerJPEqfGBw==",
            DisplayNameSalt = "FkC3woj-MPraog==",
            CreatedTimestamp = (long)DateTime.UtcNow.Subtract(DateTime.UnixEpoch).TotalSeconds, 
            ChangedTimestamp = (long)DateTime.UtcNow.Subtract(DateTime.UnixEpoch).TotalSeconds, 
            DeletedTimestamp = null,
        });
    }

    private static void CreateSampleCredentialInSampleFolder(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AnonKeyBackend.Models.Credential>().HasData(new AnonKeyBackend.Models.Credential
        {
            Uuid = "ebd1ef35-cade-4e2a-8117-3ed58bd13143",
            UserUuid = "7d9d3e99-064d-41bf-a125-ca5951e8a048",
            FolderUuid = "09f770c5-b1c1-41c6-bfd2-818b7b443da9",
            Password = "ok/4t4ivIWf2OBMDxLVwnQ==",
            PasswordSalt = "kU3e41TuHAo0wQ==",
            Username = "dVB8ZDxuX0EF1s0Sr3VamQ==",
            UsernameSalt = "-tvfo5xT9-CozA==",
            WebsiteUrl = "vtqxEki7xjUACtMWmt/+tW0Sp3UPREauv0dNN6xwHcQ=",
            WebsiteUrlSalt = "kespCjC3qaiQug==",
            Note = "",
            NoteSalt = "Luc5CtTRT3u7fA==",
            DisplayName = "eyICGOrW1LP69Wv9E1FyOA==",
            DisplayNameSalt = "KfM0ksmb-wH2tw==",
            CreatedTimestamp = (long)DateTime.UtcNow.Subtract(DateTime.UnixEpoch).TotalSeconds, 
            ChangedTimestamp = (long)DateTime.UtcNow.Subtract(DateTime.UnixEpoch).TotalSeconds, 
            DeletedTimestamp = null,
        });
    }
}
