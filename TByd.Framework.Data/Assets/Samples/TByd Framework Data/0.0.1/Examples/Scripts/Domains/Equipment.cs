﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace TBydFramework.Examples.Domains
{
    public class Equipment
    {
        private float health = 0;

        private float attackDamage = 0;

        private float abilityPower = 0;

        private float armor = 0;

        private float magicResist = 0;

        private List<CrystalInfo> crystals;

        public Equipment() : this(new List<CrystalInfo>())
        {
        }

        public Equipment(List<CrystalInfo> crystals)
        {
            this.crystals = crystals;
            foreach (var crystal in this.crystals)
            {
                if (crystal != null)
                    Apply(crystal);
            }
        }

        /// <summary>
        /// 装备ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 玩家ID
        /// </summary>
        public string PlayerId { get; set; }

        /// <summary>
        /// 级别
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// 镶嵌的宝石
        /// </summary>
        public ReadOnlyCollection<CrystalInfo> Crystals
        {
            get { return crystals.AsReadOnly(); }
        }

        /// <summary>
        /// 是否被穿戴
        /// </summary>
        public bool Used { get; set; }

        /// <summary>
        /// 装备信息
        /// </summary>
        public EquipmentInfo Definition { get; set; }

        public float Health
        {
            get { return Definition.Health + this.health; }
        }

        /// <summary>
        /// 攻击力伤害
        /// </summary>
        public float AttackDamage
        {

            get { return Definition.AttackDamage + this.attackDamage; }
        }

        /// <summary>
        /// 法术强度
        /// </summary>

        public float AbilityPower
        {
            get { return Definition.AbilityPower + this.abilityPower; }
        }

        /// <summary>
        /// 护甲值
        /// </summary>

        public float Armor
        {
            get { return Definition.Armor + this.armor; }
        }

        /// <summary>
        /// 魔法抗性
        /// </summary>
        public float MagicResist
        {
            get { return Definition.MagicResist + this.magicResist; }
        }

        /// <summary>
        /// 镶嵌宝石
        /// </summary>
        /// <param name="index"></param>
        /// <param name="crystal"></param>
        /// <returns></returns>
        public bool InlayCrystal(int index, CrystalInfo crystal)
        {
            if (index < 0)
                throw new IndexOutOfRangeException();

            while (index >= crystals.Count)
                crystals.Add(null);

            crystals[index] = crystal;
            Apply(crystal);
            return true;
        }

        /// <summary>
        /// 卸载宝石
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public CrystalInfo UnloadCrystal(int index)
        {
            if (index < 0 || index > crystals.Count)
                throw new IndexOutOfRangeException();

            CrystalInfo crystal = crystals[index];
            crystals[index] = null;
            this.Remove(crystal);
            return crystal;
        }

        private void Apply(CrystalInfo crystal)
        {
            if (crystal == null)
                return;

            switch (crystal.Key)
            {
                case "health":
                    this.health += crystal.Value;
                    break;
                case "attackDamage":
                    this.attackDamage += crystal.Value;
                    break;
                case "abilityPower":
                    this.abilityPower += crystal.Value;
                    break;
                case "armor":
                    this.armor += crystal.Value;
                    break;
                case "magicResist":
                    this.magicResist += crystal.Value;
                    break;
            }
        }

        private void Remove(CrystalInfo crystal)
        {
            if (crystal == null)
                return;

            switch (crystal.Key)
            {
                case "health":
                    this.health -= crystal.Value;
                    break;
                case "attackDamage":
                    this.attackDamage -= crystal.Value;
                    break;
                case "abilityPower":
                    this.abilityPower -= crystal.Value;
                    break;
                case "armor":
                    this.armor -= crystal.Value;
                    break;
                case "magicResist":
                    this.magicResist -= crystal.Value;
                    break;
            }
        }
    }
}
