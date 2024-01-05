using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class Inn
    {
        Player player;
        public Inn(Player player)
        {
            this.player = player;
        }

        // =========Game에서 호출할 함수들=========
        public void ShowInn()
        {
            Console.Clear();
            Console.WriteLine(
                "               :*@#:  @%##@*       \r\n" +
                "             -##- -##=@=  ##       \r\n" +
                "          .=%*:     .*@*. ##       \r\n" +
                "        :*%+.         .=%*%#       \r\n" +
                "      -#%-               -#%-      \r\n" +
                "    =%*:                   :*%=.   \r\n" +
                " .+%+.                       .+%*. \r\n" +
                "*@@#############################@@*\r\n" +
                "     .@-                   -@:     \r\n" +
                "     .@-  -+==+=           :@:     \r\n" +
                "     .@-  ##::+@           -@:     \r\n" +
                "     .@-  #*  =@   @%####@:-@:     \r\n" +
                "     .@-  #*  =@   @+   .@-:@:     \r\n" +
                "     .@-  *%###%   @=   :@--@:     \r\n" +
                "     .@-           @=   :@-:@:     \r\n" +
                "     .@-           @+   :@-:@:     \r\n" +
                "     .@=:::::::::::@*:::-@==@:     \r\n");

            Console.WriteLine("================ 여관에서 휴식하기 ================");
            Console.WriteLine($"500 Gold를 내면 여관에서 체력을 회복할 수 있습니다. \n(보유 골드 : {player.Gold})");
            Console.WriteLine("==================================================");
        }

        public void ShowInnMenu()
        {
            Console.WriteLine("\n1. 휴식하기");
            Console.WriteLine("0. 나가기");

            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">> ");
        }

        public void SelectMenu(string playerInput, ref Scenes scene)
        {
            switch (playerInput)
            {
                case "0": // 마을로 나가기
                    scene = Scenes.Town;
                    break;
                case "1": // 휴식 시도
                    Rest(player, ref scene);
                    break;
            }
        }
        // =========Game에서 호출할 함수들=========


        //========== 본 클래스의 메서드에서 호출될 함수 ==========
        //휴식 기능 메서드
        public void Rest(Player player, ref Scenes scene)
        {
            //현재 체력이 100이면
            if(player.Hp >= 100) 
            {
                Console.WriteLine("\n체력이 이미 100입니다.");
                Console.Write("아무 키나 입력하세요...");
                Console.ReadLine();
            }
            //골드가 500이상이면 휴식 성공
            else if (player.Gold >= 500)
            {
                player.Hp = 100;
                player.Gold -= 500;
                ShowRest();
            }
            //골드 500미만이면 장면 Town으로 전환
            else
            {
                scene = Scenes.Town;
                ShowKicked();
            }
        }

        //휴식 성공 시에 출력되는 함수
        public void ShowRest()
        {
            Console.Clear();
            Console.WriteLine(
                "                .===++++++**+-                    \r\n" +
                "                #%%%%%%%%%%%%%                    \r\n" +
                "                 :-:::-#%%%%+.                    \r\n" +
                "                    .+%%%%+.                      \r\n" +
                "                  .*%%%%+.                        \r\n" +
                "                 =%%%%%=::---:                    \r\n" +
                "                *%%%%%%%%%%%%%*                   \r\n" +
                "                :*********+++=.                   \r\n" +
                "                               +*#####%#-         \r\n" +
                "                               :----+%%#.         \r\n" +
                "                                   =%%=           \r\n" +
                "                                 :#%#:            \r\n" +
                "                                +%%%+=++=         \r\n" +
                "                                =******++.        \r\n" +
                "                                                  \r\n" +
                "                    =*####+                       \r\n" +
                "                      .*%+                        \r\n" +
                "                     =%#:                         \r\n" +
                "                    +%%####=                      \r\n");

            Console.WriteLine("동서고금 최고의 명약은 잠입니다..\n");
            Console.Write("엔터 눌러서 돌아가기...");
            Console.ReadLine();
        }

        //휴식 실패하면 출력되는 함수
        public void ShowKicked()
        {
            Console.Clear();
            Console.WriteLine(
                "             =.:-        -==-           \r\n" +
                "            .@.*#      =%-::=%-         \r\n" +
                "            .@:+#     .@:    **         \r\n" +
                "            .@.**     +%    .@.         \r\n" +
                "                   =*=.##=-+%-          \r\n" +
                "                 .#*.   .--:   =:.=     \r\n" +
                "                :%=   -.  %+   @-+%     \r\n" +
                "               =%-   +%. :@-   %-=@     \r\n" +
                "              +%.   =%. .%-    @:=#     \r\n" +
                "            .**    :@.  *%:             \r\n" +
                "           :%=     .#*:  :+#+:          \r\n" +
                "           %%*****+++%##+:  :*#:        \r\n" +
                "           %=  :. ..:=%::+#+:.#*        \r\n" +
                "           *#  ##.    :%*  -++-         \r\n" +
                "           =@   -@#-    +#              \r\n" +
                "          :*@.  =@.%*   =#              \r\n" +
                "      .=*#+:    %+ %=   %=              \r\n" +
                "    :%*-    .-+#* .@:  -@.              \r\n" +
                "    +%::-+**+-.   -@   #*               \r\n" +
                "     :==:.        =%  .@:               \r\n" +
                "                  .*#*#-                \r\n");

            

            Console.WriteLine("돈이 없다면 오늘은 노숙입니다..\n");
            Console.Write("엔터 눌러서 마을로 돌아가기...");
            Console.ReadLine();
        }
        //========== 본 클래스의 메서드에서 호출될 함수 ==========
    }
}
