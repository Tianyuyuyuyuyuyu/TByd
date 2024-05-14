#if LITEDB
using LiteDB;
using TBydFramework.Examples.Domains;

namespace TBydFramework.Examples.Repositories
{
    public class LiteDBEquipmentInfoRepository : LiteDBRepository<EquipmentInfo>, IEquipmentInfoRepository
    {
        public LiteDBEquipmentInfoRepository(ILiteDatabase database) : base(database)
        {
        }


        public EquipmentInfo GetById(int id)
        {
            return GetCollection().FindById(id);
        }

        public EquipmentInfo GetBySign(string sign, int quality)
        {
            return GetCollection().FindOne(c => c.Sign.Equals(sign) && c.Quality == quality);
        }
    }
}
#endif
