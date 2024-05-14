using TBydFramework.Runtime.Localizations.Data;
#if NEWTONSOFT
using Newtonsoft.Json;
#endif

namespace TBydFramework.Examples.Domains
{
    public class EquipmentInfo
    {
#if NEWTONSOFT
        [JsonProperty("id")]
#endif
        public int Id { get; set; }

#if NEWTONSOFT
        [JsonProperty("sign")]
#endif
        public string Sign { get; protected set; }

#if NEWTONSOFT
        [JsonProperty("name")]
#endif
        public LocalizedString Name { get; set; }

#if NEWTONSOFT
        [JsonProperty("category")]
#endif
        public Category Category { get; set; }

#if NEWTONSOFT
        [JsonProperty("quality")]
#endif
        public int Quality { get; set; }

#if NEWTONSOFT
        [JsonProperty("health")]
#endif
        public float Health { get; set; }

#if NEWTONSOFT
        [JsonProperty("attackDamage")]
#endif
        public float AttackDamage { get; set; }

#if NEWTONSOFT
        [JsonProperty("abilityPower")]
#endif
        public float AbilityPower { get; set; }

#if NEWTONSOFT
        [JsonProperty("armor")]
#endif
        public float Armor { get; set; }

#if NEWTONSOFT
        [JsonProperty("magicResist")]
#endif
        public float MagicResist { get; set; }
    }
}
