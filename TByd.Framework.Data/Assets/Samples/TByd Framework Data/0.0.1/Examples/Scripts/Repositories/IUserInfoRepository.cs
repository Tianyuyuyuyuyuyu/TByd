using TBydFramework.Examples.Domains;

namespace TBydFramework.Examples.Repositories
{
    public interface IUserInfoRepository
    {
        UserInfo GetById(int id);

        UserInfo GetByUsername(string username);
    }
}