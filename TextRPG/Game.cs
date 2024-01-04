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
        public Player player;
        public Store store;
        public Inventory inventory;

        public Game()
        {
            player = Player.Instance;
            store = new Store();
            inventory = player.inventory;
        }

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

                /*case Scenes.EquipManagement:
                    EquipManagement();
                    break;*/

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

            inventory.ShowInventory();

            inventory.ShowItemList();

            inventory.ShowInventoryHandle();

            playerInput = Console.ReadLine();

            inventory.InventoryHandle(playerInput, ref scenes);
        }

      
        private void Store()
        {
            //데이터
            string playerInput = "";

            store.ShowStore();

            Console.WriteLine("\n[보유 골드]");
            Console.WriteLine($"{player.Gold} G");

            store.ShowItemList();

            store.ShowStoreHandle();

            playerInput = Console.ReadLine();

            store.StoreHandle(playerInput, ref scenes);

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
