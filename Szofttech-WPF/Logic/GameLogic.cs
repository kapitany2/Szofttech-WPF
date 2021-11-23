using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Szofttech_WPF.DataPackage;

namespace Szofttech_WPF.Logic
{
    public class GameLogic
    {

        //public List<string> messageQueue = new List<string>();
        //Random rnd = new Random();
        //Player player1;
        //Player player2;
        //private Player[] players;

        //public GameLogic()
        //{
        //    player1 = new Player();
        //    player2 = new Player();
        //    players = new Player[2];
        //    players[0] = new Player();
        //    players[1] = new Player();
        //}

        //public void processMessage(Data data)
        //{
        //    switch (data.getClass().getSimpleName())
        //    {
        //        case "ChatData":
        //            data.setRecipientID(0);
        //            messageQueue.Add(DataConverter.encode((ChatData)data));
        //            data.setRecipientID(1);
        //            messageQueue.Add(DataConverter.encode((ChatData)data));
        //            break;
        //        case "PlaceShipsData":
        //            setPlayerBoard((PlaceShipsData)data);
        //            break;
        //        case "ConnectionData":
        //            break;
        //        case "ShotData":
        //            calcShot((ShotData)data);
        //            break;
        //        case "":
        //            break;
        //        default:
        //            Console.WriteLine("########## ISMERETLEN OSZTÁLY #########");
        //            Console.WriteLine("Nincs implementálva a GameLogicban az alábbi osztály: " + data.getClass().getSimpleName());
        //            throw new Exception("Not implemented");
        //    }
        //}

        //private void calcShot(ShotData data)
        //{

        //    int egyik = data.getClientID();
        //    int masik = egyik == 1 ? 0 : 1;

        //    ShotData sd = new ShotData(data.getClientID(), data.getI(), data.getJ());
        //    sd.setRecipientID(masik);
        //    messageQueue.Add(DataConverter.encode(sd));

        //    CellData cd = new CellData(-1, data.getI(), data.getJ(), players[masik].Board.cellstatus[data.getI(), data.getJ()]);
        //    cd.setRecipientID(egyik);
        //    messageQueue.Add(DataConverter.encode(cd));

        //    if (players[masik].Board.cellstatus[data.getI(), data.getJ()] == CellStatus.Ship)
        //    {
        //        players[masik].Board.cellstatus[data.getI(), data.getJ()] = CellStatus.ShipHit;
        //        if (players[masik].Board.isSunk(data.getI(), data.getJ()))
        //        {
        //            hitNear(egyik, masik, data.getI(), data.getJ());
        //        }
        //        if (isWin(players[masik]))
        //        {
        //            messageQueue.Add(DataConverter.encode(new GameEndedData(GameEndedStatus.Win, egyik)));
        //            messageQueue.Add(DataConverter.encode(new GameEndedData(GameEndedStatus.Defeat, masik)));
        //        }
        //        else
        //        {
        //            messageQueue.Add(DataConverter.encode(new TurnData(egyik)));
        //        }
        //    }
        //    else
        //    {
        //        messageQueue.Add(DataConverter.encode(new TurnData(masik)));
        //    }
        //}

        //private bool isWin(Player player)
        //{
        //    if (player.Board.hasCellStatus(CellStatus.Ship))
        //    {
        //        return false;
        //    }
        //    return true;
        //}

        //private void hitNear(int egyik, int masik, int i, int j)
        //{
        //    foreach (Coordinate nearShipPoint in players[masik].Board.nearShipPoints(i, j))
        //    {
        //        CellData cd = new CellData(-1, nearShipPoint.X, nearShipPoint.Y, players[masik].Board.cellstatus[nearShipPoint.X, nearShipPoint.Y]);
        //        cd.setRecipientID(egyik);
        //        messageQueue.Add(DataConverter.encode(cd));
        //        ShotData sd = new ShotData(egyik, nearShipPoint.X, nearShipPoint.Y);
        //        sd.setRecipientID(masik);
        //        messageQueue.Add(DataConverter.encode(sd));
        //    }
        //}

        //private void setPlayerBoard(PlaceShipsData data)
        //{
        //    if (data.getClientID() == 0)
        //    {
        //        player1.Identifier = data.getClientID();
        //        player1.Board = data.getBoard();
        //        player1.isReady = true;
        //        players[0].Identifier = data.getClientID();
        //        players[0].isReady = true;
        //        players[0].Board = data.getBoard();
        //    }
        //    else
        //    {
        //        player2.Identifier = data.getClientID();
        //        player2.Board = data.getBoard();
        //        player2.isReady = true;
        //        players[1].Identifier = data.getClientID();
        //        players[1].isReady = true;
        //        players[1].Board = data.getBoard();
        //    }

        //    if (player1.isReady == true && player2.isReady == true)
        //    {
        //        messageQueue.Add(DataConverter.encode(new TurnData(rnd.Next(1))));
        //    }
        //}
    }

}
