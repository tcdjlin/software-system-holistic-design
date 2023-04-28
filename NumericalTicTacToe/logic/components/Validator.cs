using bgf = BoardGameFramework;

namespace NumericalTicTacToe;

class Validator : bgf.ValidatorFW{
    public override bool isMoveValid(Dictionary<string, List<int>> boardAvailableInfo, Dictionary<string, string> moveInfo){
        List<int> availableCells = boardAvailableInfo["cells"];
        List<int> availableSymbols = boardAvailableInfo["symbols"];
        int targetCell = int.Parse(moveInfo["cell"]);
        int targetSymbol = int.Parse(moveInfo["symbol"]);

        if(availableCells.Contains(targetCell) && availableSymbols.Contains(targetSymbol)){
            return true;
        }

        return false;
    }

    public override bool isGameOver(string[] boardState){
        for(int i = 0; i < boardState.Length; i++){
            if(string.IsNullOrWhiteSpace(boardState[i])){
                return false;
            }
        }
        return true;
    }

    public override bool isWinnerFound(string[] boardState){
        bool vertical = false;
        bool horizontal = false;
        bool diagonal = false;

        int[] cellScores = new int[9];

        for(int i = 0; i < boardState.Length; i++){
            if(string.IsNullOrWhiteSpace(boardState[i])){
                cellScores[i] = 0;
            }else{
                cellScores[i] = int.Parse(boardState[i]);
            }
        }
        if(isWinningLine(cellScores[0], cellScores[3], cellScores[6])){
            return true;
        }
        if(isWinningLine(cellScores[1], cellScores[4], cellScores[7])){
            return true;
        }
        if(isWinningLine(cellScores[2], cellScores[5], cellScores[8])){
            return true;
        }

        if(isWinningLine(cellScores[0], cellScores[1], cellScores[2])){
            return true;
        }
        if(isWinningLine(cellScores[3], cellScores[4], cellScores[5])){
            return true;
        }
        if(isWinningLine(cellScores[6], cellScores[7], cellScores[8])){
            return true;
        }

        if(isWinningLine(cellScores[0], cellScores[4], cellScores[8])){
            return true;
        }
        if(isWinningLine(cellScores[2], cellScores[4], cellScores[6])){
            return true;
        }

        if(vertical || horizontal || diagonal){
            return true;
        }

        return false;
    }

    private bool isWinningLine(int cell1, int cell2, int cell3){
        if(cell1 > 0 && cell2 > 0 && cell3 > 0){
            if(cell1 + cell2 + cell3 == 15){
                return true;
            }
        }
        
        return false;
    }
}