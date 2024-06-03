using System.Security.Cryptography;
using System.Text;

namespace TesteLarTech.Core.Hash
{
    public class Encrypt
    {
        private HashAlgorithm _algoritmo;

        public Encrypt(HashAlgorithm algoritmo)
        {
            _algoritmo = algoritmo;
        }

        public string EncryptString(string str)
        {
            var encodedValue = Encoding.UTF8.GetBytes(str);
            var encryptedPassword = _algoritmo.ComputeHash(encodedValue);

            var sb = new StringBuilder();
            foreach (var caracter in encryptedPassword)
            {
                sb.Append(caracter.ToString("X2"));
            }

            return sb.ToString();
        }

        public bool ValidateHash(string hashReference, string hashToCompare)
        {
            if (string.IsNullOrEmpty(hashToCompare))
                throw new NullReferenceException("Cadastre uma senha.");

            var encryptedPassword = _algoritmo.ComputeHash(Encoding.UTF8.GetBytes(hashReference));

            var sb = new StringBuilder();
            foreach (var caractere in encryptedPassword)
            {
                sb.Append(caractere.ToString("X2"));
            }

            return sb.ToString() == hashToCompare;
        }
    }
}
