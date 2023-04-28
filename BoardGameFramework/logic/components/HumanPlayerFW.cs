namespace BoardGameFramework;

class HumanPlayerFW : PlayerFW{

    public HumanPlayerFW(string playerName) : base("human"){
        base.playerInfo["name"] = playerName;
    }

}