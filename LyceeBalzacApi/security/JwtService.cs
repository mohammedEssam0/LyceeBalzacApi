using System.Security.Claims;
using LyceeBalzacApi.data_models;

namespace LyceeBalzacApi.security;

public interface JwtService
{
    string CreateToken(Claim[] claims);
    
    int? GetUserIdFromToken(string token);
    
}