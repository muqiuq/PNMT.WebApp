using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace PNMT.WebApp.Authentification
{
  public class WebAppUser
  {

    public string Username { get; set; }

    public string PasswordHash { get; set; }

    public string Salt { get; set; }


    public void SetPassword(string password)
    {
      byte[] salt = RandomNumberGenerator.GetBytes(128 / 8);

      string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
          password: password!,
          salt: salt,
          prf: KeyDerivationPrf.HMACSHA256,
          iterationCount: 10000,
          numBytesRequested: 256 / 8));

      PasswordHash = hashed;

      Salt = Convert.ToBase64String(salt);
    }

    public bool CheckPassword(string otherPassword)
    {
      string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
          password: otherPassword!,
          salt: Convert.FromBase64String(Salt),
          prf: KeyDerivationPrf.HMACSHA256,
          iterationCount: 10000,
          numBytesRequested: 256 / 8));

      return hashed == PasswordHash;
    }

  }
}
