using App;
using App.Scopes;
using Xunit;

namespace SpaceBattle.Lib.Tests;

public class GameObjectsTests
{
    public GameObjectsTests()
    {
        new InitCommand().Execute();
        var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
        Ioc.Resolve<App.ICommand>("IoC.Scope.Current.Set", iocScope).Execute();
    }

    [Fact]
    public void AddObjectTest()
    {
        var registerUuidGenerate = new RegisterIoCDependencyUuidGenerate();
        registerUuidGenerate.Execute();
        var RegisterGameObjectRepository = new RegisterIoCDependencyGameObjectRepository();
        RegisterGameObjectRepository.Execute();
        var RegisterGameObject = new RegisterIoCDependencyGameObject();
        RegisterGameObject.Execute();
        var RegisterAddGameObject = new RegisterIoCDependencyAddGameObject();
        RegisterAddGameObject.Execute();

        var item = new object();
        var uuid = Ioc.Resolve<string>("Uuid.Generate");
        Ioc.Resolve<ICommand>("Game.Object.Add", uuid, item).Execute();

        var repository = (IDictionary<string, object>)Ioc.Resolve<object>("Game.Object.Repository");
        Assert.True(repository.ContainsKey(uuid));
        Assert.Equal(item, repository[uuid]);
    }
    [Fact]
    public void RemoveObjectTest()
    {
        var RegisterGameObjectRepository = new RegisterIoCDependencyGameObjectRepository();
        RegisterGameObjectRepository.Execute();
        var RegisterAddGameObject = new RegisterIoCDependencyAddGameObject();
        RegisterAddGameObject.Execute();
        var RegisterRemoveGameObject = new RegisterIoCDependencyRemoveGameObject();
        RegisterRemoveGameObject.Execute();

        var item = new object();
        var uuid = Ioc.Resolve<string>("Uuid.Generate");
        Ioc.Resolve<ICommand>("Game.Object.Add", uuid, item).Execute();

        var repository = (IDictionary<string, object>)Ioc.Resolve<object>("Game.Object.Repository");
        Assert.True(repository.ContainsKey(uuid));

        Ioc.Resolve<object>("Game.Object.Remove", uuid);

        Assert.False(repository.ContainsKey(uuid));
    }

    [Fact]
    public void GetObjectTest()
    {
        var registerUuidGenerate = new RegisterIoCDependencyUuidGenerate();
        registerUuidGenerate.Execute();
        var RegisterGameObjectRepository = new RegisterIoCDependencyGameObjectRepository();
        RegisterGameObjectRepository.Execute();
        var RegisterAddGameObject = new RegisterIoCDependencyAddGameObject();
        RegisterAddGameObject.Execute();
        var RegisterGameObject = new RegisterIoCDependencyGameObject();
        RegisterGameObject.Execute();

        var item = new object();
        var uuid = Ioc.Resolve<string>("Uuid.Generate");
        Ioc.Resolve<ICommand>("Game.Object.Add", uuid, item).Execute();

        var retrievedItem = Ioc.Resolve<object>("Game.Object", uuid);

        Assert.Same(item, retrievedItem);
    }
}
