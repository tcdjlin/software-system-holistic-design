namespace BoardGameFramework;

abstract class UserInterfaceFW{
    public abstract BoardFW board {get; set;}
    public abstract MessageFW message {get; set;}
    public abstract InputHandlerFW input {get; set;}

    public abstract void getHelp(int currentMoveIndex);
    
    
}