#if LITEDB
using LiteDB;
using TBydFramework.Examples.Domains;

namespace TBydFramework.Examples.Repositories
{
    public class LiteDBUserInfoRepository : LiteDBRepository<UserInfo>, IUserInfoRepository
    {
        public LiteDBUserInfoRepository(ILiteDatabase database) : base(database)
        {
        }

        public UserInfo GetById(int id)
        {
            return GetCollection().FindById(id);
        }

        public UserInfo GetByUsername(string username)
        {
            return GetCollection().FindOne(c => c.Username.Equals(username));
        }
    }
}
#endif
