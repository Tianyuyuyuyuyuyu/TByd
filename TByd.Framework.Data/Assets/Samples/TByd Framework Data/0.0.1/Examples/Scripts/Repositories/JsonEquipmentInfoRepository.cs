#if NEWTONSOFT
using TBydFramework.Examples.Domains;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace TBydFramework.Examples.Repositories
{
    public class JsonEquipmentInfoRepository : IEquipmentInfoRepository
    {
        private Dictionary<int, EquipmentInfo> equipments = new Dictionary<int, EquipmentInfo>();
        private bool loaded = false;
        private void LoadAll()
        {
            var text = Resources.Load<TextAsset>("Json/equipmentinfo");
            if (text == null || text.text.Length <= 0)
                return;

            using (StringReader reader = new StringReader(text.text))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    EquipmentInfo equipmentInfo = JsonConvert.DeserializeObject<EquipmentInfo>(line);
                    if (equipmentInfo == null)
                        continue;
                    equipments[equipmentInfo.Id] = equipmentInfo;
                }
            }
            this.loaded = true;
        }

        public virtual EquipmentInfo GetById(int id)
        {
            if (!loaded)
                this.LoadAll();

            EquipmentInfo info = null;
            equipments.TryGetValue(id, out info);
            return info;
        }

        public virtual EquipmentInfo GetBySign(string sign, int quality)
        {
            if (!loaded)
                this.LoadAll();

            return equipments.Values.Where(e => e.Sign.Equals(sign) && e.Quality == quality).First();
        }
    }
}
#endif