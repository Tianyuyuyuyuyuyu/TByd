using TBydFramework.Runtime.Security.Cryptography;
using UnityEngine;

namespace TBydFramework.XLua.Editor.Views
{
    public abstract class EncryptorFactory : ScriptableObject
    {
        public abstract IEncryptor Create();
    }
}
