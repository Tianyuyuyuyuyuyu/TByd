#if LITEDB
using LiteDB;

namespace TBydFramework.Examples.Repositories
{
    public abstract class LiteDBRepository<T>
    {
        protected ILiteDatabase database;
        protected ILiteCollection<T> collection;
        public LiteDBRepository(ILiteDatabase database)
        {
            this.database = database;
        }

        protected ILiteCollection<T> GetCollection()
        {
            if (collection == null)
                collection = database.GetCollection<T>();
            return collection;
        }
    }
}
#endif
