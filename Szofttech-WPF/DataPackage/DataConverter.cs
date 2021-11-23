using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Szofttech_WPF.Logic;

namespace Szofttech_WPF.DataPackage
{
    public class DataConverter
    {

        public static Data decode(string message)
        {
            Data data;
            string[] tordelt = (message.Split('$'));

            int id = int.Parse(tordelt[0]);
            string dataType = tordelt[1];
            string dataMessage = tordelt[2];
            int recipientID = int.Parse(tordelt[3]);

            int i, j;

            switch (dataType)
            {
                case "ChatData":
                    data = new ChatData(id, dataMessage);
                    break;
                case "PlaceShipsData":
                    data = new PlaceShipsData(id, new Board(dataMessage));
                    break;
                case "ConnectionData":
                    data = new ConnectionData(id);
                    break;
                case "ShotData":
                    i = int.Parse(dataMessage.Split(';')[0]);
                    j = int.Parse(dataMessage.Split(';')[1]);
                    data = new ShotData(id, i, j);
                    break;
                case "TurnData":
                    data = new TurnData(recipientID);
                    break;
                case "CellData":
                    i = int.Parse(dataMessage.Split(';')[0]);
                    j = int.Parse(dataMessage.Split(';')[1]);
                    CellStatus status;
                    Enum.TryParse(dataMessage.Split(';')[2], out status);
                    data = new CellData(id, i, j, status);
                    break;
                case "GameEndedData":
                    data = new GameEndedData((GameEndedStatus)Enum.Parse(typeof(GameEndedStatus), dataMessage), recipientID);
                    break;
                default:
                    Console.WriteLine("########## ISMERETLEN OSZTÁLY #########");
                    Console.WriteLine("Nincs implementálva a DataConverterben az alábbi osztály: " + dataType);
                    throw new Exception("Not implemented: '" + dataType + "'");
            }
            data.recipientID = recipientID;
            return data;
        }

        public static string encode(int senderID, string type, string data, int recipientID)
        {
            return senderID + "$" + type + "$" + data + "$" + recipientID;
        }

        public static string encode(Data data)
        {
            string encoded = "";
            switch (data.GetType().Name)
            {
                case "ChatData":
                    encoded += data.clientID + "$ChatData$" + ((ChatData)data).getMessage();
                    break;
                case "PlaceShipsData":
                    encoded += data.clientID + "$PlaceShipsData$" + ((PlaceShipsData)data).getBoard().convertToString();
                    break;
                case "ConnectionData":
                    break;
                case "ShotData":
                    encoded += data.clientID + "$ShotData$" + ((ShotData)data).getI() + ";" + ((ShotData)data).getJ();
                    break;
                case "TurnData":
                    encoded += data.clientID + "$TurnData$$" + data.recipientID;
                    break;
                case "CellData":
                    encoded += data.clientID + "$CellData$" + ((CellData)data).I + ";" + ((CellData)data).J + ";" + ((CellData)data).Status;
                    break;
                case "GameEndedData":
                    encoded += data.clientID + "$GameEndedData$" + ((GameEndedData)data).status + "$" + ((GameEndedData)data).recipientID;
                    break;
                default:
                    Console.WriteLine("########## ISMERETLEN OSZTÁLY #########");
                    Console.WriteLine("Nincs implementálva a DataConverterben az alábbi osztály: " + data.GetType().Name);
                    throw new Exception("Not implemented: '" + data.GetType().Name + "'");
                    break;
            }
            encoded += "$" + data.recipientID;
            return encoded;
        }
    }

}
