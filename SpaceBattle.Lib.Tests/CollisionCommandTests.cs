using App;
using App.Scopes;
using Moq;
using Xunit;

namespace SpaceBattle.Lib.Tests
{
    public class CollisionCommandTests
    {
        public CollisionCommandTests()
        {
            new InitCommand().Execute();
            var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
            Ioc.Resolve<App.ICommand>("IoC.Scope.Current.Set", iocScope).Execute();
        }

        [Fact]
        public void Execute_CollisionDetected_CommandExecuted()
        {
            var pos1 = new Vector(new[] { 10, 20 });
            var pos2 = new Vector(new[] { 12, 18 });
            var vel1 = new Vector(new[] { 1, -1 });
            var vel2 = new Vector(new[] { -1, 1 });

            var mockFirstObj = new Mock<IColliding>();
            mockFirstObj.SetupGet(o => o.Position).Returns(pos1);
            mockFirstObj.SetupGet(o => o.Velocity).Returns(vel1);
            mockFirstObj.SetupGet(o => o.Shape).Returns("Circle");

            var mockSecondObj = new Mock<IColliding>();
            mockSecondObj.SetupGet(o => o.Position).Returns(pos2);
            mockSecondObj.SetupGet(o => o.Velocity).Returns(vel2);
            mockSecondObj.SetupGet(o => o.Shape).Returns("Circle");

            var mockCommand = new Mock<ICommand>();

            new RegisterIoCDependencyGetVectorDifference().Execute();

            Ioc.Resolve<App.ICommand>("IoC.Register", "Game.IsObjectsInOneSquare",
                (object[] args) => (object)true).Execute();

            Ioc.Resolve<App.ICommand>("IoC.Register", "Game.IsCollision",
                (object[] args) => (object)true).Execute();

            var collisionCommand = new CollisionCommand(
                mockFirstObj.Object,
                mockSecondObj.Object,
                mockCommand.Object
            );

            collisionCommand.Execute();

            mockCommand.Verify(c => c.Execute(), Times.Once);
        }

        [Fact]
        public void Execute_NoCollisionDetected_CommandNotExecuted()
        {
            var pos1 = new Vector(new[] { 10, 20 });
            var pos2 = new Vector(new[] { 12, 18 });
            var vel1 = new Vector(new[] { 1, -1 });
            var vel2 = new Vector(new[] { -1, 1 });

            var mockFirstObj = new Mock<IColliding>();
            mockFirstObj.SetupGet(o => o.Position).Returns(pos1);
            mockFirstObj.SetupGet(o => o.Velocity).Returns(vel1);
            mockFirstObj.SetupGet(o => o.Shape).Returns("Circle");

            var mockSecondObj = new Mock<IColliding>();
            mockSecondObj.SetupGet(o => o.Position).Returns(pos2);
            mockSecondObj.SetupGet(o => o.Velocity).Returns(vel2);
            mockSecondObj.SetupGet(o => o.Shape).Returns("Circle");

            var mockCommand = new Mock<ICommand>();

            new RegisterIoCDependencyGetVectorDifference().Execute();

            Ioc.Resolve<App.ICommand>("IoC.Register", "Game.IsObjectsInOneSquare",
                (object[] args) => (object)true).Execute();

            Ioc.Resolve<App.ICommand>("IoC.Register", "Game.IsCollision",
                (object[] args) => (object)false).Execute();

            var collisionCommand = new CollisionCommand(
                mockFirstObj.Object,
                mockSecondObj.Object,
                mockCommand.Object
            );

            collisionCommand.Execute();

            mockCommand.Verify(c => c.Execute(), Times.Never);
        }

        [Fact]
        public void Execute_ObjectsInDifferentSquares_CommandNotExecuted()
        {
            var pos1 = new Vector(new[] { 10, 20 });
            var pos2 = new Vector(new[] { 100, 200 });
            var vel1 = new Vector(new[] { 1, -1 });
            var vel2 = new Vector(new[] { -1, 1 });

            var mockFirstObj = new Mock<IColliding>();
            mockFirstObj.SetupGet(o => o.Position).Returns(pos1);
            mockFirstObj.SetupGet(o => o.Velocity).Returns(vel1);
            mockFirstObj.SetupGet(o => o.Shape).Returns("Circle");

            var mockSecondObj = new Mock<IColliding>();
            mockSecondObj.SetupGet(o => o.Position).Returns(pos2);
            mockSecondObj.SetupGet(o => o.Velocity).Returns(vel2);
            mockSecondObj.SetupGet(o => o.Shape).Returns("Circle");

            var mockCommand = new Mock<ICommand>();

            new RegisterIoCDependencyGetVectorDifference().Execute();

            Ioc.Resolve<App.ICommand>("IoC.Register", "Game.IsObjectsInOneSquare",
                (object[] args) => (object)false).Execute();

            var collisionCommand = new CollisionCommand(
                mockFirstObj.Object,
                mockSecondObj.Object,
                mockCommand.Object
            );

            collisionCommand.Execute();

            mockCommand.Verify(c => c.Execute(), Times.Never);
        }

        [Fact]
        public void Execute_FirstObjectPositionUnreadable_ThrowsException()
        {
            var mockFirstObj = new Mock<IColliding>();
            mockFirstObj.SetupGet(o => o.Position).Throws<InvalidOperationException>();

            var mockSecondObj = new Mock<IColliding>();
            var mockCommand = new Mock<ICommand>();

            new RegisterIoCDependencyGetVectorDifference().Execute();

            var collisionCommand = new CollisionCommand(
                mockFirstObj.Object,
                mockSecondObj.Object,
                mockCommand.Object
            );

            Assert.Throws<InvalidOperationException>(() => collisionCommand.Execute());
        }
    }
}
