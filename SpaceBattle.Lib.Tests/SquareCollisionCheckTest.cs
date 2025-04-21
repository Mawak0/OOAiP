using App;
using App.Scopes;
using Moq;
using Xunit;

namespace SpaceBattle.Lib
{
    public class SquareCollisionCheckCommandTest
    {
        [Fact]
        public void SquareCollisionCheckCommand_Executes_CollisionCommands_For_EachNearbyObject()
        {
            new InitCommand().Execute();
            var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
            Ioc.Resolve<App.ICommand>("IoC.Scope.Current.Set", iocScope).Execute();

            var collidingObj = new Mock<IColliding>().Object;

            var nearbyObj1 = new Mock<IColliding>().Object;
            var nearbyObj2 = new Mock<IColliding>().Object;
            var nearbyObjects = new List<IColliding> { nearbyObj1, nearbyObj2 };

            var collisionCmd1 = new Mock<ICommand>();
            var collisionCmd2 = new Mock<ICommand>();

            var registeredObjects = new List<IColliding> { nearbyObj1, nearbyObj2 };
            var registeredCommands = new List<Mock<ICommand>> { collisionCmd1, collisionCmd2 };

            Ioc.Resolve<App.ICommand>("IoC.Register", "Game.GetObjectsInSameSquare", (object[] args) =>
            {
                return registeredObjects;
            }).Execute();

            Ioc.Resolve<App.ICommand>("IoC.Register", "Game.HandleCollision", (object[] args) =>
            {
                return new Mock<ICommand>().Object;
            }).Execute();

            Ioc.Resolve<App.ICommand>("IoC.Register", "Game.CollisionCommand", (object[] args) =>
            {
                var second = (IColliding)args[1];
                var index = registeredObjects.IndexOf(second);
                return registeredCommands[index].Object;
            }).Execute();

            new SquareCollisionCheckCommand(collidingObj).Execute();

            collisionCmd1.Verify(cmd => cmd.Execute(), Times.Once);
            collisionCmd2.Verify(cmd => cmd.Execute(), Times.Once);
        }
    }
}
