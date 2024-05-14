using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TBydFramework.Tests.Editor.Odin.Attributes.Searchable
{
    public class Example1 : MonoBehaviour
    {
        [Searchable]
        public List<Perk> Perks = new List<Perk>()
        {
            new Perk()
            {
                Name = "Old Sage",
                Effects = new List<Effect>()
                {
                    new() { Skill = Skill.Wisdom, Value = 2, },
                    new() { Skill = Skill.Intelligence, Value = 1, },
                    new() { Skill = Skill.Strength, Value = -2 },
                },
            },
            new Perk()
            {
                Name = "Hardened Criminal",
                Effects = new List<Effect>()
                {
                    new() { Skill = Skill.Dexterity, Value = 2, },
                    new() { Skill = Skill.Strength, Value = 1, },
                    new() { Skill = Skill.Charisma, Value = -2 },
                },
            },
            new Perk()
            {
                Name = "Born Leader",
                Effects = new List<Effect>()
                {
                    new() { Skill = Skill.Charisma, Value = 2, },
                    new() { Skill = Skill.Intelligence, Value = -3 },
                },
            },
            new Perk()
            {
                Name = "Village Idiot",
                Effects = new List<Effect>()
                {
                    new() { Skill = Skill.Charisma, Value = 4, },
                    new() { Skill = Skill.Constitution, Value = 2, },
                    new() { Skill = Skill.Intelligence, Value = -3 },
                    new() { Skill = Skill.Wisdom, Value = -3 },
                },
            },
        };

        [Serializable]
        public class Perk
        {
            public string Name;

            [TableList]
            public List<Effect> Effects;
        }

        [Serializable]
        public class Effect
        {
            public Skill Skill;
            public float Value;
        }

        public enum Skill
        {
            Strength,
            Dexterity,
            Constitution,
            Intelligence,
            Wisdom,
            Charisma,
        }
    }
}