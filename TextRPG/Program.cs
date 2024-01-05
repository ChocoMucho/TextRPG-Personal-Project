namespace TextRPG
{
    public enum PlayerClass
    {
        None,
        Warrior,
        Archer,
        Sorcerer,
    }

    

    public enum StoreState
    {
        Main,
        Buy,
        Sell,
    }

    public enum ItemType
    {
        Weapon,
        Armor,
        Shield,
    }

    

    


    internal class Program
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(100, 50);
            //Player player= new Player();  
            Game game = new Game();
            while(true)
            {
                game.Process();
            }
        }
    }
}
