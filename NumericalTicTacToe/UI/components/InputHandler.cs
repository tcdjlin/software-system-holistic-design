using bgf = BoardGameFramework;

namespace NumericalTicTacToe;

class InputHandler : bgf.InputHandlerFW{
    public override bool parseInput()
    {
        Dictionary<string, string> parsedInputDict = new Dictionary<string, string>();
        string[] rawInputList = base.rawInput.Split(":");

        if(rawInputList[0] == "help" && rawInputList.Length == 1){
            parsedInputDict["type"] = "help";
            base.parsedInput = parsedInputDict;
            return true;
        }
        if(rawInputList[0] == "m" && rawInputList.Length == 3){
            parsedInputDict["type"] = "move";
            parsedInputDict["cell"] = rawInputList[1];
            parsedInputDict["symbol"] = rawInputList[2];
            base.parsedInput = parsedInputDict;
            return true;
        }
        if(rawInputList[0] == "redo" && rawInputList.Length == 1){
            parsedInputDict["type"] = "redo";
            base.parsedInput = parsedInputDict;
            return true;
        }
        if(rawInputList[0] == "undo" && rawInputList.Length == 1){
            parsedInputDict["type"] = "undo";
            base.parsedInput = parsedInputDict;
            return true;
        }
        if(rawInputList[0] == "export" && rawInputList.Length == 1){
            parsedInputDict["type"] = "export";
            base.parsedInput = parsedInputDict;
            while(true){  
                Console.Write("Confirm to save game [y/n]: ");
                string response = Console.ReadLine() ?? "";
                if(response.ToUpper() != "Y" && response.ToUpper() != "N"){
                    Console.WriteLine("Please enter a valid response.");
                    continue;
                }else{
                    if(response.ToUpper() == "Y"){
                        return true;
                    }
                    if(response.ToUpper() == "N"){
                        return false;
                    }
                }
            }
            
        }
        return false;
    }


}