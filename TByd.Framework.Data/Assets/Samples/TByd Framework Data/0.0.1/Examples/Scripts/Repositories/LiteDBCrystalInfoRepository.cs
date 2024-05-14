#if LITEDB
using LiteDB;
using TBydFramework.Examples.Domains;

namespace TBydFramework.Examples.Repositories
{
    public class LiteDBCrystalInfoRepository : LiteDBRepository<CrystalInfo>, ICrystalInfoRepository
    {
        public LiteDBCrystalInfoRepository(ILiteDatabase database) : base(database)
        {
        }

        public CrystalInfo GetById(int id)
        {
            return GetCollection().FindById(id);
        }

        public CrystalInfo GetBySign(string sign, int level)
        {
            return GetCollection().FindOne(c => c.Sign.Equals(sign) && c.Level == level);
        }
    }
}
#endif