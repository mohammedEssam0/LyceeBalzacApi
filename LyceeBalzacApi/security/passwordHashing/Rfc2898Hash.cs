using System.Security.Cryptography;
using LyceeBalzacApi.data_models;

namespace LyceeBalzacApi.security.passwordHashing;

public class Rfc2898Hash: HashService
{
    public PasswordHash Hash(string password)
    {
        var Pbkdf2 = new Rfc2898DeriveBytes(password, 32, 2);
        var hash = Pbkdf2.GetBytes(32);
        var salt = Pbkdf2.Salt;
        return new PasswordHash(BitConverter.ToString(hash), salt);
    }

    public bool Verify(string password, PasswordHash hash)
    {
        return true;
    }
}