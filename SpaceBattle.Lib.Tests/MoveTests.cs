using Moq;
using Xunit;
namespace SpaceBattle.Lib.Tests;

public class MoveCommandTests
{
    [Fact]
    public void Execute_CorrectPositionChange()
    {
        var vector1 = new Vector([12, 5]);
        var vector2 = new Vector([-4, 1]);
        var vector3 = new Vector([8, 6]);

        var mockMoving = new Mock<IMoving>();
        mockMoving.SetupGet(m => m.Position).Returns(vector1);
        mockMoving.SetupGet(m => m.Velocity).Returns(vector2);

        var command = new MoveCommand(mockMoving.Object);

        command.Execute();

        mockMoving.VerifySet(m => m.Position = vector3, Times.Once);
    }

    [Fact]
    public void Execute_UnreadablePosition_ThrowsException()
    {
        var vector1 = new Vector([-7, 3]);

        var mockMoving = new Mock<IMoving>();
        mockMoving.SetupGet(m => m.Position).Throws<InvalidOperationException>();
        mockMoving.SetupGet(m => m.Velocity).Returns(vector1);

        var command = new MoveCommand(mockMoving.Object);

        Assert.Throws<InvalidOperationException>(() => command.Execute());
    }

    [Fact]
    public void Execute_UnreadableVelocity_ThrowsException()
    {
        var vector1 = new Vector([12, 5]);

        var mockMoving = new Mock<IMoving>();
        mockMoving.SetupGet(m => m.Position).Returns(vector1);
        mockMoving.SetupGet(m => m.Velocity).Throws<InvalidOperationException>();

        var command = new MoveCommand(mockMoving.Object);

        Assert.Throws<InvalidOperationException>(() => command.Execute());
    }

    [Fact]
    public void Execute_UnchangeablePosition_ThrowsException()
    {
        var vector1 = new Vector([12, 5]);
        var vector2 = new Vector([-7, 3]);

        var mockMoving = new Mock<IMoving>();
        mockMoving.SetupGet(m => m.Position).Returns(vector1);
        mockMoving.SetupSet(m => m.Position = It.IsAny<Vector>()).Throws<InvalidOperationException>();
        mockMoving.SetupGet(m => m.Velocity).Returns(vector2);
        var command = new MoveCommand(mockMoving.Object);

        Assert.Throws<InvalidOperationException>(() => command.Execute());
    }
}
