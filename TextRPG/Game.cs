using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

// 장면을 열거형으로 정의
public enum Scenes
{
    None,
    Town,
    Status,
    Inventory,
    Store,
    Dungeon,
    Inn,
}

namespace TextRPG
{
    class Game
    {
        private Scenes scene = Scenes.Town;
        private Player player;
        private Store store;
        private Inventory inventory;
        private Dungeon dungeon;
        private Inn inn;

        public Game()
        {
            player = new Player();
            store = new Store(player);
            inventory = new Inventory(player);
            dungeon = new Dungeon(player);
            inn = new Inn(player);

            player.Inventory = inventory;
        }

        
        public void Process()
        {
            //현재 장면에 맞춰서 메서드를 호출
            switch (scene)
            {
                case Scenes.None:
                    scene = Scenes.Town;
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

                case Scenes.Store:
                    Store(); 
                    break;

                case Scenes.Dungeon:
                    Dungeon(); 
                    break;

                case Scenes.Inn:
                    Inn();
                    break;
            }
        }

        private void Town()
        {
            //입력 데이터
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
            Console.WriteLine("4. 던전 들어가기");
            Console.WriteLine("5. 여관에서 휴식하기");


            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">>");
            
            //입력 값에 따라서 현재 장면을 전환한다.
            playerInput = Console.ReadLine();
            switch (playerInput)
            {
                case "1":
                    scene = Scenes.Status;
                    break;

                case "2":
                    scene = Scenes.Inventory;
                    break;

                case "3":
                    scene = Scenes.Store;
                    break;

                case "4":
                    scene = Scenes.Dungeon;
                    break;

                case "5":
                    scene = Scenes.Inn;
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
                    scene = Scenes.Town;
                    break;
            }
        }

        private void Inventory()
        {
            string playerInput = "";

            inventory.ShowInventory();

            inventory.ShowItemList();

            inventory.ShowInventoryMenu();

            playerInput = Console.ReadLine();

            inventory.SelectMenu(playerInput, ref scene);
        }

        private void Store()
        {
            string playerInput = "";

            store.ShowStore();

            Console.WriteLine("\n[보유 골드]");
            Console.WriteLine($"{player.Gold} G");

            store.ShowItemList();

            store.ShowStoreMenu();

            playerInput = Console.ReadLine();

            store.SelectMenu(playerInput, ref scene);

        }

        private void Dungeon()
        {
            string playerInput = "";

            dungeon.ShowDungeon();

            dungeon.ShowDungeonResult();

            dungeon.ShowDungeonMenu();

            playerInput = Console.ReadLine();

            dungeon.SelectMenu(playerInput, ref scene);
        }

        public void Inn()
        {
            string playerInput = "";

            inn.ShowInn();

            inn.ShowInnMenu();

            playerInput = Console.ReadLine();

            inn.SelectMenu(playerInput, ref scene);
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
