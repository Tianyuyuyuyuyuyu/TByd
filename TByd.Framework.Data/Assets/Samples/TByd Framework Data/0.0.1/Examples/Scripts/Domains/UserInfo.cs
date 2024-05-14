using System.Collections.Generic;
using TBydFramework.Runtime.Localizations.Data;
#if NEWTONSOFT
using Newtonsoft.Json;
#endif

namespace TBydFramework.Examples.Domains
{
    public class UserInfo
    {
#if NEWTONSOFT
        [JsonProperty("id")]
#endif
        public int Id { get; set; }

#if NEWTONSOFT
        [JsonProperty("username")]
#endif
        public string Username { get; set; }

#if NEWTONSOFT
        [JsonProperty("name")]
#endif
        public LocalizedString Name { get; set; }

#if NEWTONSOFT
        [JsonProperty("emails")]
#endif
        public List<string> Emails { get; set; }

#if NEWTONSOFT
        [JsonProperty("information")]
#endif
        public LocalizedString Information { get; set; }

#if NEWTONSOFT
        [JsonProperty("address")]
#endif
        public Address Address { get; set; }

#if NEWTONSOFT
        [JsonProperty("status")]
#endif
        public Status Status { get; set; }
    }

    public class Address
    {
#if NEWTONSOFT
        [JsonProperty("province")]
#endif
        public string Province { get; set; }

#if NEWTONSOFT
        [JsonProperty("street")]
#endif
        public string Street { get; set; }

#if NEWTONSOFT
        [JsonProperty("postcode")]
#endif
        public string Postcode { get; set; }
    }

    public enum Status
    {
        OK,
        LOCKED,
        DELETED
    }
}
