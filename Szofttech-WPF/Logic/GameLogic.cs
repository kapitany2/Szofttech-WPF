using System;
using System.Collections.Generic;
using System.Threading;
using Szofttech_WPF.DataPackage;

namespace Szofttech_WPF.Logic
{
    public class GameLogic
    {

        public List<string> messageQueue = new List<string>();
        Random rnd = new Random();
        private Player[] players;

        public GameLogic()
        {
            players = new Player[2];
            players[0] = new Player();
            players[1] = new Player();
        }

        public void processMessage(Data data)
        {
            switch (data.GetType().Name)
            {
                case "ChatData":
                    data.RecipientID = 0;
                    messageQueue.Add(DataConverter.encode((ChatData)data));
                    data.RecipientID = 1;
                    messageQueue.Add(DataConverter.encode((ChatData)data));
                    break;
                case "PlaceShipsData":
                    setPlayerBoard((PlaceShipsData)data);
                    break;
                case "ConnectionData":
                    break;
                case "ShotData":
                    calcShot((ShotData)data);
                    break;
                case "DisconnectData":
                    data.RecipientID = data.ClientID == 1 ? 0 : 1;
                    messageQueue.Add(DataConverter.encode((DisconnectData)data));
                    break;
                default:
                    Console.WriteLine("########## ISMERETLEN OSZTÁLY #########");
                    Console.WriteLine("Nincs implementálva a GameLogicban az alábbi osztály: " + data.GetType().Name);
                    throw new Exception("Not implemented");
            }
        }

        private void calcShot(ShotData data)
        {

            int egyik = data.ClientID;
            int masik = egyik == 1 ? 0 : 1;

            ShotData sd = new ShotData(data.ClientID, data.I, data.J);
            sd.RecipientID = masik;
            messageQueue.Add(DataConverter.encode(sd));

            CellData cd = new CellData(-1, data.I, data.J, players[masik].Board.cellstatus[data.I, data.J]);
            cd.RecipientID = egyik;
            messageQueue.Add(DataConverter.encode(cd));

            if (players[masik].Board.cellstatus[data.I, data.J] == CellStatus.Ship)
            {
                players[masik].Board.cellstatus[data.I, data.J] = CellStatus.ShipHit;
                if (players[masik].Board.isSunk(data.I, data.J))
                {
                    hitNear(egyik, masik, data.I, data.J);
                }
                if (isWin(players[masik]))
                {
                    messageQueue.Add(DataConverter.encode(new GameEndedData(GameEndedStatus.Win, egyik)));
                    messageQueue.Add(DataConverter.encode(new GameEndedData(GameEndedStatus.Defeat, masik)));
                }
                else
                {
                    messageQueue.Add(DataConverter.encode(new TurnData(egyik)));
                }
            }
            else
            {
                messageQueue.Add(DataConverter.encode(new TurnData(masik)));
            }
        }

        private bool isWin(Player player)
        {
            if (player.Board.hasCellStatus(CellStatus.Ship))
                return false;

            return true;
        }

        private void hitNear(int egyik, int masik, int i, int j)
        {
            var tesztMiatt = players[masik].Board.nearShipPoints(i, j);
            foreach (Coordinate nearShipPoint in tesztMiatt)
            {
                CellData cd = new CellData(-1, nearShipPoint.X, nearShipPoint.Y, players[masik].Board.cellstatus[nearShipPoint.X, nearShipPoint.Y]);
                cd.RecipientID = egyik;
                messageQueue.Add(DataConverter.encode(cd));
                ShotData sd = new ShotData(egyik, nearShipPoint.X, nearShipPoint.Y);
                sd.RecipientID = masik;
                messageQueue.Add(DataConverter.encode(sd));
                Thread.Sleep(50);
            }
        }

        private void setPlayerBoard(PlaceShipsData data)
        {
            if (data.ClientID == 0)
            {
                players[0].Identifier = data.ClientID;
                players[0].isReady = true;
                players[0].Board = data.Board;
            }
            else
            {
                players[1].Identifier = data.ClientID;
                players[1].isReady = true;
                players[1].Board = data.Board;
            }

            if (players[0].isReady == true && players[1].isReady == true)
            {
                messageQueue.Add(DataConverter.encode(new TurnData(rnd.Next(1))));
            }
        }
    }
}
