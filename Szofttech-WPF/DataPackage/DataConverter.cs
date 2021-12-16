using Newtonsoft.Json;
using System;
using Szofttech_WPF.Logic;

namespace Szofttech_WPF.DataPackage
{
    public class DataConverter
    {
        private static JsonSerializerSettings jsonsettings = new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All };
        public static Data decode(string message)
        {
            //Console.WriteLine(message);

            Data data = (Data)JsonConvert.DeserializeObject(message, jsonsettings);
            return data;
        }

        public static string encode(Data data)
        {
            return JsonConvert.SerializeObject(data, jsonsettings);
        }
    }
}
