using Moq;
using Xunit;
namespace SpaceBattle.Lib;

public class SendCommandTests
{
    [Fact]
    public void HasReceivedTest()
    {
        var mockReceiver = new Mock<ICommandReceiver>();
        var mockCommand = new Mock<ICommand>();
        var sendCommand = new SendCommand(mockCommand.Object, mockReceiver.Object);

        sendCommand.Execute();

        mockReceiver.Verify(r => r.Receive(mockCommand.Object));
    }

    [Fact]
    public void ReceiverDeniedTest()
    {
        var mockReceiver = new Mock<ICommandReceiver>();
        var mockCommand = new Mock<ICommand>();

        mockReceiver.Setup(r => r.Receive(mockCommand.Object)).Throws(new Exception());

        var sendCommand = new SendCommand(mockCommand.Object, mockReceiver.Object);

        Assert.Throws<Exception>(() => sendCommand.Execute());
    }
}
