using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TextRPG
{
    
    internal class Store // 상점 상태에 따라서 다른 내용을 보여주거나 다른 행동을 하게 설계함
    {
        public StoreState state = StoreState.Main;
        public List<Item> items = new List<Item>();
        public Player player = Player.Instance;

        public Store() //파일 저장이 없어 하드코딩
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

        public void ShowStore()
        {
            //출력
            Console.Clear();
            Console.WriteLine(
                ":##*                             .-:    \r\n" +
                "-@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@%.  \r\n" +
                "-@@@######%@@@##########@@@######@@@#   \r\n" +
                ".++=   :===+++==========+++===:   .     \r\n" +
                "       *@@%++*@@@@@@@@@@+++%@@+         \r\n" +
                "       *@@@@@@@@@@@@@@@@@@@@@@+         \r\n" +
                "       *@@@@@%%%%%%%%%%%%@@@@@+         \r\n" +
                "       *@@@@:            =@@@@+         \r\n" +
                "       *@@@@+============*@@@@+         \r\n" +
                "       *@@@@@@@@@@@@@@@@@@@@@@+         \r\n" +
                "       *@@@@=............+@@@@+         \r\n" +
                "       *@@@@-............=@@@@+         \r\n" +
                "       *@@@@@@@@@@@@@@@@@@@@@@+         \r\n" +
                "       *@@@@@@@@@@@@@@@@@@@@@@+         \r\n" +
                "       :*%@@@@@@@@@@@@@@@@@@%+:         \r\n" +
                "           :=*%@@@@@@@@%*=:             \r\n" +
                "                @@@@@@                  \r\n");

            switch (state)
            {
                case StoreState.Main:
                    Console.WriteLine("================= 상점 =================");
                    Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
                    Console.WriteLine("========================================");
                    break;
                case StoreState.Buy:
                    Console.WriteLine("========== 상점 - 아이템 구매 ==========");
                    Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
                    Console.WriteLine("========================================");
                    break;
                case StoreState.Sell:
                    Console.WriteLine("========== 상점 - 아이템 판매 ==========");
                    Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
                    Console.WriteLine("========================================");
                    break;
                default:
                    
                    break;
            }
        }
        public void ShowItemList()
        {
            Console.WriteLine("\n[아이템 목록]");
            int index = 0;
            switch (state)
            {
                
                case StoreState.Main: //상점 메인에 보이는 아이템 리스트
                    foreach (Item item in items)
                    {
                        string itemInfo = $"- {item.Name} | ";

                        if (item.Type == ItemType.Weapon)
                            itemInfo += $" 공격력 +{(item as Weapon).Attack}| ";
                        else if (item.Type == ItemType.Armor)
                            itemInfo += $" 방어력 +{(item as Armor).Defence}| ";
                        else if (item.Type == ItemType.Shield)
                            itemInfo += $" 방어력 +{(item as Shield).Defence}| ";

                        itemInfo += item.Desc;

                        itemInfo += item.Isbought ? "| 구매 완료" : $"| {item.Price} G";

                        Console.WriteLine(itemInfo);
                    }
                    break;
                case StoreState.Buy: //상점 - 구매하기에서 보이는 아이템 리스트
                    index = 0;
                    foreach (Item item in items)
                    {
                        string itemInfo = $"- {++index} {item.Name} | ";

                        if (item.Type == ItemType.Weapon)
                            itemInfo += $" 공격력 +{(item as Weapon).Attack}| ";
                        else if (item.Type == ItemType.Armor)
                            itemInfo += $" 방어력 +{(item as Armor).Defence}| ";
                        else if (item.Type == ItemType.Shield)
                            itemInfo += $" 방어력 +{(item as Shield).Defence}| ";

                        itemInfo += item.Desc;

                        itemInfo += item.Isbought ? "| 구매 완료" : $"| {item.Price} G";

                        Console.WriteLine(itemInfo);
                    }
                    break;
                case StoreState.Sell: //상점 - 판매하기에서 보이는 내 아이템 리스트
                    index = 0;
                    foreach (Item item in player.Inventory)
                    {
                        string itemInfo = $"- {++index} {item.Name} | ";

                        if (item.Type == ItemType.Weapon)
                            itemInfo += $" 공격력 +{(item as Weapon).Attack}| ";
                        else if (item.Type == ItemType.Armor)
                            itemInfo += $" 방어력 +{(item as Armor).Defence}| ";
                        else if (item.Type == ItemType.Shield)
                            itemInfo += $" 방어력 +{(item as Shield).Defence}| ";

                        itemInfo += item.Desc;

                        itemInfo += $"| {item.Price} G";

                        Console.WriteLine(itemInfo);
                    }
                    break;
                default:
                    break;
            }

        }
        public void ShowStoreHandle()
        {
            switch(state)
            {
                case StoreState.Main:
                    Console.WriteLine();
                    Console.WriteLine("1. 아이템 구매");
                    Console.WriteLine("2. 아이템 판매");
                    Console.WriteLine("0. 나가기\n");
                    Console.WriteLine("원하시는 행동을 입력해주세요.");
                    Console.Write(">>");
                    break;

                case StoreState.Buy:
                    Console.WriteLine();
                    Console.WriteLine("0. 나가기\n");
                    Console.WriteLine("원하시는 행동을 입력해주세요.");
                    Console.Write(">>");
                    break;

                case StoreState.Sell:
                    Console.WriteLine();
                    Console.WriteLine("0. 나가기\n");
                    Console.WriteLine("원하시는 행동을 입력해주세요.");
                    Console.Write(">>");
                    break;
            }    
        }
        public void HandleStore(string playerInput, ref Scenes scenes)
        {
            int num = 0;

            if(state == StoreState.Main)// 상점 상태 Main
            {
                switch (playerInput)
                {
                    case "0": // 마을로 나가기
                        scenes = Scenes.Town;
                        state = StoreState.Main;
                        break;
                    case "1": // 구매 화면
                        state = StoreState.Buy;
                        break;
                    case "2": // 판매 화면
                        state = StoreState.Sell;
                        break;
                }
            }
            else if (state == StoreState.Buy)// 상점 상태 Buy
            {
                switch (playerInput)
                {
                    case "0": // 상점으로 나가기
                        scenes = Scenes.Store;
                        state = StoreState.Main;
                        break;

                    default:
                        if (int.TryParse(playerInput, out num) && 0 < num && num <= items.Count)
                        {
                            player.Buy(items[--num]);
                        }
                        else
                        {
                            WrongInput();
                        }
                        break;
                }
            }
            else if (state == StoreState.Sell)// 상점 상태 Sell
            {
                switch (playerInput)
                {
                    case "0": // 상점으로 나가기
                        scenes = Scenes.Store;
                        state = StoreState.Main;
                        break;

                    default:
                        if (int.TryParse(playerInput, out num) && 0 < num && num <= player.Inventory.Count)
                        {
                            player.Sell(player.Inventory[--num]);
                        }
                        else
                        {
                            WrongInput();
                        }
                        break;
                }
            }

        }

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
