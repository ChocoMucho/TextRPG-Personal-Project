using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

// 인벤토리의 현재 상태를 나타낼 용도
public enum InventoryState
{
    Main,
    Equip,
}

namespace TextRPG
{
    public class Inventory
    {
        InventoryState state;
        
        private List<Item>  items; 

        public Player       player;


        public Inventory(Player player) 
        { 
            items = new List<Item>();
            state = InventoryState.Main;
            this.player = player;
        }

        public List<Item> Items 
        { 
            get { return items; } 
        }
        public void Add(Item item) 
        { 
            items.Add(item); 
        }



        //========== Game에서 호출될 함수 ==========
        public void ShowInventory()
        {
            //출력
            Console.Clear();
            Console.WriteLine(
                "                                        \n" +
                "              .::------::.              \n" +
                "           :-=---======---=-:           \n" +
                "        .-=--==+========+==--=-.        \n" +
                "       ---=+=======**=======+=---       \n" +
                "     .=--+========*##*========+--=.     \n" +
                "     =--+========*####*========+--=     \n" +
                "    =--+========+##++##+========+--=    \n" +
                "    +-=========*##+==+##+=========-+    \n" +
                "    +-========*##*====*##*========-+    \n" +
                "    +-=+=====+##*======*##+=====+=-+    \n" +
                "    ---+====+##*========*##+====+---    \n" +
                "     =--+=+#####========#####+=+--=     \n" +
                "      =--======================--=      \n" +
                "       :=--=+==============+=--=:       \n" +
                "         -=--==============--=:         \n" +
                "           .:-==--------==-:.           \n" +
                "                .::::::.                \n");

            switch(state)
            {
                //메인 상태 시 출력
                case InventoryState.Main:
                    Console.WriteLine("============= 인벤토리 =============");
                    Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
                    Console.WriteLine("===================================");
                    Console.WriteLine("\n[아이템 목록]");
                    break;
                //장착 관리 상태 시 출력
                case InventoryState.Equip:
                    Console.WriteLine("======= 인벤토리 - 장착 관리 =======");
                    Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
                    Console.WriteLine("===================================");
                    Console.WriteLine("\n[아이템 목록]");
                    break;
            }
        }

        public void ShowItemList()
        {
            int index = 0;

            switch(state) 
            { 
                // 메인 상태 시 플레이어가 가진 아이템 목록 출력
                case InventoryState.Main:
                    foreach (Item item in items)
                    {
                        string itemInfo = "- ";

                        if (item.IsEquip)
                            itemInfo += "[E]";

                        itemInfo += $"{item.Name}\t";

                        if (item.Type == ItemType.Weapon)
                            itemInfo += $"| 공격력 +{(item as Weapon).Attack}| ";
                        else if (item.Type == ItemType.Armor)
                            itemInfo += $"| 방어력 +{(item as Armor).Defence}| ";
                        else if (item.Type == ItemType.Shield)
                            itemInfo += $"| 방어력 +{(item as Shield).Defence}| ";

                        itemInfo += item.Desc;

                        Console.WriteLine(itemInfo);
                    }
                    break;
                // 장착 관리 상태 시 플레이어가 가진 아이템 목록 출력
                // 선택 위해 넘버링도 출력
                case InventoryState.Equip:
                    foreach (Item item in items)
                    {
                        string itemInfo = $"- {++index} ";

                        if (item.IsEquip)
                            itemInfo += "[E]";

                        itemInfo += $"{item.Name}\t";

                        if (item.Type == ItemType.Weapon)
                            itemInfo += "| 공격력 | ";
                        else
                            itemInfo += "| 방어력 | ";

                        itemInfo += item.Desc;

                        Console.WriteLine(itemInfo);
                    }
                    break;
            }
        }

        // 상태에 따른 선택 메뉴 출력
        public void ShowInventoryMenu()
        {
            switch (state)
            {
                case InventoryState.Main:
                    Console.WriteLine("\n1. 장착 관리");
                    Console.WriteLine("0. 나가기\n"); //마을로

                    Console.WriteLine("원하시는 행동을 입력해주세요.");
                    Console.Write(">>");
                    break;
                case InventoryState.Equip:
                    Console.WriteLine("\n0. 나가기\n"); //인벤토리로

                    Console.WriteLine("원하시는 행동을 입력해주세요.");
                    Console.Write(">>");
                    break;
                default:
                    break;
            }
        }

        //사용자 입력을 받아 로직 진행
        public void SelectMenu(string playerInput, ref Scenes scene)
        {
            int index = 0; // 아이템 인덱싱

            // 메인 상태인 경우
            if(state == InventoryState.Main)
            {
                switch (playerInput)
                {
                    case "0": // 마을로 나가기
                        scene = Scenes.Town;
                        break;
                    case "1": // 장착 관리 상태로 전환
                        state = InventoryState.Equip;
                        break;
                }
            }
            //장착 관리 상태인 경우
            else if(state == InventoryState.Equip)
            {
                switch (playerInput)
                {
                    case "0": // 메인 상태로 되돌아가기
                        state = InventoryState.Main;
                        break;

                    // 입력 숫자에 맞는 아이템 장착
                    default:
                        if (int.TryParse(playerInput, out index) && 0 < index && index <= items.Count)
                        {
                            player.Equip(items[--index]); //내 인벤토리에 있는 장비 장착
                        }
                        else
                        {
                            WrongInput();
                        }
                        break;

                }
            }
        }
        //========== Game에서 호출될 함수 ==========


        public void WrongInput()
        {
            Console.Clear();
            Console.WriteLine(
                ".................::-=++****++=-::.................\r\n" +
                "............::=*#@@@@@@@@@@@@@@@@#*=::............\r\n" +
                ".........::+%@@@@@@@@@@@@@@@@@@@@@@@@%+::.........\r\n" +
                ".......:-#@@@@@@@@@@%#**++**#%@@@@@@@@@@#-:.......\r\n" +
                ".....:-#@@@@@@@%*=::..........::=*%@@@@@@@#-:.....\r\n" +
                "....:*@@@@@@@@@=:.................:-*@@@@@@@*:....\r\n" +
                "...:#@@@@@@@@@@@%=:..................:*@@@@@@#:...\r\n" +
                "..:%@@@@@%+%@@@@@@%=:.................:-%@@@@@%:..\r\n" +
                ".:#@@@@@%:.:=%@@@@@@%=:.................:%@@@@@#:.\r\n" +
                ".=@@@@@@:....:=%@@@@@@%=:................:@@@@@@=.\r\n" +
                ":#@@@@@+.......:=%@@@@@@%=:...............+@@@@@#:\r\n" +
                ":@@@@@@:.........:=%@@@@@@%=:.............:@@@@@@:\r\n" +
                ":@@@@@@:...........:=%@@@@@@%=:...........:@@@@@@:\r\n" +
                ":@@@@@@:.............:=%@@@@@@%=:.........:@@@@@@:\r\n" +
                ":#@@@@@+...............:=%@@@@@@%=:.......+@@@@@#:\r\n" +
                ".=@@@@@@:................:=%@@@@@@%=:....:@@@@@@=.\r\n" +
                ".:#@@@@@%:.................:=%@@@@@@%=:.:%@@@@@#:.\r\n" +
                "..:%@@@@@%-:.................:=%@@@@@@%+%@@@@@%:..\r\n" +
                "...:%@@@@@@*:..................:=%@@@@@@@@@@@#:...\r\n" +
                "....:*@@@@@@@*-:.................:=@@@@@@@@@*:....\r\n" +
                ".....:-#@@@@@@@%*=::..........::=*%@@@@@@@#-:.....\r\n" +
                ".......:-#@@@@@@@@@@%#**++**#%@@@@@@@@@@#-:.......\r\n" +
                ".........::+%@@@@@@@@@@@@@@@@@@@@@@@@%+::.........\r\n" +
                "............::=*#@@@@@@@@@@@@@@@@#*=::............");
            Console.WriteLine("=================잘못된 입력입니다.=================");
            Console.Write("아무 키나 눌러서 돌아가기...");
            Console.ReadLine();
        }
    }
}
