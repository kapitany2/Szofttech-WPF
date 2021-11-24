using System;
using System.IO;
using System.Reflection;

namespace Szofttech_WPF.Utils
{
    public class Settings
    {
        private static Settings instance = new Settings();
        public static int port = 25564;

        private static readonly FieldInfo[] fields = typeof(Settings).GetFields();
        private static int lineCount = 0;
        private static readonly char sep = Path.DirectorySeparatorChar;

        private Settings() { }

        public static Settings getInstance()
        {
            Read();
            return instance;
        }

        private static void Read()
        {
            string dir = Directory.GetCurrentDirectory();
            StreamReader file = null;
            try
            {
                file = new StreamReader(dir + $"{sep}settings.cfg");
                while (file.Peek() >= 0)
                {
                    string line = file.ReadLine();
                    foreach (FieldInfo variable in fields)
                    {
                        SetValue(line, variable);
                    }
                }
                file.Close();
                if (lineCount != fields.Length)
                {
                    Console.WriteLine("Potenciális elírás a settings.cfg fájlban...");
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine(dir + $"{sep}settings.cfg fájl nem létezik");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hiba lépett fel a settings.cfg fájl olvasása közben...\nhibaüzenet: " + ex.Message);
            }
            finally
            {
                if (file != null)
                {
                    file.Close();
                }
                Save();
            }
        }

        private static void SetValue(string line, FieldInfo variable)
        {
            if (line.Contains(variable.Name))
            {
                variable.SetValue(null, int.Parse(line.Split(' ')[1]));
                ++lineCount;
            }
        }

        public static void Save()
        {
            string dir = Directory.GetCurrentDirectory();
            StreamWriter file = new StreamWriter(dir + $"{sep}settings.cfg");
            foreach (FieldInfo item in fields)
            {
                file.WriteLine(item.Name + " " + item.GetValue(null));
            }
            file.Close();
        }

        public static string getIP()
        {
            return "0.0.0.0";
        }

        public static int getPort()
        {
            return port;
        }

        public static void setPort(int _port)
        {
            port = _port;
        }
    }
}
