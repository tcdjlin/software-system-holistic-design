namespace BoardGameFramework;

abstract class GameRunnerFW{
    public abstract UserInterfaceFW UI {get; set;}
    public abstract LogicFW logic {get; set;}

    protected abstract int gameMode {get; set;}
    
    #region ------ Methods: Game Flow ------
    public abstract void initGame();

    protected abstract PlayerFW[] initializePlayers();

    protected bool isNewGame(){
        Console.WriteLine("Select game state:");
        Console.WriteLine(" - To start a new game, press <enter>.");
        Console.WriteLine(" - To load a game from file, enter <load>.");
        Console.Write("> ");
        string input = Console.ReadLine() ?? "";
        UI.input.clearConsole();
        if(input == "load"){
            return false;
        }else return true;
    }
    
    public abstract void runGame();

    protected abstract bool endGame();
    #endregion
}