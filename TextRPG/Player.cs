﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;



namespace TextRPG
{
    public class Player
    {
        public int Level { get; set; }
        public string Name { get; set; }
        public PlayerClass PlayerClass { get; set; }
        public int Attack { get; set; }
        public int AttackBonus { get; set; }

        public int Defence { get; set; }
        public int DefenceBonus { get; set; }

        public float Hp { get; set; }
        public int Gold { get; set; }
        public List<Item> Inventory { get; set; }

        public Player()
        {
            Level = 1;
            Gold = 1500;
            Inventory = new List<Item>();
            SetInfo();
        }

        public void SetInfo()//지금은 하드 코딩이지만 나중에는 캐릭터 생성 창에서 정보 받아옴
        {
            Name = "레오니다스";
            PlayerClass = PlayerClass.Warrior;
            Attack = 10;
            Defence = 5;
            Hp = 100;

            
        }

        /*public void EquipItem(int index)
        {
            //해당 인덱스 아이템 장착중 아니면 -> 장착, 추가 공격/방어력에 추가
            if (!Inventory[index].IsEquip)
                Inventory[index].IsEquip = true;

            else//장착 중이면 -> 장착 해제, 추가 공격/방어력에서 뺴기
                Inventory[index].IsEquip = false;
        }*/

        public void Buy(Item item) //장비 구매
        {
            foreach(Item myItem in Inventory) // 이미 구매했는지 검사
            {
                if (myItem == item)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("이미 구매한 아이템입니다.");
                    Console.ResetColor();
                    Console.ReadLine();
                    return;
                }
            }

            //돈 충분한지 검사 -> 충분하면 구매처리
            if(Gold >= item.Price)
            {
                item.Isbought = true;
                Inventory.Add(item);
                Gold -= item.Price;
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("구매 완료.");
                Console.ResetColor();
                Console.ReadLine();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("골드가 부족합니다.");
                Console.ResetColor();
                Console.ReadLine();
            }
            
        }
    }
}