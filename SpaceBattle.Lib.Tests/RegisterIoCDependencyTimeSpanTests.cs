using App;
using App.Scopes;
using Xunit;

namespace SpaceBattle.Lib.Tests
{
    public class RegisterIoCTimeSpanTests
    {
        [Fact]
        public void Test_TimeSpanSuccessfulRegistration()
        {
            new InitCommand().Execute();
            var scope = Ioc.Resolve<object>("IoC.Scope.Create");
            Ioc.Resolve<App.ICommand>("IoC.Scope.Current.Set", scope).Execute();

            var registerCmd = new RegisterIoCTimeSpan();
            registerCmd.Execute();

            var result = Ioc.Resolve<TimeSpan>("Game.TimeSpan");

            Assert.Equal(new TimeSpan(0, 0, 200), result);
        }

        [Fact]
        public void Test_TimeSpanResolutionReturnsExpectedValue()
        {
            new InitCommand().Execute();
            var scope = Ioc.Resolve<object>("IoC.Scope.Create");
            Ioc.Resolve<App.ICommand>("IoC.Scope.Current.Set", scope).Execute();

            Ioc.Resolve<App.ICommand>(
                "IoC.Register",
                "Game.TimeSpan",
                (object[] _) => (object)new TimeSpan(0, 0, 200))
               .Execute();

            var result = Ioc.Resolve<TimeSpan>("Game.TimeSpan");

            Assert.Equal(new TimeSpan(0, 0, 200), result);
        }
    }
}
