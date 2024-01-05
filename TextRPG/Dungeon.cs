using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    public enum DungeonDifficulty
    {
        Easy,
        Normal,
        Hard,
    }

    public class Dungeon
    {
        public Player player;
        DungeonState state; //메인 / 성공 / 실패
        private int[] recommendedDefense = { 5, 11, 17 }; //DungeonDifficulty와 한 쌍

        // 성공, 실패 
        int hpSave;
        int goldSave;
        string difficultySave = "";

        public Dungeon(Player player)
        {
            state = DungeonState.Main;
            this.player = player;
            hpSave = player.Hp;
            goldSave = player.Gold;
        }

        public void ShowDungeon()
        {
            Console.Clear();
            Console.WriteLine(
                "                                        \r\n" +
                "              .+*##%%##*+.              \r\n" +
                "         :+*. -@@@@@@@@@@: :*+.         \r\n" +
                "       =%@@@%  *@@@@@@@@= .@@@@%=       \r\n" +
                "     =@@@@@@@#  %@@@@@@#  %@@@@@@%-     \r\n" +
                "     -#@@@@@@@+ .@@@@@@. *@@@@@@@#-     \r\n" +
                "  .+=.  -#@@@@%  :++++:  %@@@@*-  .=+.  \r\n" +
                "  %@@@%+:  -=:     ::     :=:  :+%@@@#  \r\n" +
                " +@@@@@@@%+    =: :@@. -=    *@@@@@@@@- \r\n" +
                " %@@@@@@@@#   @@- :@@. =@#   %@@@@@@@@# \r\n" +
                " =********:   @@- :@@. =@%   :********= \r\n" +
                "  ........    @@- :@@. =@%    ........  \r\n" +
                " @@@@@@@@@=   @@- :@@. =@%   +@@@@@@@@% \r\n" +
                " @@@@@@@@@=   @@- :@@. =@%   +@@@@@@@@@ \r\n" +
                " @@@@@@@@@=   @@- :@@. =@%   +@@@@@@@@@ \r\n" +
                " :=======-    @@- :@@. =@%   .-=======: \r\n" +
                " :=======-    @@- :@@. =@%   .-=======: \r\n" +
                " @@@@@@@@@=   @@- :@@. =@%   +@@@@@@@@@ \r\n" +
                " @@@@@@@@@=   @@- .@@. =@#   +@@@@@@@@@ \r\n" +
                " @@@@@@@@@=                  +@@@@@@@@% \r\n");

            switch (state)
            {
                case DungeonState.Main:
                    Console.WriteLine("=============== 던전 입장 ===============");
                    Console.WriteLine(" 던전으로 들어가기전 활동을 할 수 있습니다.");
                    Console.WriteLine("========================================");
                    break;
                case DungeonState.Clear:
                    Console.WriteLine("============== 클리어 성공 ==============");
                    Console.WriteLine("               축하합니다!               ");
                    Console.WriteLine($"   {difficultySave} 던전을 클리어 하였습니다.");
                    Console.WriteLine("========================================");
                    break;
                case DungeonState.Failure:
                    Console.WriteLine("============== 클리어 실패 ==============");
                    Console.WriteLine($"   {difficultySave} 던전 클리어에 실패했습니다...");
                    Console.WriteLine("========================================");
                    break;
                default:
                    break;
            }
        }

        public void ShowDungeonResult() //던전 도전 성공, 실패 시에 뜨는 화면
        {
            switch (state)
            {
                case DungeonState.Main: //아무런 출력 없음
                    break;
                case DungeonState.Clear:
                    Console.WriteLine("[탐험 결과]");
                    Console.WriteLine($"[체력 {hpSave} -> {player.Hp}]");
                    Console.WriteLine($"[Gold {goldSave} -> {player.Gold}]");
                    break;
                case DungeonState.Failure:
                    Console.WriteLine("[탐험 결과]");
                    Console.WriteLine($"[체력 {hpSave} -> {player.Hp}]");
                    Console.WriteLine("아무런 보상도 얻지 못했습니다.");
                    break;
            }
        }

        public void ShowDungeonMenu()
        {
            switch (state)
            {
                case DungeonState.Main:
                    Console.WriteLine("1. 쉬운 던전\t\t 방어력 5 이상 권장");
                    Console.WriteLine("2. 일반 던전\t\t 방어력 11 이상 권장");
                    Console.WriteLine("3. 어려운 던전\t\t 방어력 17 이상 권장");
                    Console.WriteLine("0. 나가기");
                    break;
                case DungeonState.Clear:
                case DungeonState.Failure:
                    Console.WriteLine("\n0. 나가기");
                    break;
            }

            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.WriteLine(">>>");
        }

        public void SelectMenu(string playerInput, ref Scenes scene)
        {
            int index = 0; //던전 난이도 선택용
            if (state == DungeonState.Main)
            {
                switch (playerInput)
                {
                    case "0": // 마을로 나가기
                        scene = Scenes.Town;
                        break;
                    case "1": // 쉬운 던전
                        EnterDungeon(DungeonDifficulty.Easy);
                        break;
                    case "2": // 일반 던전
                        EnterDungeon(DungeonDifficulty.Normal);
                        break;
                    case "3": // 어려운 던전
                        EnterDungeon(DungeonDifficulty.Hard);
                        break;
                }
            }
            //클리어 or 실패라서 던전 메인으로 가는 선택지만 있음
            else if (state == DungeonState.Clear || state == DungeonState.Failure)
            {
                switch (playerInput)
                {
                    case "0": // Main으로 돌아가기
                        state = DungeonState.Main;
                        break;
                }
            }
        }

        public void EnterDungeon(DungeonDifficulty difficulty) //던전 결과 뱉는 로직
        {
            Random random = new Random();

            int num = random.Next(1, 101);

            int damaged = 0;
            int reward = 0;

            //결과 화면에 쓰일 데이터 저장
            hpSave = player.Hp;
            goldSave = player.Gold;
            if (difficulty == DungeonDifficulty.Easy)
                difficultySave = "쉬운";
            else if (difficulty == DungeonDifficulty.Easy)
                difficultySave = "보통";
            else if (difficulty == DungeonDifficulty.Hard)
                difficultySave = "어려운";

            if ((recommendedDefense[(int)difficulty] > player.Defence) && (num <= 40)) //권장 방어력 미만 && 40퍼 당첨
            {
                //체력 절반 감소
                damaged = player.Hp / 2;
                player.Hp -= damaged;

                //죽었는지 확인
                if (player.IsDead())
                {
                    player.Hp = 0;
                }

                //던전 상태 -> 실패
                state = DungeonState.Failure;
                return;
            }
            else // 권장 방어력 이상 or 40퍼 미당첨
            {
                //데미지 계산
                damaged = random.Next(20, 35);//받은 데미지 저장(표시 용도)
                damaged += recommendedDefense[(int)difficulty] - player.Defence; //권장 방어력 - 내 방어력
                player.Hp -= damaged;

                //보상 계산
                int playerAttack = (int)player.Attack + (int)player.AttackBonus;
                switch (difficulty)
                {
                    case DungeonDifficulty.Easy:
                        reward = (1000 / 100) * random.Next(playerAttack, playerAttack * 2);
                        reward += 1000;
                        break;
                    case DungeonDifficulty.Normal:
                        reward = (1700 / 100) * random.Next(playerAttack, playerAttack * 2);
                        reward += 1700;
                        break;
                    case DungeonDifficulty.Hard:
                        reward = (2500 / 100) * random.Next(playerAttack, playerAttack * 2);
                        reward += 2500;
                        break;
                }

                //플레이어가 죽었는지 확인
                if (player.IsDead())
                {
                    player.Hp = 0;
                    state = DungeonState.Failure;
                    return;
                }

                //보상 등 클리어 후 처리
                state = DungeonState.Clear;
                player.Gold += reward;
                ++player.ClearCount;
                player.LevelUp();
            }
        }

    }
}
