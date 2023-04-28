using bgf = BoardGameFramework;

namespace NumericalTicTacToe;

class Board : bgf.BoardFW{
    
    protected override string[] initBoard(){
        string[] initializedBoard = {" ", " ", " ", " ", " ", " ", " ", " ", " "};
        return initializedBoard;
    }

    protected override void updateBoard(List<Dictionary<string, string>> historyState, int currentMoveIndex)
    {   
        base.gameBoard = initBoard();
        if(currentMoveIndex > 0){
            for(int i = 0; i < currentMoveIndex; i++){
                int cell = int.Parse(historyState[i]["cell"]);
                string symbol = historyState[i]["symbol"];
                base.gameBoard[cell] = symbol;
            }
        }
    }

    public override void displayBoard(List<Dictionary<string, string>> historyState, int currentMoveIndex){
        updateBoard(historyState, currentMoveIndex);
        Console.WriteLine("---+---+---");
        Console.WriteLine($" {base.gameBoard[0]} | {base.gameBoard[1]} | {base.gameBoard[2]} ");
        Console.WriteLine("---+---+---");
        Console.WriteLine($" {base.gameBoard[3]} | {base.gameBoard[4]} | {base.gameBoard[5]} ");
        Console.WriteLine("---+---+---");
        Console.WriteLine($" {base.gameBoard[6]} | {base.gameBoard[7]} | {base.gameBoard[8]} ");
        Console.WriteLine("---+---+---");
    }

    public override Dictionary<string, List<int>> getAvailableInfo(int currentMoveIndex){
        Dictionary<string, List<int>> availableInfo = new Dictionary<string, List<int>>();
        List<int> emptyCells = new List<int>();
        List<int> availableNums = Enumerable.Range(1, 9).ToList();
        List<int> usedNums = new List<int>();

        for(int i = 0; i < base.gameBoard.Length; i++){
            if(!string.IsNullOrWhiteSpace(base.gameBoard[i])){
                usedNums.Add(int.Parse(base.gameBoard[i].Trim()));
            }else{
                emptyCells.Add(i+1);
            }
        }
        availableNums.RemoveAll(n => usedNums.Contains(n));

        if(currentMoveIndex % 2 == 0){
            availableNums.RemoveAll(n => n % 2 == 0);
        }else{
            availableNums.RemoveAll(n => n % 2 != 0);
        }

        availableInfo["cells"] = emptyCells;
        availableInfo["symbols"] = availableNums;

        return availableInfo;
    }

    
}