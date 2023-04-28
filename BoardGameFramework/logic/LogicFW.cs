namespace BoardGameFramework;

abstract class LogicFW{
    public PlayerFW[] players = new PlayerFW[2];
    public GameHistory history = new GameHistory();
    public abstract ValidatorFW validator {get; set;}



    #region ------ Methods: Players ------
    public void playersAssign(PlayerFW[] players){
        this.players = players;
    }

    public bool handleComputerMove(int currentMoveIndex, Dictionary<string, List<int>> availableInfo, GameHistory history){
        List<int> availableCells = availableInfo["cells"];
        List<int> availableSymbols = availableInfo["symbols"];
        Dictionary<string, string> move = new Dictionary<string, string>();
        Random rand = new Random();
        int selectedCell = availableCells[rand.Next(availableCells.Count)];
        int selectedSymbol = availableSymbols[rand.Next(availableSymbols.Count)];

        move["cell"] = (selectedCell - 1).ToString();
        move["symbol"] = selectedSymbol.ToString();

        if(currentMoveIndex % 2 == 0){
            if(this.players[0].getPlayerInfo()["type"] == "computer"){
                Console.WriteLine("Opponent making a move...");
                Thread.Sleep(500);

                this.players[0].makeMove(move, this.history);
                Console.WriteLine(this.players[1].getPlayerInfo()["name"] + ", it's your turn!");
                return true;
            }
        }else{
            if(this.players[1].getPlayerInfo()["type"] == "computer"){
                Console.WriteLine("Opponent making a move...");
                Thread.Sleep(500);

                this.players[1].makeMove(move, this.history);
                Console.WriteLine(this.players[0].getPlayerInfo()["name"] + ", it's your turn!");
                return true;
            }
        }
        return false;
    }
    
    public void handlePlayerMove(int currentMoveIndex, Dictionary<string, string> move, GameHistory history){
        string cellNumber = move["cell"];
        cellNumber = (int.Parse(cellNumber) - 1).ToString();
        move["cell"] = cellNumber;

        if(currentMoveIndex % 2 == 0){
            this.players[0].makeMove(move, this.history);
        }else{
            this.players[1].makeMove(move, this.history);
        }
    }
    #endregion

    
    
}