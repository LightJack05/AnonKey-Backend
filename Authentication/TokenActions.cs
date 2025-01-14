namespace AnonKeyBackend.Authentication;

/// <summary>
/// Contians actions to perform on Tokens
/// </summary>
public class TokenActions
{
    /// <summary>
    /// Stores a refresh token into the database
    /// </summary>
    /// <param name="token">The token to store</param>
    /// <param name="databaseHandle">Databasehandle to store the token to</param>
    public static void StoreRefreshTokenInDb(Models.Token token, Data.DatabaseHandle databaseHandle)
    {
        databaseHandle.RefreshTokens.Add(token);
    }

    /// <summary>
    /// Validates if an access token is valid (and not revoked)
    /// </summary>
    /// <param name="token">The token to validate</param>
    /// <param name="databaseHandle">Databasehandle to validate the token against</param>
    public static bool IsAccessTokenValid(Models.Token token, Data.DatabaseHandle databaseHandle)
    {
        if(token.TokenType != "AccessToken") throw new ArgumentOutOfRangeException(nameof(token),"The token is not an access token.");
        Models.Token? parentToken = databaseHandle.RefreshTokens.FirstOrDefault(t => t.Uuid == token.ParentUuid);
        ArgumentNullException.ThrowIfNull(parentToken);

        if(parentToken.Revoked) return false;

        return true;
    }

    /// <summary>
    /// Validates if a refresh token is valid (and not revoked)
    /// </summary>
    /// <param name="token">The token to validate</param>
    /// <param name="databaseHandle">Databasehandle to validate the token against</param>
    public static bool IsRefreshTokenValid(Models.Token token, Data.DatabaseHandle databaseHandle){
        if(token.TokenType != "RefreshToken") throw new ArgumentOutOfRangeException(nameof(token),"The token is not a refresh token.");
        Models.Token? tokenInDb = databaseHandle.RefreshTokens.FirstOrDefault(t => t.Uuid == token.Uuid);
        ArgumentNullException.ThrowIfNull(tokenInDb);

        if(tokenInDb.Revoked) return false;

        return true;
    }
}
