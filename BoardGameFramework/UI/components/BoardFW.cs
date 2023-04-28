namespace BoardGameFramework;

abstract class BoardFW{
    protected string[] gameBoard = new string[9];
    protected abstract string[] initBoard();

    public string[] getBoardState(List<Dictionary<string, string>> historyState, int currentMoveIndex){
        updateBoard(historyState, currentMoveIndex);
        return this.gameBoard;
    }

    protected abstract void updateBoard(List<Dictionary<string, string>> historyState, int currentMoveIndex);
    
    public abstract void displayBoard(List<Dictionary<string, string>> historyState, int currentMoveIndex);

    public abstract Dictionary<string, List<int>> getAvailableInfo(int currentMoveIndex);
}