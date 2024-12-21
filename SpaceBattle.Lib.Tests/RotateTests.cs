using Moq;
using Xunit;

namespace SpaceBattle.Lib.Tests;

public class RotateCommandTests
{

    [Fact]
    public void RotateCommandPositive()
    {
        var angle1 = new Angle(1, 8);
        var angle2 = new Angle(1, 8);

        var mockRotating = new Mock<IRotate>();
        mockRotating.SetupGet(v => v.PositionAngle).Returns(angle1).Verifiable();
        mockRotating.SetupGet(v => v.VelocityAngle).Returns(angle2).Verifiable();

        var command = new RotateCommand(mockRotating.Object);
        command.Execute();

        mockRotating.VerifySet(v => v.PositionAngle = angle1 + angle2, Times.Once);
    }

    [Fact]
    public void CantReadAngleTest()
    {
        var mockRotating = new Mock<IRotate>();
        mockRotating.SetupGet(x => x.PositionAngle).Throws<Exception>();
        mockRotating.SetupGet(x => x.VelocityAngle).Returns(new Angle(1, 8));
        var command = new RotateCommand(mockRotating.Object);
        Assert.Throws<Exception>(() => command.Execute());
    }

    [Fact]
    public void CantReadVelocityTest()
    {
        var mockRotating = new Mock<IRotate>();
        mockRotating.SetupGet(x => x.PositionAngle).Returns(new Angle(1, 8));
        mockRotating.SetupGet(x => x.VelocityAngle).Throws<Exception>();
        var command = new RotateCommand(mockRotating.Object);
        Assert.Throws<Exception>(() => command.Execute());
    }

    [Fact]
    public void CantChangeAngleTest()
    {
        var mockRotating = new Mock<IRotate>();
        mockRotating.SetupGet(x => x.PositionAngle).Returns(new Angle(1, 8));
        mockRotating.SetupSet(x => x.PositionAngle = It.IsAny<Angle>()).Throws<Exception>();
        mockRotating.SetupGet(x => x.VelocityAngle).Returns(new Angle(1, 8));
        var command = new RotateCommand(mockRotating.Object);
        Assert.Throws<Exception>(() => command.Execute());
    }
}
