using App;

namespace SpaceBattle.Lib;

public class RegisterIoCDependencyShapesDictAdd : ICommand
{
    public void Execute()
    {
        Ioc.Resolve<ICommand>(
                "IoC.Register",
                "Collision.ShapeDict.Add",
                (object[] args) =>
                {
                    var dct = Ioc.Resolve<Dictionary<string, Polygon>>(
                        "Collision.ShapeDict");

                    dct[(string)args[0]] = (Polygon)args[1];

                    Ioc.Resolve<Dictionary<string, Polygon>>(
                        "IoC.Register",
                        "Collision.ShapeDict",
                        () => dct);

                }).Execute();
    }
}
