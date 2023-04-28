namespace BoardGameFramework;
abstract class PlayerFW{
    protected Dictionary<string, string> playerInfo = new Dictionary<string, string>();

    public PlayerFW(string playerType){
        this.playerInfo["name"] = randomNameGenerator();
        this.playerInfo["type"] = playerType;
    }

    
    public Dictionary<string, string> getPlayerInfo(){
        return playerInfo;
    }
    public void makeMove(Dictionary<string, string> move, GameHistory history){
        Dictionary <string, string> newMove = new Dictionary<string, string>();
        newMove["type"] = this.playerInfo["type"];
        newMove["name"] = this.playerInfo["name"];
        newMove["cell"] = move["cell"];
        newMove["symbol"] = move["symbol"];
        history.updateHistory(newMove, true);
    }

    private string randomNameGenerator(){
        Random random = new Random();

        string baseName = "player";
        int randomNum = random.Next(1000);
        
        return baseName + randomNum.ToString();
    }

}