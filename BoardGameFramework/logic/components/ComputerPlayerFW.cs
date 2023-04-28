namespace BoardGameFramework;

class ComputerPlayerFW : PlayerFW{

    public ComputerPlayerFW(string playerName = "") : base("computer"){
        if(!string.IsNullOrWhiteSpace(playerName)){
            base.playerInfo["name"] = playerName;
        }
    }

}