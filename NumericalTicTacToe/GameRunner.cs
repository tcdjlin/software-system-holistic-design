using bgf = BoardGameFramework;

namespace NumericalTicTacToe;
class GameRunner : bgf.GameRunnerFW{
    public override bgf.UserInterfaceFW UI {get; set;}
    public override bgf.LogicFW logic {get; set;}

    protected override int gameMode{get; set;}

    public GameRunner(){
        this.UI = new UserInterface();
        this.logic = new Logic();
    }

    #region ------ Methods: Game Flow ------
    public override void initGame(){
        #region ------ Game Guide Configurations ------
        string gameGuide = "Game Rules:\n - Two players take turn to place a number between 1 - 9.\n - First Player -> Only Uses Odd Number.\n - Second Player -> Onlt Uses Even Number.\n - All numbers used only once.\n - First to get 15 points in a line (sum of numbers)\n    -> horizontal, vertical, diagonal";    

        string cellGuide = "Board Cell Guide (Each number represent the cell's number):\n * ----------- *\n *  1 | 2 | 3  *\n * ----------- *\n *  4 | 5 | 6  *\n * ----------- *\n *  7 | 8 | 9  *\n * ----------- *";

        string moveGuide = "To make a move -> 'm:<cell number>:<your number>'\n - To get help and instructions -> enter 'help'\n - To redo/undo/save, refer to help center.";
        #endregion
        
        UI.message.addMessage("gameGuide", gameGuide);
        UI.message.addMessage("cellGuide", cellGuide);
        UI.message.addMessage("moveGuide", moveGuide);

        UI.input.clearConsole();
        
        Console.WriteLine("* Welcome to [ Numerical Tic-Tac-Toe ] *");
        UI.message.displayMessage("gameGuide");
        UI.input.freezePrompt();
        UI.input.clearConsole();
        UI.message.displayMessage("cellGuide");
        UI.message.displayMessage("moveGuide");
        UI.input.freezePrompt();
        UI.input.clearConsole();

        if(isNewGame()){
            logic.playersAssign(initializePlayers());
        }else{
            var gameData = logic.history.loadFromFile();
            if(gameData != null){
                string playerName1 = gameData[0]["name"];
                string playerName2 = gameData[1]["name"];
                string playerType1 = gameData[0]["type"];
                string playerType2 = gameData[1]["type"];
                bgf.PlayerFW[] players = new bgf.PlayerFW[2];
                if(playerType1 == "human"){
                    players[0] = new bgf.HumanPlayerFW(playerName1);
                }else{
                    players[0] = new bgf.ComputerPlayerFW(playerName1);
                }
                if(playerType2 == "human"){
                    players[1] = new bgf.HumanPlayerFW(playerName2);
                }else{
                    players[1] = new bgf.ComputerPlayerFW(playerName2);
                }
                logic.playersAssign(players);
                logic.history.loadGameHistory(gameData);

                if(playerType1 == "human" && playerType2 == "human"){
                    this.gameMode = 1;
                }else{
                    this.gameMode = 0;
                }
            }else{
                logic.playersAssign(initializePlayers());
            }
        }
    }
    
    protected override bgf.PlayerFW[] initializePlayers(){
        bgf.PlayerFW[] players = new bgf.PlayerFW[2];

        string mode = "";
        while(true){
            Console.WriteLine("Pick game mode:");
            Console.WriteLine(" - Enter <0> for [ Computer VS Human ]");
            Console.WriteLine(" - Enter <1> for [ Human VS Human ] ");
            Console.Write("> ");
            mode = Console.ReadLine() ?? "";
            if(mode != "0" && mode != "1"){
                UI.input.invalidPrompt();
                UI.input.freezePrompt();
                UI.input.clearConsole();
                continue;
            }else{
                this.gameMode = int.Parse(mode);
                UI.input.clearConsole();
                break;
            }
        }

        if(mode == "0"){
            Console.WriteLine("Creating [ Computer Player ]...");
            bgf.ComputerPlayerFW computerPlayer = new bgf.ComputerPlayerFW();
            Thread.Sleep(500);
            Console.WriteLine(" - [ Computer Player ] initialized.");

            bgf.HumanPlayerFW humanPlayer;

            while(true){
                Console.WriteLine("Enter your name for [ Human Player ]");
                Console.Write("> ");
                string playerName = Console.ReadLine() ?? "";
                if(string.IsNullOrWhiteSpace(playerName)){
                    Console.WriteLine("[ Human Player ] name is invalid, enter gain.");
                    UI.input.freezePrompt();
                    UI.input.clearConsole();
                    continue;
                }else{
                    humanPlayer = new bgf.HumanPlayerFW(playerName);
                    Thread.Sleep(500);
                    Console.WriteLine("[ Human Player ] " + playerName + " successfully created.");
                    UI.input.freezePrompt();
                    UI.input.clearConsole();
                    break;
                }
            }
            Console.WriteLine("Default First Player: [ Human Player ], press <enter> to continue.");
            Console.WriteLine("Enter <1>: [ Computer Player ] to be the first player.");
            Console.Write(" > ");
            string playerOption = Console.ReadLine() ?? "";

            if(playerOption == "1"){
                players[0] = computerPlayer;
                players[1] = humanPlayer;
            }else{
                players[0] = humanPlayer;
                players[1] = computerPlayer;
            }
        }
        if(mode == "1"){
            while(true){
                Console.WriteLine("Enter name for [ Human Player 1 ]");
                Console.Write("> ");
                string playerName1 = Console.ReadLine() ?? "";
                if(string.IsNullOrWhiteSpace(playerName1)){
                    Console.WriteLine("[ Human Player 1 ] name is invalid, enter gain.");
                    UI.input.freezePrompt();
                    UI.input.clearConsole();
                    continue;
                }else{
                    bgf.HumanPlayerFW humanPlayer1 = new bgf.HumanPlayerFW(playerName1);
                    players[0] = humanPlayer1;
                    Console.WriteLine(playerName1 + " successfully created.");
                    UI.input.freezePrompt();
                    UI.input.clearConsole();
                    break;
                }
            }
            while(true){
                Console.WriteLine("Enter name for [ Human Player 2 ]");
                Console.Write("> ");
                string playerName2 = Console.ReadLine() ?? "";
                if(string.IsNullOrWhiteSpace(playerName2)){
                    Console.WriteLine("[ Human Player 2 ] name is invalid, enter gain.");
                    UI.input.freezePrompt();
                    UI.input.clearConsole();
                    continue;
                }else{
                    bgf.HumanPlayerFW humanPlayer2 = new bgf.HumanPlayerFW(playerName2);
                    players[1] = humanPlayer2;
                    Console.WriteLine(playerName2 + " successfully created.");
                    UI.input.freezePrompt();
                    UI.input.clearConsole();
                    break;
                }
            }
        }
        Console.WriteLine("Your game is ready to start!");
        UI.input.freezePrompt();
        UI.input.clearConsole();
        return players;
    }
    
    public override void runGame(){
        while(true){
            UI.input.clearConsole();

            if(endGame()){
                UI.board.displayBoard(logic.history.getHistoryState(), logic.history.getCurrentMoveIndex());
                break;
            }

            UI.board.displayBoard(logic.history.getHistoryState(), logic.history.getCurrentMoveIndex());
            UI.message.displayMessage("moveGuide");

            if(gameMode == 1){
                if(logic.history.getCurrentMoveIndex() % 2 == 0){
                    Console.WriteLine(logic.players[0].getPlayerInfo()["name"] + ", it's your turn!");
                }else{
                    Console.WriteLine(logic.players[1].getPlayerInfo()["name"] + ", it's your turn!");
                }
            }else{
                if(logic.handleComputerMove(logic.history.getCurrentMoveIndex(), UI.board.getAvailableInfo(logic.history.getCurrentMoveIndex()), logic.history)){
                    continue;
                }
            }

            if(!UI.input.promptInput()){
                UI.input.invalidPrompt();
                UI.input.freezePrompt();
                continue;
            }

            if(!UI.input.parseInput()){
                UI.input.invalidPrompt();
                UI.input.freezePrompt();
                continue;
            }

            string commandType = UI.input.getParsedInput()["type"];
            if(commandType == "help"){
                UI.getHelp(logic.history.getCurrentMoveIndex());
                continue;
            }
            if(commandType == "move"){
                Dictionary<string, string> move = new Dictionary<string, string>();
                move["cell"] = UI.input.getParsedInput()["cell"];
                move["symbol"] = UI.input.getParsedInput()["symbol"];
                
                if(logic.validator.isMoveValid(UI.board.getAvailableInfo(logic.history.getCurrentMoveIndex()), move)){
                    logic.handlePlayerMove(logic.history.getCurrentMoveIndex(), move, logic.history);
                }else{
                    Console.WriteLine("Illegal move, please enter again.");
                }
            }
            if(commandType == "redo"){
                if(logic.history.onRedo()){
                    continue;
                }else{
                    Console.WriteLine("Redo is not available.");
                    UI.input.freezePrompt();
                    continue;
                }
            }

            if(commandType == "undo"){
                if(logic.history.onUndo()){
                    continue;
                }else{
                    Console.WriteLine("Undo is not available.");
                    UI.input.freezePrompt();
                    continue;
                }

            }

            if(commandType == "export"){
                if(logic.history.exportToFile()){
                    Console.WriteLine("Game successfully saved.");
                }else{
                    Console.WriteLine("Unable to save game.");
                }
            }
            UI.input.freezePrompt();
        }
    }

    protected override bool endGame(){
        if(logic.validator.isWinnerFound(UI.board.getBoardState(logic.history.getHistoryState(), logic.history.getCurrentMoveIndex()))){
            string winnerName = "";
            if(logic.history.getCurrentMoveIndex() % 2 == 0){
                winnerName = logic.players[1].getPlayerInfo()["name"];
            }else{
                winnerName = logic.players[0].getPlayerInfo()["name"];
            }
            Console.WriteLine(winnerName + " won the game! Definitely a genius!");
            return true;
        }else{
            if(logic.validator.isGameOver(UI.board.getBoardState(logic.history.getHistoryState(), logic.history.getCurrentMoveIndex()))){
                Console.WriteLine("The game is over. Both players are a genius!");
                return true;
            }
            return false;
        }
    }
    #endregion
    

    
}
