using App;

namespace SpaceBattle.Lib;

public class RegisterIoCDependencyIsObjectsInOneSquare : ICommand
{
    public void Execute()
    {
        Ioc.Resolve<App.ICommand>(
            "IoC.Register",
            "Game.IsObjectsInOneSquare",
            (object[] args) =>
            {
                var pos1 = (Vector)args[0];
                var pos2 = (Vector)args[1];

                var fieldSize = Ioc.Resolve<Vector>("Game.FieldSize");
                var squareCount = Ioc.Resolve<int>("Game.FieldSquareCount");

                var squareSizeX = fieldSize[0] / squareCount;
                var squareSizeY = fieldSize[1] / squareCount;

                var pos1SquareX = pos1[0] / squareSizeX;
                var pos1SquareY = pos1[1] / squareSizeY;

                var pos2SquareX = pos2[0] / squareSizeX;
                var pos2SquareY = pos2[1] / squareSizeY;

                return (object)(pos1SquareX == pos2SquareX && pos1SquareY == pos2SquareY);
            }
        ).Execute();
    }
}
