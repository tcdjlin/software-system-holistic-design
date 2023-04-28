using bgf = BoardGameFramework;

namespace NumericalTicTacToe;

class UserInterface : bgf.UserInterfaceFW{
    public override bgf.BoardFW board {get; set;}
    public override bgf.MessageFW message {get; set;}
    public override bgf.InputHandlerFW input {get; set;}

    public UserInterface(){
        this.board = new Board();  
        this.message = new bgf.MessageFW();
        this.input = new InputHandler();
    }

    public override void getHelp(int currentMoveIndex){

        while(true){
            input.clearConsole();
            Console.WriteLine("Enter a help option:");
            Console.WriteLine(" - Enter <general> to view guide of the game.");
            Console.WriteLine(" - Enter <available> to view available cells, symbols, and examples.");
            Console.WriteLine(" - Enter <exit> to leave help menu.");
            Console.Write("> ");
            string option = Console.ReadLine() ?? "";

            if(string.IsNullOrWhiteSpace(option)){
                continue;
            }

            if(option == "exit"){
                break;
            }

            if(option == "general"){
                message.displayMessage("gameGuide");
                Console.WriteLine(new string('-', 50));
                message.displayMessage("cellGuide");
                Console.WriteLine(new string('-', 50));
                message.displayMessage("moveGuide");
                Console.WriteLine(" - To redo -> enter 'redo'\n - To undo -> enter 'undo'\n - To save -> enter 'export'");
                Console.WriteLine("> Press <enter> to go back");
                Console.ReadLine();
            }
            
            if(option == "available"){
                List<int> availableCells = board.getAvailableInfo(currentMoveIndex)["cells"];
                List<int> availableSymbols = board.getAvailableInfo(currentMoveIndex)["symbols"];

                Console.WriteLine("Available cell number: " + string.Join(", ", availableCells));
                Console.WriteLine("Available symbols: " + string.Join(", ", availableSymbols));
                Random rand = new Random();

                string exampleMove = availableCells[rand.Next(availableCells.Count)] + ":" + availableSymbols[rand.Next(availableSymbols.Count)];
                Console.WriteLine(" - Move command format: < m:cell:symbol >");
                Console.WriteLine(" - An example of inputing a move command: < m:" + exampleMove + " >");
                Console.WriteLine("> Press <enter> to go back");
                Console.ReadLine();
            }
        }
    }

    
}