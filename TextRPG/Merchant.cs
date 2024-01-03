using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class Merchant //아이템을 어찌 생성할지 몰라 만든 상인 클래스
    {
        public List<Item> items = new List<Item>();

        public Merchant() 
        {
            items.Add(new Weapon("낡은 직검", "쉽게 볼 수 있는 낡은 직검입니다.", 2, 600));
            items.Add(new Weapon("청동 도끼", "어디선가 사용된 느낌의 도끼입니다.", 5, 1500));
            items.Add(new Weapon("스파르타의 창", "스파르타의 전사들이 사용했다는 전설의 창입니다.", 7, 3000));

            items.Add(new Armor("수련자 갑옷", "수련에 도움을 주는 갑옷입니다.", 5, 1000));
            items.Add(new Armor("무쇠갑옷", "무쇠로 만들어져 튼튼한 갑옷입니다.", 9, 2000));
            items.Add(new Armor("스파르타의 갑옷", "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 15, 3500));

            items.Add(new Shield("나무 방패", "나무를 덧대 만든 조악한 방패입니다.", 2, 500));
            items.Add(new Shield("철/나무 방패", "철과 나무로 된 견고한 방패입니다.", 4, 1000));
            items.Add(new Shield("스파르타의 방패", "Λ가 새겨진 전사의 방패입니다.", 8, 2000));
        }
    }
}
