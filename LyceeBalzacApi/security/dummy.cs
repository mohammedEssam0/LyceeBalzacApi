using System.Security.Claims;

namespace LyceeBalzacApi.security;

public class dummy: JwtService
{
    public string CreateToken(Claim[] claims)
    {
        return "";
    }

    public int? GetUserIdFromToken(string token)
    {
        return null;
    }
}