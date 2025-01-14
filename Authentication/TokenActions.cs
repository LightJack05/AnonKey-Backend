namespace AnonKeyBackend.Authentication;

public class TokenActions
{
    public static void StoreTokenInDb(Models.Token token, Data.DatabaseHandle databaseHandle)
    {
        databaseHandle.RefreshTokens.Add(token);
    }

    public static bool IsAccessTokenValid(Models.Token token, Data.DatabaseHandle databaseHandle)
    {
        if(token.TokenType != "AccessToken") throw new ArgumentOutOfRangeException(nameof(token),"The token is not an access token.");
        Models.Token? parentToken = databaseHandle.RefreshTokens.FirstOrDefault(t => t.Uuid == token.ParentUuid);
        ArgumentNullException.ThrowIfNull(parentToken);

        if(parentToken.Revoked) return false;

        return true;
    }

    public static bool IsRefreshTokenValid(Models.Token token, Data.DatabaseHandle databaseHandle){
        if(token.TokenType != "RefreshToken") throw new ArgumentOutOfRangeException(nameof(token),"The token is not a refresh token.");
        Models.Token? tokenInDb = databaseHandle.RefreshTokens.FirstOrDefault(t => t.Uuid == token.Uuid);
        ArgumentNullException.ThrowIfNull(tokenInDb);

        if(tokenInDb.Revoked) return false;

        return true;
    }
}
