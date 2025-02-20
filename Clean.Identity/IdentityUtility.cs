
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Clean.Identity;

public class IdentityUtility
{


    public static string GenerateToken(TokenParams TokenParams)
    {
        //https://8gwifi.org/jwsgen.jsp (key generator:HS256)
        string key = TokenParams.Key;
        var claims = new List<Claim>();
        claims.Add(new Claim("UserId", TokenParams.UserId));
        claims.Add(new Claim("Expire", DateTime.Now.Add(TokenParams.ExpireTime).ToString("yyyy-MM-dd HH:mm:ss")));
        if (TokenParams.OtherClaims != null)
        {
            foreach (var customClaim in TokenParams.OtherClaims)
            {
                claims.Add(new Claim(customClaim.Key, customClaim.Value));
            }
        }
        if (TokenParams.OtherClaims != null)
            claims.Add(new Claim("TokenId", TokenParams.TokenId));
        if (TokenParams.Roles != null && TokenParams.Roles.Any())
        {
            foreach (var role in TokenParams.Roles)
            {
                claims.Add(new Claim("Role", role));
            }
        }

        if (TokenParams.Reason != null && TokenParams.Reason.Any())
            claims.Add(new Claim("Reason", TokenParams.Reason));
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

        var credentials = new SigningCredentials(securityKey, TokenParams.SecurityAlgorithm);

        var token = new JwtSecurityToken(TokenParams.Issuer,
            TokenParams.Audience,
            claims,
            null,
            expires: DateTime.Now.AddMilliseconds(TokenParams.ExpireTime.Milliseconds),
            signingCredentials: credentials);

        var result = new JwtSecurityTokenHandler().WriteToken(token);
        return result;
    }

    public static ClaimsPrincipal GetPrincipals(string key,string audience,string issuer,string token)
    {
        try
        {

            token = token.Replace("bearer ", "").Replace("Bearer ", "");
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = GetValidationParams(key,issuer,audience);

            SecurityToken validatedToken;

            ClaimsPrincipal principal = tokenHandler.ValidateToken(token, validationParameters, out validatedToken);
            return principal;
        }
        catch (Exception ex)
        {
            return new ClaimsPrincipal();
        }

    }
    public static TokenResult ValidateToken(ClaimsPrincipal principal, List<string> validRoles = null, List<string> validReasons = null,
        bool isAdmin = false)
    {
        try
        {

            var tokenUserId = principal.FindFirst("UserId");
            var tokenId = principal.FindFirst("TokenId");
            var expireTime = principal.FindFirst("Expire");


            if (principal == null || tokenUserId == null)
            {
                return new TokenResult(TokenValidateResult.NotValid);
            }

            if (DateTime.Now > DateTime.Parse(expireTime.Value))
            {
                return new TokenResult(TokenValidateResult.NotValidExpire);
            }

            var tokenRole = principal.FindAll("Role").ToList();

            if (validRoles.Any())
            {
                if (!tokenRole.Any(a => validRoles.Contains(a.Value)))
                {
                    return new TokenResult(TokenValidateResult.NotValidRole);
                }
            }

            var tokenReason = principal.FindFirst("Reason");

            if (validReasons.Any())
            {
                if (tokenReason == null || !validReasons.Contains(tokenReason.Value))
                {
                    return new TokenResult(TokenValidateResult.NotValidReason);
                }
            }

            var result = new TokenResult(tokenId != null ? tokenId.Value : null, tokenUserId.Value);
            result.ClaimPrincipal = principal;
            return result;
        }
        catch (Exception ex)
        {
            return new TokenResult(TokenValidateResult.NotValid);
        }
    }
    static TokenValidationParameters GetValidationParams(string key,string issuer,string audience)
    {

        var validationParameters = new TokenValidationParameters()
        {
            RequireExpirationTime = false,
            ValidateLifetime = false,  
            ValidateAudience = false, 
            ValidateIssuer = false,  
            ValidIssuer = issuer,
            ValidAudience = audience,
            IssuerSigningKey =
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(key)),
        };
        return validationParameters;
    }
  

}