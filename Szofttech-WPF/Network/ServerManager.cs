using System;
using System.Collections.Generic;
using System.IO;

namespace Szofttech_WPF.Network
{
    public class ServerManager
    {
        private static readonly List<ServerAddress> serverList = new List<ServerAddress>();
        private static ServerManager instance = new ServerManager();
        private static readonly char sep = Path.DirectorySeparatorChar;

        public static ServerManager getInstance()
        {
            ReadServersFromFile();
            return instance;
        }

        private static void WriteSavedServersToFile()
        {
            try
            {
                string dir = Directory.GetCurrentDirectory();
                StreamWriter file = new StreamWriter(dir + $"{sep}settings.cfg");
                file.WriteLine("Saved_Servers {");
                foreach (ServerAddress item in serverList)
                {
                    file.WriteLine(item);
                }
                file.WriteLine("}");
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error reading file: " + ex.Message);
            }
            
        }

        private static void ReadServersFromFile()
        {
            try
            {
                string dir = Directory.GetCurrentDirectory();
                StreamReader file = new StreamReader(dir + $"{sep}settings.cfg");

                while (file.Peek() != -1)
                {
                    string data = file.ReadLine();
                    if (data.Contains("Saved_Servers {"))
                    {
                        while (true)
                        {
                            string line = file.ReadLine();
                            if (line.Contains("}"))
                                break;
                            string[] strArr = line.Trim().Split('$');
                            if (strArr.Length != 3)
                                continue;
                            ServerAddress sAddress = new ServerAddress(strArr[0], strArr[1], int.Parse(strArr[2]));
                            serverList.Add(sAddress);
                        }
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("servers.dat file not found");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static List<ServerAddress> GetServers()
        {
            return serverList;
        }

        public static void AddServer(ServerAddress sAddress)
        {
            foreach (ServerAddress item in serverList)
            {
                if(sAddress.Name == item.Name)
                {
                    throw new Exception("SZERVER NÉV MÁR LÉTEZIK!");
                }
            }
        }

        public static void EditServer(string name, ServerAddress newAddress)
        {
            if(name != newAddress.Name)
            {
                foreach (ServerAddress item in serverList)
                {
                    if(newAddress.Name == item.Name)
                    {
                        throw new Exception("SZERVER NÉV MÁR LÉTEZIK!");
                    }
                }
            }

            foreach (ServerAddress item in serverList)
            {
                if(name == item.Name)
                {
                    item.Name = newAddress.Name;
                    item.IP = newAddress.IP;
                    item.Port = newAddress.Port;
                    WriteSavedServersToFile();
                    break;
                }
            }
        }

        public static void DeleteServer(ServerAddress sAddress)
        {
            for (int i = 0; i < serverList.Count; ++i)
            {
                if (serverList[i].Name == sAddress.Name)
                {
                    serverList.RemoveAt(i);
                    WriteSavedServersToFile();
                    break;
                }
            }
        }
    }
}
