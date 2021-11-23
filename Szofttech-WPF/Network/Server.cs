using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Szofttech_WPF.Logic;

namespace Szofttech_WPF.Network
{
    public class Server
    {
        private int clientID = 0;
        private List<string>[] queueArray = new List<string>[2];
        private bool close = false;
        private GameLogic gameLogic = null;
        private Socket sSocket = null;

        public void getMessageToQueue(string message, int ID)
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

            Thread threadQueuePoll = new Thread(() => {
                while (!close)
                {
                    Console.WriteLine("Faszcibàlo");
                    Close();
                }
                
            });
            threadQueuePoll.Start();
        }
    }
}
