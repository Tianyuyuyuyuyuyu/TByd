using System.IO;

namespace TBydFramework.Runtime.Security.Cryptography
{
    public interface IStreamEncryptor : IEncryptor
    {
        Stream Encrypt(Stream input);

    }
}