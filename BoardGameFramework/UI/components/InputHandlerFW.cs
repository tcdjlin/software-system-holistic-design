namespace BoardGameFramework;
abstract class InputHandlerFW{
    protected string rawInput = "";
    protected Dictionary<string, string> parsedInput = new Dictionary<string, string>();

    public Dictionary<string, string> getParsedInput(){return this.parsedInput;}

    public bool promptInput(){
        Console.Write("> Enter a command: ");
        this.rawInput = Console.ReadLine() ?? "";
        if(string.IsNullOrEmpty(this.rawInput)){
            return false;
        }else{
            return true;
        }
    }
    
    public abstract bool parseInput();

    public void freezePrompt(){
        Console.WriteLine("Press <enter> to continue...");
        Console.ReadLine();
    }

    public void invalidPrompt(){
        Console.WriteLine("Invalid Input.");
        Console.WriteLine(" - Please enter a valid command.");
    }

    public void clearConsole(){
        Console.Clear();
        Thread.Sleep(500);
    }
}