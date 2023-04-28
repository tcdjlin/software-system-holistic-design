using BoardGameFramework;
using bgf = BoardGameFramework;

namespace NumericalTicTacToe;

class Logic : bgf.LogicFW{
    public override ValidatorFW validator { get; set; }
    public Logic(){
        this.validator = new Validator();
    }

    

}