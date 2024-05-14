using TBydFramework.Examples.Domains;

namespace TBydFramework.Examples.Repositories
{
    public interface ICrystalInfoRepository
    {
        CrystalInfo GetById(int id);

        CrystalInfo GetBySign(string sign, int level);
    }
}
