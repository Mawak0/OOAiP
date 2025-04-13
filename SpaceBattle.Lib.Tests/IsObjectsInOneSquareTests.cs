using App;
using App.Scopes;
using Xunit;

namespace SpaceBattle.Lib.Tests
{
    public class IsObjectsInOneSquareTests
    {
        public IsObjectsInOneSquareTests()
        {
            new InitCommand().Execute();
            var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
            Ioc.Resolve<App.ICommand>("IoC.Scope.Current.Set", iocScope).Execute();
        }

        private static void RegisterFieldParams(Vector fieldSize, int squareCount)
        {
            Ioc.Resolve<App.ICommand>("IoC.Register", "Game.FieldSize", (object[] args) => (object)fieldSize).Execute();
            Ioc.Resolve<App.ICommand>("IoC.Register", "Game.FieldSquareCount", (object[] args) => (object)squareCount).Execute();
        }

        [Fact]
        public void ObjectsInSameSquare_ReturnsTrue()
        {
            RegisterFieldParams(new Vector(new[] { 100, 100 }), 10);
            new RegisterIoCDependencyIsObjectsInOneSquare().Execute();

            var pos1 = new Vector(new[] { 12, 18 });
            var pos2 = new Vector(new[] { 19, 10 });

            var result = (bool)Ioc.Resolve<object>("Game.IsObjectsInOneSquare", pos1, pos2);

            Assert.True(result);
        }

        [Fact]
        public void ObjectsInDifferentSquares_ReturnsFalse()
        {
            RegisterFieldParams(new Vector(new[] { 100, 100 }), 10);
            new RegisterIoCDependencyIsObjectsInOneSquare().Execute();

            var pos1 = new Vector(new[] { 5, 5 });
            var pos2 = new Vector(new[] { 95, 95 });

            var result = (bool)Ioc.Resolve<object>("Game.IsObjectsInOneSquare", pos1, pos2);

            Assert.False(result);
        }
    }
}
