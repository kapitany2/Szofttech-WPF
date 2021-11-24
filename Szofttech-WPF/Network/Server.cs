using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Szofttech_WPF.DataPackage;
using Szofttech_WPF.Logic;
using Szofttech_WPF.Utils;

namespace Szofttech_WPF.Network
{
    public class Server
    {
        private int clientID = 0;
        private List<string>[] queueArray = new List<string>[2];
        private bool close = false;
        private GameLogic gameLogic = null;
        private Socket sSocket = null;

        public void addMessageToQueue(string message, int ID)
        {
            if (ID != 1)
                queueArray[ID].Add(message);
        }

        public void Close()
        {
            close = true;
            if (sSocket != null)
                sSocket.Close();
        }

        public Server(int port)
        {
            for (int i = 0; i < 2; ++i)
            {
                queueArray[i] = new List<string>();
            }
            gameLogic = new GameLogic();
            IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Any, port);
            sSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            sSocket.Bind(localEndPoint);
            sSocket.Listen(3);


            Thread threadQueuePoll = new Thread(() => {
                while (!close)
                {
                    Thread.Sleep(10);
                    while (gameLogic.messageQueue.Count != 0)
                    {
                        string BroadcastMessage = gameLogic.messageQueue[0];
                        gameLogic.messageQueue.RemoveAt(0);
                        if (BroadcastMessage != null)
                        {
                            Data decoded = DataConverter.decode(BroadcastMessage);
                            int recipient = decoded.getRecipientID();
                            addMessageToQueue(BroadcastMessage, recipient);
                        }
                    }
                } 
            });
            threadQueuePoll.Start();

            for (int i = 0; i < 3; ++i)
                ServeClient();
        }

        private void ServeClient()
        {
            Thread thread = new Thread(() =>
            {
                while (!close)
                {
                    BEGIN:
                    Socket socket = sSocket.Accept();

                    byte[] buffer = new byte[1024];
                    string inMsg = null;

                    while (true)
                    {
                        int numByte = socket.Receive(buffer);

                        inMsg += Encoding.ASCII.GetString(buffer, 0, numByte);

                        if (inMsg.IndexOf("<EOF>") > -1)
                        {
                            inMsg = inMsg.Replace("<EOF>", "");
                            if (inMsg != "CLIENT")
                            { 
                                socket.Close();
                                goto BEGIN;
                            }
                            break;
                        }
                    }

                    int ID = clientID++;
                    int otherQueueID = (ID == 0) ? 1 : 0;
                    int ownQueueID = (ID == 0) ? 0 : 1;
                    addMessageToQueue(ID + "$ConnectionData$$" + ((ID == 0) ? 1 : 0), otherQueueID);

                    byte[] message = Encoding.ASCII.GetBytes(ID.ToString());
                    socket.Send(message);

                    Thread threadReader = new Thread(() =>
                    {
                        while (!close)
                        {
                            Thread.Sleep(10);
                            try
                            {
                                while (true)
                                {
                                    int numByte = socket.Receive(buffer);
                                    inMsg += Encoding.ASCII.GetString(buffer, 0, numByte);

                                    if (inMsg.IndexOf("<EOF>") > -1)
                                    {
                                        inMsg = inMsg.Replace("<EOF>", "");
                                        break;
                                    }
                                }
                                if (inMsg == "$DisconnectData$$-1")
                                {
                                    int recipient = (ID == 0) ? 1 : 0;
                                    inMsg = "-1$ChatData$The other player has left the game.$" + recipient;
                                }
                                gameLogic.processMessage(DataConverter.decode(inMsg));
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                        }
                    });
                    threadReader.Start();

                    while (!close)
                    {
                        Thread.Sleep(10);
                        while (queueArray[ownQueueID].Count != 0)
                        {
                            string queueMsg = queueArray[ownQueueID][0];
                            queueArray[ownQueueID].RemoveAt(0);
                            byte[] bytes = Encoding.ASCII.GetBytes(queueMsg);
                            socket.Send(bytes);
                        }
                    }
                }                
            });
            thread.Start();
        }

        public static bool isServerAvailable(string ip, int port)
        {
            bool isAvailable = false;

            try
            {
                Socket socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
                IAsyncResult result = socket.BeginConnect(IPAddress.Parse(ip), port, null, null);
                bool success = result.AsyncWaitHandle.WaitOne(1000, true);

                if (socket.Connected)
                {
                    socket.EndConnect(result);
                    socket.Close();
                    isAvailable = true;
                }
                else
                {
                    // NOTE, MUST CLOSE THE SOCKET

                    socket.Close();
                    isAvailable = false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                isAvailable = false;
            }          

            return isAvailable;
        }

        public static string getLocalIP()
        {
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            return "NO IP FOUND";
        }
    }
}
