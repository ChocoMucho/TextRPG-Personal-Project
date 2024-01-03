using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;



// 여기서는 로비, 상태창 등으로 가는 함수를 호출
namespace TextRPG
{
    class Game
    {
        Scenes scenes = Scenes.Town;
        Player player = new Player();
        Merchant merchant = new Merchant();
        bool isStoreActive = false;

        public void Process()
        {
            //현재 씬에 따라서 호출
            switch (scenes)
            {
                case Scenes.None:
                    scenes = Scenes.Town;
                    break;

                case Scenes.Town:
                    Town();
                    break;

                case Scenes.Status:
                    Status();
                    break;

                case Scenes.Inventory:
                    Inventory();
                    break;

                case Scenes.EquipManagement:
                    EquipManagement();
                    break;

                case Scenes.Store:
                    Store(); 
                    break;
            }
        }

        private void Town()
        {
            //데이터
            string playerInput = "";

            //출력
            Console.Clear();
            Console.WriteLine("                      ..:::.                      ");
            Console.WriteLine("                     #@@@@@@%.                    ");
            Console.WriteLine("                      *@@@@%                      ");
            Console.WriteLine("                       %%%%.                      ");
            Console.WriteLine("                       *@@#                       ");
            Console.WriteLine("                      .=##+.                      ");
            Console.WriteLine("                   .+%@@%%@@@*:                   ");
            Console.WriteLine("                  -@@@@*#%#@@@@+                  ");
            Console.WriteLine("                  @@%##@@@@%#%@@:                 ");
            Console.WriteLine("                 =##@@@@@@@@@@%%*                 ");
            Console.WriteLine("                 -@*-=+#@@%+=-*@+                 ");
            Console.WriteLine("                 -@@#=. @@. -*@@=                 ");
            Console.WriteLine("                 %@@@@+.@@:-@@@@@.                ");
            Console.WriteLine("                 =@@@@+ #%.-@@@@*                 ");
            Console.WriteLine("            =*#   #@@@+    -@@@#  =.:-            ");
            Console.WriteLine("       ::---@@==.  *@@+    -@@#  --  %  :-.       ");
            Console.WriteLine("    :=++*#@#@@@=.   -%+    -@-  .  -*: :-.  .:    ");
            Console.WriteLine(" .*@@@@@@@@@@@*+#+:   :    .   .:--.     .:--=%+  ");
            Console.WriteLine(" #+-...:-=+*###=  .:.         .    .:--==-:...:=* ");

            Console.WriteLine("====================스파르타마을====================");
            Console.WriteLine("스파르타 마을에 오신 것을 환영합니다.");
            Console.WriteLine("던전으로 들어가기전 활동을 할 수 있습니다.");
            Console.WriteLine("===================================================");

            Console.WriteLine("1. 상태 보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine("3. 상점");

            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">>");
            
            playerInput = Console.ReadLine();
            switch (playerInput)
            {
                case "1":
                    scenes = Scenes.Status;
                    break;

                case "2":
                    scenes = Scenes.Inventory;
                    break;

                case "3":
                    scenes = Scenes.Store;
                    break;

                default:
                    WrongInput();
                    break;
            }

        }

        private void Status()
        {
            //데이터
            string playerInput = "";
            string AttackBonus = player.AttackBonus > 0 ? $"(+{player.AttackBonus})" : "";
            string DefenceBonus = player.DefenceBonus > 0 ? $"(+{player.DefenceBonus})" : "";


            //출력
            Console.Clear();
            Console.WriteLine(
                "               -@@@@@@@@.               \r\n" +
                "                *@@@@@@=                \r\n" +
                "                .@@@@@@                 \r\n" +
                "               :=@@@@@%=-               \r\n" +
                "            -#@@@@@@@@@@@@%=            \r\n" +
                "          .#@@@@@@@@@@@@@@@@%.          \r\n" +
                "          %@@%#@@@@@@@@@@@@@@%          \r\n" +
                "         =@%%@%@@@@@@@@@@@@@@@+         \r\n" +
                "         *@@@@@@@@@@@@@#@@@@@@#         \r\n" +
                "         :#@**%@@@@@@@@++%*+@#-         \r\n" +
                "           ==  :=%@@@@@+.  -+           \r\n" +
                "           %@#=   *@@#   ==%%           \r\n" +
                "           *@@@%. :@@= .%@@#*           \r\n" +
                "           =@@@@# -@@= #@@@@=           \r\n" +
                "           -@@@@@   .  @%@@@-           \r\n" +
                "            -%@@@.    .@%#%-            \r\n" +
                "              +@@-    -@@:              \r\n" +
                "               .**    **.               \r\n");
            Console.WriteLine("================ 상태창 ================");
            Console.WriteLine("       캐릭터의 정보가 표시됩니다.        ");
            Console.WriteLine("========================================");

            Console.WriteLine($"Lv. {player.Level}");
            Console.WriteLine($"{player.Name} ({player.PlayerClass})");
            Console.WriteLine($"공격력 : {player.Attack + player.AttackBonus} {AttackBonus}");
            Console.WriteLine($"방어력 : {player.Defence + player.DefenceBonus} {DefenceBonus}");

            Console.WriteLine($"체력 : {player.Hp}");
            Console.WriteLine($"Gold : {player.Gold}");

            Console.WriteLine("\n0. 나가기");
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">>");
            playerInput = Console.ReadLine();
            switch (playerInput)
            {
                case "0":
                    scenes = Scenes.Town;
                    break;
            }
        }

        private void Inventory()//인벤토리
        {
            //데이터
            string playerInput = "";

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
            Console.WriteLine("============= 인벤토리 =============");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine("===================================");
            Console.WriteLine("\n[아이템 목록]");
            
            foreach(Item item in player.Inventory)
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

            Console.WriteLine("\n1. 장착 관리");
            Console.WriteLine("0. 나가기\n"); //마을로

            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">>");
            playerInput = Console.ReadLine();
            switch (playerInput)
            {
                case "0":
                    scenes = Scenes.Town;
                    break;
                case "1":
                    scenes = Scenes.EquipManagement;
                    break;
            }
        }

        private void EquipManagement()//장착 관리
        {
            //데이터
            string playerInput = "";
            int index = 0;

            Console.Clear();
            Console.WriteLine(
                "                    .:-------:                    \r\n" +
                "                .-++.*+*--**#.**=-                \r\n" +
                "              :=-#+#-+**=++*+=+*#.==.             \r\n" +
                "             +*++:#=-.   :   :-===*+*=            \r\n" +
                "           .+:+*+=.     =@.     :=+=:-+           \r\n" +
                "           **++=-      :@@%       =+#+++          \r\n" +
                "          =-++*=       %%=@#       *==:+.         \r\n" +
                "          *==++       #@: +@+      -***+=         \r\n" +
                "          #***#      +@=   #@-     --+=+=         \r\n" +
                "          =:-===    -@#     %@.    +*+++.         \r\n" +
                "           **+**- :+@@*.   :*@%+  +:*+++          \r\n" +
                "           .*:-++=:::::    .:::::=*++.+           \r\n" +
                "            .+***=+=-.       :-=*:+**=            \r\n" +
                "              :+--***:***-+**:*#++-=.             \r\n" +
                "                .-=#-:**# **+=:*=-                \r\n" +
                "                    .--------:.                   \r\n");

            Console.WriteLine("=============== 인벤토리 - 장착 관리 ===============");
            Console.WriteLine("        보유 중인 아이템을 관리할 수 있습니다.        ");
            Console.WriteLine("===================================================");
            Console.WriteLine("\n[아이템 목록]");

            
            foreach (Item item in player.Inventory)
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

            Console.WriteLine("\n0. 나가기\n"); //인벤토리로

            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">>");
            playerInput = Console.ReadLine();

            switch (playerInput)
            {
                case "0":
                    scenes = Scenes.Inventory;
                    break;

                default:
                    //입력 값이 숫자 and 1 이상 and 인벤토리 총 수 보다 작음
                    if (int.TryParse(playerInput, out index) && 0 < index && index <= player.Inventory.Count)
                    {
                        player.Inventory[--index].Equip(player);
                    }
                    else
                    {
                        WrongInput();
                    }
                    break;

            }

        }

        private void Store()
        {
            //데이터
            string playerInput = "";
            
            int index = 0;

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
            if(!isStoreActive)
                Console.WriteLine("================= 상점 =================");
            else
                Console.WriteLine("========== 상점 - 아이템 구매 ==========");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
            Console.WriteLine("========================================");

            Console.WriteLine("\n[보유 골드]");
            Console.WriteLine($"{player.Gold} G");

            Console.WriteLine("\n[아이템 목록]");

            foreach(Item item in merchant.items)
            {
                string itemInfo = "- ";
                if (isStoreActive)
                    itemInfo = $"{++index} ";

                itemInfo += $"{item.Name}\t";

                if (item.Type == ItemType.Weapon)
                    itemInfo += $"| 공격력 +{(item as Weapon).Attack}| ";
                else if (item.Type == ItemType.Armor)
                    itemInfo += $"| 방어력 +{(item as Armor).Defence}| ";
                else if (item.Type == ItemType.Shield)
                    itemInfo += $"| 방어력 +{(item as Shield).Defence}| ";

                itemInfo += item.Desc;
                if(item.Isbought)
                {
                    itemInfo += "| 구매 완료";
                }
                else
                {
                    itemInfo += $"| {item.Price} G";
                }
                

                Console.WriteLine(itemInfo);
            }

            Console.WriteLine();
            if (!isStoreActive) //상점 구매 비활성화
                Console.WriteLine("1. 아이템 구매");
                
            Console.WriteLine("0. 나가기\n");
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">>");

            playerInput = Console.ReadLine();

            if (!isStoreActive)//구매 비활성화
            {
                switch (playerInput)
                {
                    case "0":
                        scenes = Scenes.Town;
                        break;
                    case "1":
                        isStoreActive = true;
                        break;
                }
            }
            else//구매 활성화
            {
                switch (playerInput)
                {
                    case "0":
                        isStoreActive = false;
                        break;

                    default:
                        
                        if (int.TryParse(playerInput, out index) && 0 < index && index <= merchant.items.Count)
                        {
                            player.Buy(merchant.items[--index]);
                        }
                        else
                        {
                            WrongInput();
                        }
                        break;
                }
            }
        }


        private void WrongInput()
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
