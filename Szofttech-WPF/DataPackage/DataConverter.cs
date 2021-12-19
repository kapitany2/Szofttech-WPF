using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using Szofttech_WPF.Logic;

namespace Szofttech_WPF.DataPackage
{
    public class DataConverter
    {
        private static JsonSerializerSettings jsonsettings = new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All, Formatting = Formatting.Indented };
        public static Data decode(string message)
        {
            //Console.WriteLine(message);
            //Data data;
            //try
            //{
                //data = (Data)JsonConvert.DeserializeObject(message, jsonsettings);
            //}
            //catch (Exception e)
            //{
            //    return new ChatData(-1, "Error\n" + e.Message);
            //}
            //return data;
            return (Data)JsonConvert.DeserializeObject(message, jsonsettings);
        }

        public static List<Data> tryDecode(string message)
        {
            List<Data> datas = new List<Data>();
            string[] messages = message.Split('}');
            for (int i = 0; i < messages.Length; i++)
            {
                messages[i] += "}";
            }
            for (int i = 0; i < messages.Length-1; i++)
            {
                datas.Add(decode(messages[i]));
            }
            return datas;
        }

        public static string encode(Data data)
        {
            //string a = JsonConvert.SerializeObject(data, jsonsettings);
            //int count = a.Count(f => f == '$');
            //if (a.Length > 150 && !(data is PlaceShipsData))//normál esetben csak a placeshipdata küld ekkoránál nagyobbat, vagy a chatdata
            //{
            //    Console.WriteLine("nagyobb a message mint 150: " + a.Length + "\n" + a);
            //}
            //if (count > 1 && !(data is PlaceShipsData))//ha több típust serializál
            //{
            //    Console.WriteLine("Több típust serializáltam, ennyit: " + count);
            //}
            //return a;
            return JsonConvert.SerializeObject(data, jsonsettings);
        }
    }
}
