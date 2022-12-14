namespace Basic_RPG_Game;

public class MainMenu
{
    public static void Main()
    {
        Console.WriteLine("Welcome to the game!");
        Console.WriteLine("What would you like to do?");
        Console.WriteLine("1. Start a new game");
        Console.WriteLine("2. Load a game");
        Console.WriteLine("0. Exit");
        var choice = Console.ReadLine();

        if (choice == null) return;
        switch (int.Parse(choice))
        {
            case 1:
                Console.WriteLine("Starting a new game...");
                CreateNewGame.Main();
                break;
            case 2:
                Console.WriteLine("Loading a game...");
                break;
            //loadGame.Main();

            case 0:
                Console.WriteLine("Exiting...");
                Environment.Exit(0);
                break;
        }
    }
}