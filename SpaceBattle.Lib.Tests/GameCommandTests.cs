using App;
using App.Scopes;
using Moq;
using Xunit;
namespace SpaceBattle.Lib;

public class GameCommandTests
{
    [Fact]
    public void CorrectCommandExecution()
    {
        new InitCommand().Execute();
        var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
        Ioc.Resolve<App.ICommand>("IoC.Scope.Current.Set", iocScope).Execute();

        var mockQueue = new Mock<ITake>();
        var mockCmd = new Mock<ICommand>();
        // var mockTimerService = new Mock<ITimerService>();
        // Ioc.Resolve<App.ICommand>("IoC.Register", "Game.TimerService", (object[] _) => mockTimerService.Object).Execute();

        bool isQueueEmpty = false;

        var mockCanBeEmpty = mockQueue.As<ICanBeEmpty>();
        mockCanBeEmpty.Setup(m => m.isEmpty()).Returns(() => isQueueEmpty);

        mockQueue.Setup(q => q.Take())
            .Returns(mockCmd.Object)
            .Callback(() =>
            {
                isQueueEmpty = true;
            });

        var registerIoCDependencyGameCycleBehaviourCommand = new RegisterIoCDependencyGameCycleBehaviourCommand();
        var regShouldLoopRun = new RegisterIoCDependencyShouldLoopRun();
        var regNextCmd = new RegisterIoCDependencyNextCommand();

        var depsToReg = new List<ICommand> { registerIoCDependencyGameCycleBehaviourCommand, regShouldLoopRun, regNextCmd };

        var gameScope = Ioc.Resolve<object>("IoC.Scope.Create");
        var oldScope = Ioc.Resolve<object>("IoC.Scope.Current");
        Ioc.Resolve<App.ICommand>("IoC.Scope.Current.Set", gameScope).Execute();
        Ioc.Resolve<App.ICommand>(
                "IoC.Register",
                "Game.Queue",
                (object[] _) => mockQueue.Object).Execute();
        Ioc.Resolve<App.ICommand>(
            "IoC.Register",
            "Game.ExceptionHandle",
            (object[] obj) => new object()
        ).Execute();

        var registerIoCDependencyMacroCommand = new RegisterIoCDependencyMacroCommand();
        registerIoCDependencyMacroCommand.Execute();
        Ioc.Resolve<MCommand>("Commands.Macro", depsToReg.Select(d => d as ICommand).ToArray()).Execute();

        Ioc.Resolve<App.ICommand>("IoC.Scope.Current.Set", oldScope).Execute();

        var RegCreateGame = new RegisterIoCDependencyCreateGame();
        RegCreateGame.Execute();

        var timeSpanRegCommand = new RegisterIoCTimeSpan();
        timeSpanRegCommand.Execute();

        var game = Ioc.Resolve<ICommand>("Commands.CreateGame", gameScope);
        game.Execute();

        mockCmd.Verify(c => c.Execute(), Times.Once);
    }

    [Fact]
    public void ExceptionThrowsTest()
    {
        new InitCommand().Execute();
        var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
        Ioc.Resolve<App.ICommand>("IoC.Scope.Current.Set", iocScope).Execute();

        var mockQueue = new Mock<ITake>();
        var mockCmd = new Mock<ICommand>();
        // var mockTimerService = new Mock<ITimerService>();
        // Ioc.Resolve<App.ICommand>("IoC.Register", "Game.TimerService", (object[] _) => mockTimerService.Object).Execute();
        var exceptionCounter = 0;
        var mockCmdHandler = new Mock<ICommand>();
        mockCmdHandler.Setup(cmd => cmd.Execute())
              .Callback(() => exceptionCounter++);
        mockCmd.Setup(cmd => cmd.Execute()).Throws(new Exception("Command execution failed"));
        bool isQueueEmpty = false;

        var mockCanBeEmpty = mockQueue.As<ICanBeEmpty>();
        mockCanBeEmpty.Setup(m => m.isEmpty()).Returns(() => isQueueEmpty);

        mockQueue.Setup(q => q.Take())
            .Returns(mockCmd.Object)
            .Callback(() =>
            {
                isQueueEmpty = true;
            });

        var registerIoCDependencyGameCycleBehaviourCommand = new RegisterIoCDependencyGameCycleBehaviourCommand();
        var regShouldLoopRun = new RegisterIoCDependencyShouldLoopRun();
        var regNextCmd = new RegisterIoCDependencyNextCommand();

        var depsToReg = new List<ICommand> { registerIoCDependencyGameCycleBehaviourCommand, regShouldLoopRun, regNextCmd };

        var gameScope = Ioc.Resolve<object>("IoC.Scope.Create");
        var oldScope = Ioc.Resolve<object>("IoC.Scope.Current");
        Ioc.Resolve<App.ICommand>("IoC.Scope.Current.Set", gameScope).Execute();
        Ioc.Resolve<App.ICommand>(
                "IoC.Register",
                "Game.Queue",
                (object[] _) => mockQueue.Object).Execute();
        Ioc.Resolve<App.ICommand>(
            "IoC.Register",
            "Game.ExceptionHandle",
            (object[] obj) => mockCmdHandler.Object
        ).Execute();

        var registerIoCDependencyMacroCommand = new RegisterIoCDependencyMacroCommand();
        registerIoCDependencyMacroCommand.Execute();

        Ioc.Resolve<MCommand>("Commands.Macro", depsToReg.Select(d => d as ICommand).ToArray()).Execute();

        Ioc.Resolve<App.ICommand>("IoC.Scope.Current.Set", oldScope).Execute();

        var RegCreateGame = new RegisterIoCDependencyCreateGame();
        RegCreateGame.Execute();

        var timeSpanRegCommand = new RegisterIoCTimeSpan();
        timeSpanRegCommand.Execute();

        var game = Ioc.Resolve<ICommand>("Commands.CreateGame", gameScope);
        game.Execute();

        mockQueue.Verify(queue => queue.Take(), Times.Once);

        mockCmd.Verify(cmd => cmd.Execute(), Times.Once);

        Assert.Equal(1, exceptionCounter);
    }
}
