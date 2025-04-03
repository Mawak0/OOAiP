using App;
using App.Scopes;
using Moq;
using Xunit;

namespace SpaceBattle.Lib.Tests
{
    public class ShootCommandTests
    {

        public ShootCommandTests()
        {
            new InitCommand().Execute();
            var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
            Ioc.Resolve<App.ICommand>("IoC.Scope.Current.Set", iocScope).Execute();
        }
        [Fact]
        public void ShootCommand_ExecutesSuccessfully()
        {
            var shooterMock = new Mock<IShooting>();
            shooterMock.SetupGet(s => s.Position).Returns(new Vector(new int[] { 0, 0 }));
            shooterMock.SetupGet(s => s.FireVelocity).Returns(new Vector(new int[] { 1, 1 }));
            shooterMock.SetupGet(s => s.Velocity).Returns(new Vector(new int[] { 2, 2 }));

            var torpedo = new Dictionary<string, object> { { "Position", new Vector(new int[] { 0, 0 }) }, { "Velocity", new Vector(new int[] { 0, 0 }) }, { "Id", "123" } };

            var movingMock = new Mock<IMoving>();

            var commandMock = new Mock<ICommand>();

            Ioc.Resolve<App.ICommand>("IoC.Register", "Adapters.IMovingObject", (object[] args) => movingMock.Object).Execute();
            Ioc.Resolve<App.ICommand>("IoC.Register", "Actions.Start", (object[] args) => commandMock.Object).Execute();
            Ioc.Resolve<App.ICommand>("IoC.Register", "Game.GetTorpedo", (object[] args) => torpedo).Execute();
            var regTorpedoInit = new RegisterIoCDependencyTorpedoInitialization();
            regTorpedoInit.Execute();
            var regCreateOrder = new RegisterIoCDependencyCreateStartMoveOrder();
            regCreateOrder.Execute();

            var shootCommandRegister = new RegisterIoCDependencyShootCommand();
            shootCommandRegister.Execute();
            var shootCommand = Ioc.Resolve<ICommand>("Commands.ShootCommand", shooterMock.Object);
            shootCommand.Execute();

            Assert.Equal(new Vector(new int[] { 1, 1 }), torpedo["Position"]);
            Assert.Equal(new Vector(new int[] { 3, 3 }), torpedo["Velocity"]);
            commandMock.Verify(cmd => cmd.Execute(), Times.Exactly(1));
        }
    }
}
