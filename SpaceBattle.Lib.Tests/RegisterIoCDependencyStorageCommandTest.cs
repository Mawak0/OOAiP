namespace SpaceBattle.Lib.Tests;

using Xunit;
using App;
using App.Scopes;

public class RegisterIoCDependencyStorageCommandTest
{
    [Fact]
    public void CorrectStorageRegistrationTest()
    {
        new InitCommand().Execute();
        var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
        Ioc.Resolve<ICommand>("IoC.Scope.Current.Set", iocScope).Execute();

        var f1 = "shape1";
        var f2 = "shape2";

        var fours = new List<(int, int, int, int)> { (2, 4, 9, 7), (8, 3, 4, 6) };
        var t = new Tree(fours);

        var registerCommand = new RegisterIoCDependencyTree();

        registerCommand.Execute();

        var res = Ioc.Resolve<ICommand>("Collision.Tree.Add", f1, f2, t);

        Assert.IsType<AddToStorageCommand>(res);
    }

}