#if NEWTONSOFT
using TBydFramework.Examples.Domains;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace TBydFramework.Examples.Repositories
{
    public class JsonUserInfoRepository : IUserInfoRepository
    {
        private Dictionary<int, UserInfo> idAndUserInfoMapping = new Dictionary<int, UserInfo>();
        private Dictionary<string, UserInfo> usernameAndUserInfoMapping = new Dictionary<string, UserInfo>();
        private bool loaded = false;
        private void LoadAll()
        {
            var text = Resources.Load<TextAsset>("Json/userinfo");
            if (text == null || text.text.Length <= 0)
                return;

            using (StringReader reader = new StringReader(text.text))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    UserInfo userInfo = JsonConvert.DeserializeObject<UserInfo>(line);
                    if (userInfo == null)
                        continue;

                    idAndUserInfoMapping[userInfo.Id] = userInfo;
                    usernameAndUserInfoMapping[userInfo.Username] = userInfo;
                }
            }
            this.loaded = true;
        }

        public virtual UserInfo GetById(int id)
        {
            if (!loaded)
                this.LoadAll();

            UserInfo userInfo = null;
            idAndUserInfoMapping.TryGetValue(id, out userInfo);
            return userInfo;
        }

        public virtual UserInfo GetByUsername(string username)
        {
            if (!loaded)
                this.LoadAll();

            UserInfo userInfo = null;
            usernameAndUserInfoMapping.TryGetValue(username, out userInfo);
            return userInfo;
        }
    }
}
#endif