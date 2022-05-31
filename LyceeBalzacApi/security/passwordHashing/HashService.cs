using LyceeBalzacApi.data_models;

namespace LyceeBalzacApi.security.passwordHashing;

public interface HashService
{
    PasswordHash Hash(string password);
    
    bool Verify(string password, PasswordHash hash);
}