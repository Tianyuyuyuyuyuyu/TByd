using TBydFramework.Examples.Domains;

namespace TBydFramework.Examples.Repositories
{
    public interface IEquipmentInfoRepository
    {
        EquipmentInfo GetById(int id);

        EquipmentInfo GetBySign(string sign, int quality);
    }
}
