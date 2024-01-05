using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;
using static System.Runtime.InteropServices.JavaScript.JSType;

//상점의 상태를 나타내는 열거형
public enum StoreState
{
    Main,
    Buy,
    Sell,
}

namespace TextRPG
{
    
    internal class Store
    {
        public StoreState   state = StoreState.Main;      
        public List<Item>   items = new List<Item>();     
        public Player       player;

        public Store(Player player) 
        {
            this.player = player;

            //파일 저장이 없어 하드코딩
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

        //========== Game에서 호출될 함수 ==========
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
                // Main 상태 시 출력
                case StoreState.Main:
                    Console.WriteLine("================= 상점 =================");
                    Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
                    Console.WriteLine("========================================");
                    break;

                // Buy 상태 시 출력
                case StoreState.Buy:
                    Console.WriteLine("========== 상점 - 아이템 구매 ==========");
                    Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
                    Console.WriteLine("========================================");
                    break;

                // Sell 상태 시 출력
                case StoreState.Sell:
                    Console.WriteLine("========== 상점 - 아이템 판매 ==========");
                    Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
                    Console.WriteLine("========================================");
                    break;
                default:
                    
                    break;
            }
        }  

        // 아이템 리스트 출력
        public void ShowItemList()  
        {
            Console.WriteLine("\n[아이템 목록]");
            int index = 0;
            switch (state)
            {
                //Main 상태 시 상점의 아이템 모두 출력
                case StoreState.Main: 
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

                //Buy 상태 시 상점의 아이템 모두 출력
                //선택을 위한 넘버링 추가
                case StoreState.Buy:
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

                //Sell 상태 시 플레이어 인벤토리의 아이템 모두 출력
                case StoreState.Sell: 
                    index = 0;
                    foreach (Item item in player.Inventory.Items)
                    {
                        string itemInfo = $"- {++index} {item.Name} | ";

                        if (item.Type == ItemType.Weapon)
                            itemInfo += $" 공격력 +{(item as Weapon).Attack}| ";
                        else if (item.Type == ItemType.Armor)
                            itemInfo += $" 방어력 +{(item as Armor).Defence}| ";
                        else if (item.Type == ItemType.Shield)
                            itemInfo += $" 방어력 +{(item as Shield).Defence}| ";

                        itemInfo += item.Desc;

                        itemInfo += $"| 재판매 : {item.Price * 0.85f} G";

                        Console.WriteLine(itemInfo);
                    }
                    break;
                default:
                    break;
            }

        }

        // 선택 메뉴 출력
        public void ShowStoreMenu()
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

        // 사용자 입력에 따른 로직 진행
        public void SelectMenu(string playerInput, ref Scenes scene)
        {
            int num = 0;

            // 상점 상태 Main
            if (state == StoreState.Main)
            {
                switch (playerInput)
                {
                    case "0": // 마을로 나가기
                        scene = Scenes.Town;
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

            // 상점 상태 Buy
            else if (state == StoreState.Buy)
            {
                switch (playerInput)
                {
                    case "0": // 상점으로 나가기
                        scene = Scenes.Store;
                        state = StoreState.Main;
                        break;

                    // 입력 값에 맞게 상점 아이템 구매
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

            // 상점 상태 Sell
            else if (state == StoreState.Sell)
            {
                switch (playerInput)
                {
                    case "0": // 상점으로 나가기
                        scene = Scenes.Store;
                        state = StoreState.Main;
                        break;

                    // 입력 값에 맞게 플레이어 아이템 판매
                    default:
                        if (int.TryParse(playerInput, out num) && 0 < num && num <= player.Inventory.Items.Count)
                        {
                            player.Sell(player.Inventory.Items[--num]);
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
