using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LyceeBalzacApi.data_models;
using Microsoft.IdentityModel.Tokens;

namespace LyceeBalzacApi.security;

public class BasicJwtService: JwtService
{
    private IConfiguration _config;
    public BasicJwtService(IConfiguration configuration)
    {
        _config = configuration;
    }
    public string CreateToken(Claim[] claims)
    {
        var key = Encoding.UTF8.GetBytes(_config["Jwt:secret"]);

        var securityKey = new SymmetricSecurityKey(key);
        
        var signIn = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(issuer: _config["Jwt:issuer"], audience: _config["Jwt:audience"], claims,
            expires: DateTime.Today.AddDays(1), signingCredentials: signIn);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public int? GetUserIdFromToken(string token)
    {
        var key = Encoding.UTF8.GetBytes(_config["Jwt:secret"]);
        var securityKey = new SymmetricSecurityKey(key);
        var tokenHandler = new JwtSecurityTokenHandler();
        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = securityKey,
            ValidateIssuer = true,
            ValidIssuer = _config["Jwt:issuer"],
            ValidateAudience = true,
            ValidAudience = _config["Jwt:audience"]
        };
        var claims = tokenHandler.ValidateToken(token, validationParameters, out var securityToken);
        var jwtSecurityToken = securityToken as JwtSecurityToken;
        if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            throw new SecurityTokenException("Invalid token");
        var value = claims.FindFirst(x => x.Type == "userId")?.Value;
        if (value != null)
            return int.Parse(value);
        return null;
    }
}