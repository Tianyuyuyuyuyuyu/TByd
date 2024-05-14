using System.IO;

namespace TBydFramework.Runtime.Security.Cryptography
{
    public interface IStreamDecryptor : IDecryptor
    {
        Stream Decrypt(Stream input);

    }
}