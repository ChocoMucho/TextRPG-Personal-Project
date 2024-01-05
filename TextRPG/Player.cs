using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;



namespace TextRPG
{
    public class Player
    {
        //싱글톤
        private static Player instance;
        public static Player Instance
        { get 
            {
                if (instance == null)
                    instance = new Player();
                
                return instance;
            }
        }

        public Inventory inventory;

        public int Level { get; set; }
        public int ClearCount { get; set; }
        public string Name { get; set; }
        public PlayerClass PlayerClass { get; set; }
        public float Attack { get; set; }
        public float AttackBonus { get; set; }

        public int Defence { get; set; }
        public int DefenceBonus { get; set; }
            
        public int Hp { get; set; }
        public int Gold { get; set; }
        //public List<Item> Inventory { get; set; }


        public Item[] slots;

        public Player()
        {
            inventory = new Inventory(this);
            slots = new Item[3];
            Level = 1;
            ClearCount = 0;
            Gold = 1500;
            AttackBonus = 0;
            DefenceBonus = 0;

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

        public void Equip(Item item)
        {
            //슬롯에 있는 장비와 같은 장비라면
            if (slots[(int)item.Type] == item)
            {
                Unequip(slots[(int)item.Type]);
                return;
            }


            if (null != slots[(int)item.Type])      //이미 슬롯이 차있다면
                Unequip(slots[(int)item.Type]);     //슬롯에 있는 장비 해제 메서드 호출

            slots[(int)item.Type] = item;           //새 장비로 슬롯 채움
            item.Equip(this);                       //새 장비 장착
        }

        public void Unequip(Item item)
        {
            slots[(int)item.Type] = null;           //슬롯 비우기
            item.Unequip(this);                     //아이템의 장착 해제 메서드 호출
        }

        public void Buy(Item item) //장비 구매
        {
            foreach(Item myItem in inventory.Items) // 이미 구매했는지 검사
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
                inventory.Add(item);
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
        public void Sell(Item item)
        {
            //85퍼 가격에 판매
            Gold += (int)(item.Price * 0.85);
            //판매시 장착 해제 -> 장착 개선기능에 영향 미칠 듯 하다.

            //아이템 벗음
            Unequip(item);

            //아이템에 세팅 전부 false
            item.Isbought = false;
            item.IsEquip = false;
            
            //인벤토리에서 제외함
            int index = inventory.Items.IndexOf(item);
            inventory.Items.RemoveAt(index);
        }

        public bool IsDead()
        {
            if(Hp <= 0)
            {
                Hp = 0;
                return true;
            }
            return false;

        }

        public void LevelUp()
        {
            //레벨 만큼 클리어하면
            //레벨 + 1, 공격력 +0.5, 방어력 +1

            if(Level <= ClearCount)
            {
                ClearCount = 0;
                ++Level;
                Attack += 0.5f;
                ++Defence;
                ShowLevelUp();
            }
        }

        public void ShowLevelUp()
        {
            Console.Clear();
            Console.WriteLine(
                "     --        ------: --    :-.  ------: -:          \r\n" +
                "     %%.     -+#*****+.%%    #%:=+#*****=:%#          \r\n" +
                "     %%.     +%%##=   .%%    #%:*%##*-   :%#          \r\n" +
                "     %%-:::: +%*-=-::. :+%*=%#: *%+=-:::.:%%:::::     \r\n" +
                "     *######-  ***###*    *#     .*#**+*=:#######     \r\n" +
                "                                                      \r\n" +
                "     :::::           ::::.  .::::::::::::::::         \r\n" +
                "     #####           ####+  =################.        \r\n" +
                "     #####           ####+  =################-:::     \r\n" +
                "     #####           ####+  =####:..........*####     \r\n" +
                "     #####           ####+  =####-::::::::::*####     \r\n" +
                "     #####           ####+  =################:        \r\n" +
                "     #####           ####+  =##########*#####.        \r\n" +
                "     #####           ####+  =####:                    \r\n" +
                "     #####-----------####+  =####.                    \r\n" +
                "         ############*      =####.                    \r\n" +
                "         ############*      =####.                    \r\n");

            Console.WriteLine($"Lv.{Level-1} -> Lv.{Level}");
            Console.WriteLine("레벨업 하였습니다.\n");

            Console.Write("돌아가기...");
            Console.ReadLine();
        }
    }
}
