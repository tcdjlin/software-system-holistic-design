namespace NumericalTicTacToe;

class NumericalTicTacToe{
    static void Main(string[] args)
    {   
        GameRunner runner = new GameRunner();

        runner.initGame();
        runner.runGame();


    }
}