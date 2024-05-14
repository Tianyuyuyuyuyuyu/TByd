#if NEWTONSOFT
using TBydFramework.Examples.Domains;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace TBydFramework.Examples.Repositories
{
    public class JsonCrystalInfoRepository : ICrystalInfoRepository
    {
        private Dictionary<int, CrystalInfo> crystals = new Dictionary<int, CrystalInfo>();
        private bool loaded = false;

        private void LoadAll()
        {
            var text = Resources.Load<TextAsset>("Json/crystalinfo");
            if (text == null || text.text.Length <= 0)
                return;

            using (StringReader reader = new StringReader(text.text))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    CrystalInfo info = JsonConvert.DeserializeObject<CrystalInfo>(line);
                    if (info == null)
                        continue;

                    crystals[info.Id] = info;
                }
            }
            this.loaded = true;
        }

        public CrystalInfo GetById(int id)
        {
            if (!loaded)
                this.LoadAll();

            CrystalInfo info = null;
            crystals.TryGetValue(id, out info);
            return info;
        }

        public CrystalInfo GetBySign(string sign, int level)
        {
            if (!loaded)
                this.LoadAll();
            return crystals.Values.Where(e => e.Sign.Equals(sign) && e.Level == level).First();
        }
    }
}
#endif