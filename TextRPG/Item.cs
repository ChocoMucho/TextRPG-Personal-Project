using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    
    public abstract class Item
    {
        public string Name { get; private set; }
        public string Desc { get; private set; }
        public ItemType Type { get; private set; }
        public int Price { get; private set; }
        public bool IsEquip { get; set; }
        public bool Isbought { get; set; }
        public Item(string name, string desc, ItemType type, int price)
        {
            Name = name;
            Desc = desc;
            Type = type;
            Price = price;
            IsEquip = false;
            Isbought = false;
        }

        public abstract void Equip(Player player); //장비 입음
        public abstract void Unequip(Player player); //장비 벗음

        
    }

    public class Weapon : Item
    {
        public int Attack { get; private set; }

        public Weapon(string name, string desc, int attack, int price) :base(name, desc, ItemType.Weapon, price)
        {
            Attack = attack;
        }

        public override void Equip(Player player)
        {
            this.IsEquip = true;
            player.AttackBonus += this.Attack;
        }

        public override void Unequip(Player player)
        {
            this.IsEquip = false;
            player.AttackBonus -= this.Attack;
        }
    }
    public class Armor : Item
    {
        public int Defence { get; private set; }

        public Armor(string name, string desc, int defence, int price) : base(name, desc, ItemType.Armor, price)
        {
            Defence = defence;
        }

        public override void Equip(Player player)
        {
            this.IsEquip = true;
            player.DefenceBonus += this.Defence;
        }

        public override void Unequip(Player player)
        {
            this.IsEquip = false;
            player.DefenceBonus -= this.Defence;
        }
    }

    public class Shield : Item
    {
        public int Defence { get; private set; }
        public Shield(string name, string desc, int defence, int price) : base(name, desc, ItemType.Shield, price)
        {
            Defence = defence;
        }

        public override void Equip(Player player)
        {
            this.IsEquip = true;
            player.DefenceBonus += this.Defence;
        }

        public override void Unequip(Player player)
        {
            this.IsEquip = false;
            player.DefenceBonus -= this.Defence;
        }
    }
}