using App;
using App.Scopes;
using Moq;
using Xunit;

namespace SpaceBattle.Lib.Tests
{
    public class RegisterIoCTimerServiceTests
    {
        [Fact]
        public void ResolveDependencyCheckTimerService()
        {
            new InitCommand().Execute();
            var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
            Ioc.Resolve<App.ICommand>("IoC.Scope.Current.Set", iocScope).Execute();

            var mockRegTimerService = new Mock<RegisterIoCTimerService>();
            var regTimerService = mockRegTimerService.Object;
            regTimerService.Execute();

            var result = Ioc.Resolve<ITimerService>("Game.TimerService");

            Assert.NotNull(result);
            Assert.IsType<TimerService>(result);
        }
    }
}

