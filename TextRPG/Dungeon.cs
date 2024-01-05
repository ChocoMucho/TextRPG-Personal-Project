using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    // 던전의 현재 상태를 나타낼 용도
    public enum DungeonState
    {
        Main,
        Clear,
        Failure
    }

    // 던전 난이도
    public enum DungeonDifficulty
    {
        Easy,
        Normal,
        Hard,
    }

    public class Dungeon
    {
        public Player player;

        // 열거형 값이 배열의 인덱스 역할을 함
        DungeonState    state;
        private int[]   recommendedDefense = { 5, 11, 17 }; 

        //(체력,재화,난이도)를 클리어, 실패 화면에 띄워줄 용도
        int             hpSave;
        int             goldSave;
        string          difficultySave = "";

        public Dungeon(Player player)
        {
            state = DungeonState.Main;
            this.player = player;
            hpSave = player.Hp;
            goldSave = player.Gold;
        }

        //========== Game에서 호출될 함수 ==========
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
                // 던전의 메인 상태 시 출력
                case DungeonState.Main:
                    Console.WriteLine("=============== 던전 입장 ===============");
                    Console.WriteLine(" 던전으로 들어가기전 활동을 할 수 있습니다.");
                    Console.WriteLine("========================================");
                    break;

                //던전의 클리어 상태 시 출력
                case DungeonState.Clear:
                    Console.WriteLine("============== 클리어 성공 ==============");
                    Console.WriteLine("               축하합니다!               ");
                    Console.WriteLine($"   {difficultySave} 던전을 클리어 하였습니다.");
                    Console.WriteLine("========================================");
                    break;

                //던전의 클리어 실패 상태 시 출력
                case DungeonState.Failure:
                    Console.WriteLine("============== 클리어 실패 ==============");
                    Console.WriteLine($"   {difficultySave} 던전 클리어에 실패했습니다...");
                    Console.WriteLine("========================================");
                    break;
                default:
                    break;
            }
        }

        //던전 도전 결과 출력
        public void ShowDungeonResult() 
        {
            switch (state)
            {
                //아무런 출력 없음
                case DungeonState.Main: 
                    break;

                //던전 클리어 상태 시 출력
                case DungeonState.Clear:
                    Console.WriteLine("[탐험 결과]");
                    Console.WriteLine($"[체력 {hpSave} -> {player.Hp}]");
                    Console.WriteLine($"[Gold {goldSave} -> {player.Gold}]");
                    break;
                //던전 클리어 실패 시 출력
                case DungeonState.Failure:
                    Console.WriteLine("[탐험 결과]");
                    Console.WriteLine($"[체력 {hpSave} -> {player.Hp}]");
                    Console.WriteLine("아무런 보상도 얻지 못했습니다.");
                    break;
            }
        }

        //던전 선택지 출력
        public void ShowDungeonMenu()
        {
            switch (state)
            {
                // 메인 상태 시 출력
                case DungeonState.Main:
                    Console.WriteLine("1. 쉬운 던전\t\t 방어력 5 이상 권장");
                    Console.WriteLine("2. 일반 던전\t\t 방어력 11 이상 권장");
                    Console.WriteLine("3. 어려운 던전\t\t 방어력 17 이상 권장");
                    Console.WriteLine("0. 나가기");
                    break;
                // 클리어, 실패 시 출력
                case DungeonState.Clear:
                case DungeonState.Failure:
                    Console.WriteLine("\n0. 나가기");
                    break;
            }

            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.WriteLine(">>>");
        }

        //사용자 입력을 받아 로직 진행
        public void SelectMenu(string playerInput, ref Scenes scene)
        {
            //메인 상태인 경우 
            if (state == DungeonState.Main)
            {
                switch (playerInput)
                {
                    //장면 마을로 변경
                    case "0": 
                        scene = Scenes.Town;
                        break;

                    // 난이도별 던전 입장
                    case "1": 
                        EnterDungeon(DungeonDifficulty.Easy);
                        break;
                    case "2":
                        EnterDungeon(DungeonDifficulty.Normal);
                        break;
                    case "3":
                        EnterDungeon(DungeonDifficulty.Hard);
                        break;
                }
            }
            //클리어, 실패 상태인 경우 
            else if (state == DungeonState.Clear || state == DungeonState.Failure)
            {
                switch (playerInput)
                {
                    // 메인 상태로 돌아가는 선택지만 존재
                    case "0": 
                        state = DungeonState.Main;
                        break;
                }
            }
        }
        //========== Game에서 호출될 함수 ==========


        //========== 본 클래스의 메서드에서 호출될 함수 ==========
        //난이도별 던전 입장 함수
        public void EnterDungeon(DungeonDifficulty difficulty)
        {
            Random random = new Random();

            int num = random.Next(1, 101);

            int damaged = 0;
            int reward = 0;

            //결과 화면에 쓰일 플레이어 원본 데이터와 던전 난이도 저장
            hpSave = player.Hp;
            goldSave = player.Gold;
            if (difficulty == DungeonDifficulty.Easy)
                difficultySave = "쉬운";
            else if (difficulty == DungeonDifficulty.Easy)
                difficultySave = "보통";
            else if (difficulty == DungeonDifficulty.Hard)
                difficultySave = "어려운";

            //권장 방어력 이하 and 40퍼센트 확률 당첨
            if ((recommendedDefense[(int)difficulty] >= player.Defence) && (num <= 40)) 
            {
                //체력 절반 감소
                damaged = player.Hp / 2;
                player.Hp -= damaged;

                //죽었는지 확인 후 체력 조정
                if (player.IsDead())
                    player.Hp = 0;

                //던전 상태 -> 실패
                state = DungeonState.Failure;
                return;
            }
            // 권장 방어력 이상 or 40퍼 미당첨
            else
            {
                //받은 데미지 계산
                damaged = random.Next(20, 35);
                damaged += recommendedDefense[(int)difficulty] - player.Defence;
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

                //플레이어가 죽었는지 확인 후 죽었으면 클리어 실패
                if (player.IsDead())
                {
                    player.Hp = 0;
                    state = DungeonState.Failure;
                    return;
                }

                //클리어 시의 후처리
                state = DungeonState.Clear;
                player.Gold += reward;
                ++player.ClearCount;
                player.LevelUp();
            }
        }
        //========== 클래스의 메서드에서 호출될 함수 ==========

    }
}
