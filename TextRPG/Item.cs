using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// 아이템의 종류 나타내는 열거형
public enum ItemType
{
    Weapon,
    Armor,
    Shield,
}

namespace TextRPG
{
    
    public abstract class Item
    {
        //========== 아이템 기본 정보 ==========
        public string       Name { get; private set; }
        public string       Desc { get; private set; }
        public ItemType     Type { get; private set; }
        public int          Price { get; private set; }
        public bool         IsEquip { get; set; }
        public bool         Isbought { get; set; }


        //========== 아이템 초기화 ==========
        public Item(string name, string desc, ItemType type, int price)
        {
            Name = name;
            Desc = desc;
            Type = type;
            Price = price;
            IsEquip = false;
            Isbought = false;
        }


        //========== 아이템 장착, 해제 ==========
        public abstract void Equip(Player player); //장비 입음
        public abstract void Unequip(Player player); //장비 벗음

        
    }

    //Weapon은 공격력을 Armor와 Shield는 방어력을 필드로 가지고 있음

    public class Weapon : Item
    {
        public int Attack { get; private set; }

        public Weapon(string name, string desc, int attack, int price) :base(name, desc, ItemType.Weapon, price)
        {
            Attack = attack;
        }

        // 장비 장착 시 호출되는 메서드
        public override void Equip(Player player)
        {
            this.IsEquip = true;
            player.AttackBonus += this.Attack; //플레이어의 추가 공격력 더함
        }

        //장비 해제 시 호출되는 메서드
        public override void Unequip(Player player)
        {
            this.IsEquip = false;
            player.AttackBonus -= this.Attack; //플레이어의 추가 공격력 뺌
        }
    }



    // 아래는 공격력 -> 방어력의 차이만 존재함
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