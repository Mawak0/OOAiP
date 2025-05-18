using App;

namespace SpaceBattle.Lib;

public class RegisterIoCDependencyShapesDict : ICommand
{
    public void Execute()
    {

        Ioc.Resolve<Dictionary<string, Polygon>>(
                    "IoC.Register",
                    "Collision.ShapeDict",
                    () => new Dictionary<string, Polygon> { });
    }
}
