using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Szofttech_WPF.DataPackage;
using Szofttech_WPF.EventArguments.Chat;

namespace Szofttech_WPF.Network
{
    public class Client
    {
        public int ID;

        private List<string> messageQueue = new List<string>();
        private string ip;
        private int port;
        private bool close = false;
        private bool timedOut = false;
        public event EventHandler<MessageReveivedArgs> OnMessageReceived;
        public event EventHandler<GameEndedArgs> OnGameEnded;
        public event EventHandler<EnemyHitMeArgs> OnEnemyHitMe;
        public event EventHandler<MyHitArgs> OnMyHit;
        public event EventHandler OnYourTurn, OnJoinedEnemy;

        public Client(string ip, int port)
        {
            this.ip = ip;
            this.port = port;
            Thread thread = new Thread(() =>
            {
                run();
            });
            thread.Start();
        }

        public Client() { }

        public void Connect(string ip, int port)
        {
            this.ip = ip;
            this.port = port;

            Thread thread = new Thread(() =>
            {
                run();
            });
            thread.Start();
        }

        public bool isTimeout()
        {
            return timedOut;
        }

        public void sendMessage(string message)
        {
            message += "<EOF>";
            messageQueue.Add(message);
        }

        public void sendMessage(Data data)
        {
            string message = DataConverter.encode(data);
            messageQueue.Add(message);
        }

        public void Close()
        {
            close = true;
        }

        private void run()
        {
            try
            {
                Socket socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint serverAddress = new IPEndPoint(IPAddress.Parse(ip), port);
                socket.Connect(serverAddress);

                byte[] bytes = Encoding.ASCII.GetBytes("CLIENT<EOF>");
                socket.Send(bytes);

                Thread thread = new Thread(() =>
                {
                    while (!close)
                    {
                        Thread.Sleep(10);
                        string inMsg = getInMsg(socket);
                        if (inMsg == "0" || inMsg == "1")
                        {
                            ID = int.Parse(inMsg);
                            continue;
                        }

                        if (inMsg != null)
                        {
                            Data data = DataConverter.decode(inMsg);
                            switch (data.GetType().Name)
                            {
                                case "ChatData":
                                    //ChatData
                                    break;
                                case "PlaceShipsData":
                                    //PlaceShipsData
                                    break;
                                case "ConnectionData":
                                    //ConnectionData
                                    break;
                                case "ShotData":
                                    //ShotData
                                    break;
                                case "CellData":
                                    //CellData
                                    break;
                                case "TurnData":
                                    //TurnData
                                    break;
                                case "GameEndedData":
                                    //GameEndedData
                                    break;
                                default:
                                    //NOT IMPLEMENTED
                                    Console.WriteLine("Nincs implementálva a Client-ben az alábbi osztály: " + data.GetType().Name);
                                    break;
                            }
                        }
                    }
                });
                thread.Start();

                while (!close)
                {
                    Thread.Sleep(10);
                    while (messageQueue.Count != 0)
                    {
                        string message = messageQueue[0];
                        messageQueue.RemoveAt(0);
                        socket.Send(Encoding.ASCII.GetBytes(message));
                    }
                }
                try
                {
                    socket.Send(Encoding.ASCII.GetBytes("$DisconnectData$$-1"));
                    socket.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message, ex.StackTrace);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message, ex.StackTrace);
                timedOut = true;
            }
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
                    inMsg += Encoding.ASCII.GetString(buffer, 0, numByte);

                    if (inMsg.IndexOf("<EOF>") > -1)
                    {
                        inMsg = inMsg.Replace("<EOF>", "");
                        break;
                    }
                }
            }
            catch
            {
                Console.WriteLine("Szerver elpusztult amíg a kliens rajta volt. Ez nem egy hiba.");
            }

            Console.WriteLine(inMsg);
            return inMsg;
        }
    }
}
