using App;
using App.Scopes;
using Moq;
using Xunit;

namespace SpaceBattle.Lib.Tests;

public class QuadrantInspectorTests
{
    public QuadrantInspectorTests()
    {
        new InitCommand().Execute();
        var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
        Ioc.Resolve<App.ICommand>("IoC.Scope.Current.Set", iocScope).Execute();
    }

    [Fact]
    public void GetQuadrantTest()
    {
        var key = "shape";

        var polygon = new Polygon(new List<Point>() { new Point(1, 1), new Point(2, 2), new Point(1, 3) });

        var polygonDict = new Dictionary<string, Polygon> {
                            { key, polygon }
                        };

        Ioc.Resolve<App.ICommand>(
            "IoC.Register",
            "Collision.GetPolygonDict",
            (object[] args) => polygonDict
        ).Execute();

        Ioc.Resolve<App.ICommand>("IoC.Register", "Field.Quadrant.Size", (object[] args) => (object)10).Execute();

        var inspector = new QuadrantInspector();
        var mockColliding = new Mock<IColliding>();

        mockColliding.SetupGet(o => o.Shape).Returns("shape");

        var result = inspector.getQuadrants(mockColliding.Object);

        Assert.Equal(new List<int[]> { new[] { 0, 0 } }, result.Select(x => x.ToArray()).ToList());
    }

    [Fact]
    public void GetManyQuadrantsTest()
    {
        var key = "shape";

        var polygon = new Polygon(new List<Point>() { new Point(1, 12), new Point(2, 2), new Point(1, 3) });

        var polygonDict = new Dictionary<string, Polygon> {
                            { key, polygon }
                        };

        Ioc.Resolve<App.ICommand>(
            "IoC.Register",
            "Collision.GetPolygonDict",
            (object[] args) => polygonDict
        ).Execute();

        Ioc.Resolve<App.ICommand>("IoC.Register", "Field.Quadrant.Size", (object[] args) => (object)10).Execute();

        var inspector = new QuadrantInspector();
        var mockColliding = new Mock<IColliding>();

        mockColliding.SetupGet(o => o.Shape).Returns("shape");

        var result = inspector.getQuadrants(mockColliding.Object);

        Assert.Equal(new List<int[]> { new[] { 0, 1 }, new[] { 0, 0 } }, result.Select(x => x.ToArray()).ToList());
    }

    [Fact]
    public void CorrectObjectsInQuadrantTest()
    {
        var key1 = "shape1";
        var key2 = "shape2";
        var key3 = "shape3";

        var polygon1 = new Polygon(new List<Point>() { new Point(1, 1), new Point(2, 2), new Point(11, 3) });
        var polygon2 = new Polygon(new List<Point>() { new Point(5, 5), new Point(3, 7),
            new Point(5, 9), new Point(6, 7) });
        var polygon3 = new Polygon(new List<Point>() { new Point(13, 3), new Point(12, 6),
            new Point(13, 7), new Point(14, 6), new Point(15, 4) });

        var polygonDict = new Dictionary<string, Polygon> {
                            { key1, polygon1 },
                            { key2, polygon2 },
                            { key3, polygon3 },
                        };

        Ioc.Resolve<App.ICommand>(
            "IoC.Register",
            "Collision.GetPolygonDict",
            (object[] args) => polygonDict
        ).Execute();

        Ioc.Resolve<App.ICommand>("IoC.Register", "Field.Quadrant.Size", (object[] args) => (object)10).Execute();

        var inspector = new QuadrantInspector();
        var mockColliding1 = new Mock<IColliding>();
        var mockColliding2 = new Mock<IColliding>();
        var mockColliding3 = new Mock<IColliding>();

        mockColliding1.SetupGet(o => o.Shape).Returns("shape1");
        mockColliding2.SetupGet(o => o.Shape).Returns("shape2");
        mockColliding3.SetupGet(o => o.Shape).Returns("shape3");

        var colList = new List<IColliding> { mockColliding1.Object, mockColliding2.Object, mockColliding3.Object };

        Ioc.Resolve<App.ICommand>(
            "IoC.Register",
            "Collision.GetCollisionObjectsList",
            (object[] args) => colList
        ).Execute();

        var quadrantObjects = inspector.GetObjectsInSameSquare(new[] { 1, 0 });

        Assert.Equal(new List<IColliding>() { mockColliding1.Object, mockColliding3.Object }, quadrantObjects);
    }
}
