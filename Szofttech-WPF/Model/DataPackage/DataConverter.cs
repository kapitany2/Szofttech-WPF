using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using Szofttech_WPF.Logic;

namespace Szofttech_WPF.DataPackage
{
    public class DataConverter
    {
        private static JsonSerializerSettings jsonsettings = new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All };
        public static Data decode(string message)
        {
            Data data = new ChatData(-1, "Exception handled");
            try
            {
                data = (Data)JsonConvert.DeserializeObject(message, jsonsettings);
            }
            catch (Exception)
            {
                Console.WriteLine("MAJDNEM LEFAGYTUNK DESZERIALIZÁLÁS KÖZBEN");
            }
            return data;
            
        }

        public static string encode(Data data)
        {
            string msg;
            try
            {
                msg = JsonConvert.SerializeObject(data, jsonsettings);
            }
            catch (Exception)
            {
                Console.WriteLine("MAJDNEM LEFAGYTUNK SZERIALIZÁLÁS KÖZBEN");
                msg = JsonConvert.SerializeObject(new ChatData(-1, "Exception handled"), jsonsettings);
            }
            return msg;

        }
    }
}
