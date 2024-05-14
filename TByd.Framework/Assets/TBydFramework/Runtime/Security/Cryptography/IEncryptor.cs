namespace TBydFramework.Runtime.Security.Cryptography
{
    public interface IEncryptor
    {
        string AlgorithmName { get; }

        byte[] Encrypt(byte[] buffer);

    }
}
