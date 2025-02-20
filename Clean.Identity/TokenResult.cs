using System.Security.Claims;

namespace Clean.Identity;

public enum TokenValidateResult
{
    NotValid,
    NotValidRole,
    NotValidAudience,
    NotValidIssuer,
    NotValidReason,
    Valid,
    NotValidExpire
}
public class TokenResult
{
    TokenResult(TokenValidateResult tokenValidateResult, string tokenId, string userId)
    {
        ValidateResult= tokenValidateResult;
        TokenId = tokenId;
        UserId = userId;
        NumericUserId = 1;
    }
    public TokenResult(TokenValidateResult tokenValidateResult) : this(tokenValidateResult, null, null)
    {

    }
    public TokenResult(string tokenId, string userId) : this(TokenValidateResult.Valid, tokenId, userId)
    {

    }
    public TokenValidateResult ValidateResult { set; get; }
    public string TokenId { set; get; }
    public string UserId { set; get; }
    public int? NumericUserId { set; get; }
    public ClaimsPrincipal ClaimPrincipal { get; set; }
}