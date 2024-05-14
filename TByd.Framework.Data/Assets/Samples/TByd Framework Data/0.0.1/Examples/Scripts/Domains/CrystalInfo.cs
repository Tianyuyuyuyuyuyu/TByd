#if NEWTONSOFT
using Newtonsoft.Json;
#endif
using System.Collections.Generic;
namespace TBydFramework.Examples.Domains
{
    public class CrystalInfo
    {
        /// <summary>
        /// ID
        /// </summary>
#if NEWTONSOFT
        [JsonProperty("id")]
#endif
        public int Id { get; protected set; }

        /// <summary>
        /// 标识
        /// </summary>
#if NEWTONSOFT
        [JsonProperty("sign")]
#endif
        public string Sign { get; protected set; }

        /// <summary>
        /// 名称
        /// </summary>
#if NEWTONSOFT
        [JsonProperty("name")]
#endif
        public string Name { get; protected set; }

        /// <summary>
        /// 等级
        /// </summary>
#if NEWTONSOFT
        [JsonProperty("level")]
#endif
        public int Level { get; protected set; }

        /// <summary>
        /// 类型
        /// </summary>
#if NEWTONSOFT
        [JsonProperty("category")]
#endif
        public Category Category { get; protected set; }

        /// <summary>
        /// 品质
        /// </summary>
#if NEWTONSOFT
        [JsonProperty("quality")]
#endif
        public int Quality { get; protected set; }

        /// <summary>
        /// 水晶适配装备类型
        /// </summary>
#if NEWTONSOFT
        [JsonProperty("equipmentCategorys")]
#endif
        public List<int> EquipmentCategorys { get; protected set; }

        /// <summary>
        /// 属性名称
        /// </summary>
#if NEWTONSOFT
        [JsonProperty("key")]
#endif
        public string Key { get; protected set; }

        /// <summary>
        /// 属性数值
        /// </summary>
#if NEWTONSOFT
        [JsonProperty("value")]
#endif
        public int Value { get; protected set; }

        /// <summary>
        /// 合成所需水晶ID
        /// </summary>
#if NEWTONSOFT
        [JsonProperty("combineRequired")]
#endif
        public string CombineRequired { get; protected set; }

        /// <summary>
        /// 合成所需水晶数量
        /// </summary>
#if NEWTONSOFT
        [JsonProperty("combineRequiredCount")]
#endif
        public int CombineRequiredCount { get; protected set; }

        /// <summary>
        /// 镶嵌消耗金币
        /// </summary>
#if NEWTONSOFT
        [JsonProperty("inlayCostGold")]
#endif
        public int InlayCostGold { get; protected set; }

        /// <summary>
        /// 出售价格
        /// </summary>
#if NEWTONSOFT
        [JsonProperty("sale")]
#endif
        public int Sale { get; protected set; }
    }
}
