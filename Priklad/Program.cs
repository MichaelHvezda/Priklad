//using Newtonsoft.Json;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
//using System.Text.Json;

namespace Priklad
{
    class Program
    {
        static void Main(string[] args)
        {
            //ziskani cesty
            var cesta = Path.GetFullPath("..\\..\\..\\");

            //nastaveni excel souboru
            ExcelReader excelReader = new ExcelReader((cesta+ "excel.xlsx"));

            //ziskani seznamu
            List<Zakazka> zakazky = new List<Zakazka>(excelReader.openAndReadData());

            //vytvoreni json souboru
            string json = JsonConvert.SerializeObject(zakazky, Formatting.Indented);
            
            //ulozeni json souboru
            File.WriteAllText(cesta + "json.json", json);

            Console.WriteLine("json ulozen");
        }
    }
}
