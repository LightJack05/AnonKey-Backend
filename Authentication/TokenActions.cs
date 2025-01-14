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
    /// <param name="TokenType">The type of the token</param>
    /// <param name="parentUuid">The UUID of the parent token</param>
    /// <param name="databaseHandle">Databasehandle to validate the token against</param>
    public static bool IsAccessTokenValid(string TokenType, string parentUuid, Data.DatabaseHandle databaseHandle)
    {
        if (TokenType != "AccessToken") return false;
        Models.Token? parentToken = databaseHandle.RefreshTokens.FirstOrDefault(t => t.Uuid == parentUuid);
        ArgumentNullException.ThrowIfNull(parentToken);

        if (parentToken.Revoked) return false;

        return true;
    }

    /// <summary>
    /// Validates if a refresh token is valid (and not revoked)
    /// </summary>
    /// <param name="TokenType">The type of the token</param>
    /// <param name="uuid">The UUID of the token</param>
    /// <param name="databaseHandle">Databasehandle to validate the token against</param>
    public static bool IsRefreshTokenValid(string TokenType, string uuid, Data.DatabaseHandle databaseHandle)
    {
        if (TokenType != "RefreshToken") return false;
        Models.Token? tokenInDb = databaseHandle.RefreshTokens.FirstOrDefault(t => t.Uuid == uuid);
        ArgumentNullException.ThrowIfNull(tokenInDb);

        if (tokenInDb.Revoked) return false;

        return true;
    }

    /// <summary>
    /// Validate if a token is valid based on a ClaimsPrincipal object.
    /// </summary>
    /// <param name="user">The ClaimsPrinciple to validate the token on</param>
    /// <param name="databaseHandle">The database to use for validation</param>
    /// <param name="isRefreshRequest">Whether the request is for a token refresh operation</param>
    public static bool ValidateClaimsOnRequest(ClaimsPrincipal user, Data.DatabaseHandle databaseHandle, bool isRefreshRequest = false)
    {
        if(!isRefreshRequest){
            return IsAccessTokenValid(user.Claims.First(c => c.Type == "TokenType").Value, user.Claims.First(c => c.Type == "TokenParent").Value, databaseHandle);
        }
        else {
            return IsRefreshTokenValid(user.Claims.First(c => c.Type == "TokenType").Value, user.Claims.First(c => c.Type == "TokenUuid").Value, databaseHandle);
        }
    }
}
