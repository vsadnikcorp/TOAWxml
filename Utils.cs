using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using TOAWXML;

namespace TOAWXML
{
    public class Utils
    {
        public string ReadFileToString(string filePath)
        {
            StreamReader streamReader = new StreamReader(filePath);
            string text = streamReader.ReadToEnd();
            streamReader.Close();
            return text;
        }

        public static string RemoveSpecialCharacters(string str)
        {
            //change regular expression as per your need
            return Regex.Replace(str, "[^a-zA-Z0-9_.]", "", RegexOptions.Compiled);
        }

        public static string AssignCdrName(string forceID)
        {
            Random rng = new Random();

            string CdrNameDirectory = Path.GetDirectoryName(TOAWXML.Properties.Settings.Default.FilePath);
            string CdrNameFilePath = CdrNameDirectory + "\\CDRNAMES.XML";
            XDocument xdocCDR = XDocument.Load(CdrNameFilePath);

            var commanders = xdocCDR.Descendants("CDRNAMES").Descendants("CDRNAME")
                .Where(f => f.Attribute("forceid").Value == forceID);

            int cdrnameCount = commanders.Count();
            string cdrname = commanders.ElementAt(rng.Next(0, cdrnameCount)).Attribute("cdrname").Value;

            return cdrname;
        }

        public static string RemoveOrdinals(string input)
        {
            // Ugly but oh so simple.
            return input.Replace("0th", "0")
                        .Replace("1st", "1")
                        .Replace("2nd", "2")
                        .Replace("3rd", "3")
                        .Replace("11th", "11") // Need to handle these separately...
                        .Replace("12th", "12")
                        .Replace("13th", "13")
                        .Replace("4th", "4")
                        .Replace("5th", "5")
                        .Replace("6th", "6")
                        .Replace("7th", "7")
                        .Replace("8th", "8")
                        .Replace("9th", "9");
        }
    }
}
