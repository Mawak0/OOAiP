using App;
using App.Scopes;
using Xunit;

namespace SpaceBattle.Lib.Tests;

public class CollisionDataGeneratorTests
{
    public CollisionDataGeneratorTests()
    {
        new InitCommand().Execute();
        var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
        Ioc.Resolve<App.ICommand>("IoC.Scope.Current.Set", iocScope).Execute();
    }

    [Fact]
    public void CorrectIoCRegistrationTest()
    {
        var key1 = "shape1";
        var key2 = "shape2";

        var polygon1 = new Polygon(new List<Point>() { new Point(1, 1), new Point(2, 2), new Point(1, 3) });
        var polygon2 = new Polygon(new List<Point>() { new Point(1, 2), new Point(2, 3), new Point(1, 4) });

        var polygonDict = new Dictionary<string, Polygon> {
                            { key1, polygon1 },
                            { key2, polygon2 }
                        };
        Ioc.Resolve<App.ICommand>(
            "IoC.Register",
            "Collision.GetPolygonDict",
            (object[] args) => polygonDict
        ).Execute();

        Ioc.Resolve<App.ICommand>("IoC.Register", "Collision.Dataset.Length", (object[] args) => (object)100).Execute();

        Ioc.Resolve<App.ICommand>("IoC.Register", "Field.Quadrant.Size", (object[] args) => (object)10).Execute();

        var registerIoCDependencyCollisionGenerator = new RegisterIoCDependencyCollisionGenerator();
        registerIoCDependencyCollisionGenerator.Execute();

        var dataGenerator = Ioc.Resolve<CollisionDataGenerator>("Collision.DataGenerator", key1, key2);

        Assert.IsType<CollisionDataGenerator>(dataGenerator);
    }

    [Fact]
    public void GenerateCollisionsTest()
    {
        var key1 = "shape1";
        var key2 = "shape2";

        var polygon1 = new Polygon(new List<Point>() { new Point(1, 1), new Point(2, 2), new Point(1, 3) });
        var polygon2 = new Polygon(new List<Point>() { new Point(1, 2), new Point(2, 3), new Point(1, 4) });

        var polygonDict = new Dictionary<string, Polygon> {
                            { key1, polygon1 },
                            { key2, polygon2 }
                        };
        Ioc.Resolve<App.ICommand>(
            "IoC.Register",
            "Collision.GetPolygonDict",
            (object[] args) => polygonDict
        ).Execute();

        Ioc.Resolve<App.ICommand>("IoC.Register", "Collision.Dataset.Length", (object[] args) => (object)100).Execute();

        Ioc.Resolve<App.ICommand>("IoC.Register", "Field.Quadrant.Size", (object[] args) => (object)10).Execute();

        var collisionDataGenerator = new CollisionDataGenerator(key1, key2);

        List<int[]> collisionData = (List<int[]>)collisionDataGenerator.GenerateCollisionData();

        Assert.True(collisionData.Count > 0);
    }

    [Fact]
    public void CreatePointReturnsValidRandomPointTest()
    {
        Ioc.Resolve<App.ICommand>("IoC.Register", "Field.Quadrant.Size", (object[] args) => (object)10).Execute();

        var generator = new CollisionDataGenerator("shape1", "shape2");
        var point = generator.CreatePoint();

        Assert.InRange(point.X, 0, 10);
        Assert.InRange(point.Y, 0, 10);
    }

    [Fact]
    public void HandleEmptyPolygonsTest()
    {
        var key1 = "empty_shape1";
        var key2 = "empty_shape2";

        var emptyPolygon = new Polygon(new List<Point>());

        var polygonDict = new Dictionary<string, Polygon> {
                            { key1, emptyPolygon },
                            { key2, emptyPolygon }
                        };
        Ioc.Resolve<App.ICommand>(
            "IoC.Register",
            "Collision.GetPolygonDict",
            (object[] args) => polygonDict
        ).Execute();

        Ioc.Resolve<App.ICommand>("IoC.Register", "Collision.Dataset.Length", (object[] args) => (object)100).Execute();

        Ioc.Resolve<App.ICommand>("IoC.Register", "Field.Quadrant.Size", (object[] args) => (object)10).Execute();

        var collisionDataGenerator = new CollisionDataGenerator(key1, key2);

        List<int[]> collisionData = (List<int[]>)collisionDataGenerator.GenerateCollisionData();

        Assert.Empty(collisionData);
    }

    [Fact]
    public void HandleOnePointPolygonsTest()
    {
        var key1 = "degenerate_shape1";
        var key2 = "degenerate_shape2";

        var degeneratePolygon = new Polygon(new List<Point>() { new Point(1, 1) });

        var polygonDict = new Dictionary<string, Polygon> {
                            { key1, degeneratePolygon },
                            { key2, degeneratePolygon }
                        };
        Ioc.Resolve<App.ICommand>(
            "IoC.Register",
            "Collision.GetPolygonDict",
            (object[] args) => polygonDict
        ).Execute();

        Ioc.Resolve<App.ICommand>("IoC.Register", "Collision.Dataset.Length", (object[] args) => (object)100).Execute();

        Ioc.Resolve<App.ICommand>("IoC.Register", "Field.Quadrant.Size", (object[] args) => (object)10).Execute();

        var collisionDataGenerator = new CollisionDataGenerator(key1, key2);

        List<int[]> collisionData = (List<int[]>)collisionDataGenerator.GenerateCollisionData();

        Assert.Empty(collisionData);
    }

    [Fact]
    public void FirstShape_ReturnsCorrectValue()
    {
        var key1 = "shape1";
        var key2 = "shape2";

        var collisionDataGenerator = new CollisionDataGenerator(key1, key2);

        Assert.Equal(key1, collisionDataGenerator.firstShape);
    }

    [Fact]
    public void SecondShape_ReturnsCorrectValue()
    {
        var key1 = "shape1";
        var key2 = "shape2";

        var collisionDataGenerator = new CollisionDataGenerator(key1, key2);

        Assert.Equal(key2, collisionDataGenerator.secondShape);
    }
}
