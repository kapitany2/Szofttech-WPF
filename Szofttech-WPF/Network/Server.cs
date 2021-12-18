using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Szofttech_WPF.DataPackage;
using Szofttech_WPF.Logic;
using Szofttech_WPF.Utils;

namespace Szofttech_WPF.Network
{
    public class Server
    {
        private int clientID = 0;
        private LinkedList<string>[] queueArray = new LinkedList<string>[2];
        private bool close = false;
        private GameLogic gameLogic = null;
        private Socket sSocket = null;

        public void addMessageToQueue(string message, int ID)
        {
            if (ID != -1)
                queueArray[ID].AddLast(message + "<EOF>");
        }

        public void Close()
        {
            close = true;
            if (sSocket != null)
                sSocket.Close();
        }

        public Server(int port)
        {
            Console.WriteLine("Opening server on port " + port);
            for (int i = 0; i < 2; ++i)
            {
                queueArray[i] = new LinkedList<string>();
            }
            gameLogic = new GameLogic();
            IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Any, port);
            sSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            sSocket.Bind(localEndPoint);
            sSocket.Listen(4);

            Thread messageDistributorThread = new Thread(() =>
            {
                while (!close)
                {
                    Thread.Sleep(10);
                    while (gameLogic.messageQueue.Count != 0)
                    {
                        string messageToClient = gameLogic.messageQueue[0];
                        gameLogic.messageQueue.RemoveAt(0);
                        if (messageToClient != null)
                        {
                            Data decoded = DataConverter.decode(messageToClient);
                            int recipient = decoded.RecipientID;
                            addMessageToQueue(messageToClient, recipient);
                        }
                    }
                }
            });
            messageDistributorThread.Start();

            for (int i = 0; i < 4; ++i)
                ServeClient();
        }

        private void ServeClient()
        {
            Thread thread = new Thread(() =>
            {
                try
                {

                    while (!close)
                    {
                        Socket socket = sSocket.Accept();

                        byte[] buffer = new byte[1024];
                        string inMsg = null;
                        bool isClient = false;

                        if (!isClient)
                        {
                            if (getInMsg(socket) != "CLIENT")
                            {
                                socket.Close();
                                Console.WriteLine("PING received");
                                continue;
                            }
                            else isClient = true;
                        }

                        int ID = clientID++;
                        int otherQueueID = (ID == 0) ? 1 : 0;
                        int ownQueueID = (ID == 0) ? 0 : 1;
                        Console.WriteLine("Client " + ID + " joined the server.");

                        byte[] message = Encoding.UTF8.GetBytes(ID.ToString() + "<EOF>");
                        socket.Send(message);

                        ConnectionData cData = new ConnectionData(ID);

                        addMessageToQueue(DataConverter.encode(cData), otherQueueID);

                        Thread messageProcessingThread = new Thread(() =>
                        {
                            while (!close)
                            {
                                Thread.Sleep(10);
                                try
                                {
                                    inMsg = getInMsg(socket);
                                    gameLogic.processMessage(DataConverter.decode(inMsg));
                                }
                                catch (Exception) { }
                            }
                        });
                        messageProcessingThread.Start();

                        while (!close)
                        {
                            Thread.Sleep(10);
                            while (queueArray[ownQueueID].Count != 0)
                            {
                                string queueMsg = queueArray[ownQueueID].First.Value;
                                queueArray[ownQueueID].RemoveFirst();
                                byte[] bytes = Encoding.UTF8.GetBytes(queueMsg);
                                socket.Send(bytes);
                            }
                        }
                    }
                }
                catch (Exception) { }

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
                    socket.Close();
                    isAvailable = false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message, ex.StackTrace);
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

        public static List<string> getLocalIPs()
        {
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            List<string> ipList = new List<string>();
            foreach (IPAddress ip in host.AddressList)
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                    ipList.Add(ip.ToString() + ":" + Settings.getPort());

            return ipList;
        }

        private string getInMsg(Socket socket)
        {
            byte[] buffer = new byte[1024];
            string inMsg = null;
            try
            {
                while (true)
                {
                    int numByte = socket.Receive(buffer);
                    inMsg += Encoding.UTF8.GetString(buffer, 0, numByte);

                    if (inMsg.Contains("<EOF>"))
                    {
                        inMsg = inMsg.Replace("<EOF>", "");
                        break;
                    }
                }
            }
            catch { Close(); }

            //Console.WriteLine(inMsg);
            return inMsg;
        }
    }
}
