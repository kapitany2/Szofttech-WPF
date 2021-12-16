using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using Szofttech_WPF.Network;

namespace Tests
{
    [TestClass]
    public class ServerTest
    {
        [TestMethod]
        public void Server_ShouldAcceptConnectionAndCloseGracefully()
        {
            //Arrange
            Server server = new Server(25564);

            //Act
            Client client1 = new Client(IPAddress.Loopback.ToString(), 25564);
            Client client2 = new Client(IPAddress.Loopback.ToString(), 25564);

            bool b1 = Server.isServerAvailable(IPAddress.Loopback.ToString(), 25564);
            bool b2 = Server.isServerAvailable(IPAddress.Loopback.ToString(), 25564);
            bool b3 = Server.isServerAvailable(IPAddress.Loopback.ToString(), 25564);

            client1.Close();
            client2.Close();

            server.Close();

            //Assert
            Assert.IsTrue(b1 && b2 && b3);
        }
    }
}
