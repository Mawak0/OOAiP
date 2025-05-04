using App;
using App.Scopes;
using Moq;
using Xunit;

namespace SpaceBattle.Lib.Tests
{
    public class RegisterIoCTimerServiceTests
    {
        // [Fact]
        // public void ResolveDependencyCheckTimerService()
        // {
        //     // Arrange
        //     new InitCommand().Execute();
        //     var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
        //     Ioc.Resolve<App.ICommand>("IoC.Scope.Current.Set", iocScope).Execute();

        //     // Мокируем саму команду регистрации
        //     var mockRegTimerService = new Mock<RegisterIoCTimerService>();
        //     var regTimerService = mockRegTimerService.Object;
        //     regTimerService.Execute();

        //     // Проверяем, что регистрация прошла успешно
        //     var result = Ioc.Resolve<ITimerService>("Game.TimerService");

        //     // Assert
        //     Assert.NotNull(result);
        //     Assert.IsType<TimerService>(result);
        // }

        // [Fact]
        // public void Execute_ShouldRegisterTimerServiceInIoCContainer()
        // {
        //     // Создаём экземпляр тестируемого класса
        //     var registerIoCTimerService = new RegisterIoCTimerService();

        //     // Выполняем метод Execute
        //     registerIoCTimerService.Execute();

        //     // Получаем зарегистрированный объект из контейнера IoC
        //     var timerService = Ioc.Resolve<ITimerService>("Game.TimerService");

        //     // Проверяем, что TimerService был зарегистрирован
        //     Assert.NotNull(timerService);
        //     Assert.IsType<TimerService>(timerService);
        // }

        // [Fact]
        // public void Execute_ShouldResolveAndRegisterTimerService()
        // {
        //     // Создаём экземпляр тестируемого класса
        //     var registerIoCTimerService = new RegisterIoCTimerService();

        //     // Выполняем метод Execute
        //     registerIoCTimerService.Execute();

        //     // Проверяем, что сервис был зарегистрирован в контейнере IoC
        //     Assert.NotNull(Ioc.Resolve<ITimerService>("Game.TimerService"));
        // }

        // [Fact]
        // public void Execute_ShouldResolveTimerService()
        // {
        //     // Arrange
        //     var registerIoCTimerService = new RegisterIoCTimerService();
        //     registerIoCTimerService.Execute();

        //     // Act
        //     var timerService = Ioc.Resolve<ITimerService>("Game.TimerService");

        //     // Assert
        //     Assert.NotNull(timerService);
        //     Assert.IsType<TimerService>(timerService);
        // }
    }
}