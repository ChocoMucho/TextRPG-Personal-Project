namespace TextRPG
{
    

    public enum PlayerClass
    {
        None,
        Warrior,
        Archer,
        Sorcerer,
    }

    //텍스트 RPG에서는 인벤토리, 상점 등 모든 것이 씬이기 때문에 아래처럼 열거형으로 나누었음.
    public enum Scenes
    {
        None,
        Town,
        Status,
        Inventory,
        EquipManagement,
        Store,
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(100, 50);
            Game game = new Game();
            while(true)
            {
                game.Process();
            }
        }
    }
}
