using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace authentication.Authentication;

public class Authentication
{

    protected readonly IConfiguration _configuration;
    public Authentication(IConfiguration configuration1)
    {
        _configuration = configuration1;
    }

    public string GenerateJwtToken(List<Claim> subject)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var Sectoken = new JwtSecurityToken(
            _configuration["Jwt:Issuer"],
            _configuration["Jwt:Issuer"],
            subject,
            expires: DateTime.Now.AddMinutes(120),
            signingCredentials: credentials);
        
        var token = new JwtSecurityTokenHandler().WriteToken(Sectoken);
        
        return token;
    }

    public ClaimsPrincipal DecodeJwtToken(string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _configuration["Jwt:Issuer"],
                ValidAudience = _configuration["Jwt:Issuer"],
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };

            return tokenHandler.ValidateToken(token, tokenValidationParameters, out _);
        }
        catch (SecurityTokenMalformedException ex)
        {
            throw new SecurityTokenMalformedException(ex.Message);
        }
    }

}