using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;

namespace MapTools
{
    internal class JSONTools
    {
        public JSONTools() { }
        //Pour sérialiser en JSON voici un exemple :
        public void WriteJSON(int[,] pData, string pFileName)
        {
            StreamWriter file;
            string json = JsonSerializer.Serialize(pData);
            file = File.CreateText(pFileName);
            file.WriteLine(json);
            file.Close();
        }

        //Et pour désérialiser :
        public void ReadJSON(ref int[,] mapData, string pFileName)
        {
            StreamReader file;
            file = File.OpenText(pFileName);
            string json = file.ReadToEnd();
            mapData = JsonSerializer.Deserialize<int[,]>(json);
        }
    }
}
