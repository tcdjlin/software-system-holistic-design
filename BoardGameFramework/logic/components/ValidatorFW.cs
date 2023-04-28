namespace BoardGameFramework;

abstract class ValidatorFW{
    public bool isFirstPlayerTurn(int currentMoveIndex){
        if(currentMoveIndex % 2 == 0){
            return true;
        }else{
            return false;
        }
    }

    public abstract bool isMoveValid(Dictionary<string, List<int>> boardAvailableInfo, Dictionary<string, string> moveInfo);

    public abstract bool isGameOver(string[] boardState);

    public abstract bool isWinnerFound(string[] boardState);
}