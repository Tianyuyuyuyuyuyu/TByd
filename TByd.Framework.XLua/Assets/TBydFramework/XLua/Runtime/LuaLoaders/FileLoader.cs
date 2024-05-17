using System.Linq;
using TBydFramework.Runtime.Utilities;

namespace TBydFramework.XLua.Runtime.LuaLoaders
{
    public class FileLoader : PathLoaderBase
    {
        public FileLoader(string prefix) : this(prefix, ".lua.txt")
        {
        }

        public FileLoader(string prefix, string suffix) : base(prefix, suffix)
        {
        }

        protected override byte[] Load(ref string path)
        {
            string fullname = GetFullname(path);
            if (!FileUtil.Exists(fullname))
                return null;

            path = fullname;
            byte[] data = FileUtil.ReadAllBytes(fullname);
            if (!HasBOMFlag(data))
                return data;

            return data.Skip(3).ToArray();
        }

        protected bool HasBOMFlag(byte[] data)
        {
            if (data == null || data.Length < 3)
                return false;

            if (data[0] == 239 && data[1] == 187 && data[2] == 191)
                return true;

            return false;
        }
    }
}