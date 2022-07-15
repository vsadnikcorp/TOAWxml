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

        public static XElement AddUnitToTac(XElement unit, string date, string unitName, string formID, string forceID)
        {
            string unitcdrname;
            string iconid;
            string x;
            string y;
            string next;

            unitcdrname = Utils.AssignCdrName(forceID);

            if (unit.Attribute("ICONID")?.Value != null)
            {
                iconid = unit.Attribute("ICONID").Value;
            }
            else
            {
                iconid = "";
            }

            if (unit.Attribute("X")?.Value != null)
            {
                x = unit.Attribute("X").Value;
            }
            else
            {
                x = "";
            }

            if (unit.Attribute("Y")?.Value != null)
            {
                y = unit.Attribute("Y").Value;
            }
            else
            {
                y = "";
            }

            if (unit.Attribute("NEXT")?.Value != null)
            {
                next = unit.Attribute("NEXT").Value;
            }
            else
            {
                next = "";
            }

            //ADD MISSING UNIT TO TACFILE
            XElement newunit =
                new XElement("UNIT",
                new XAttribute("ID", unit.Attribute("ID").Value),
                new XAttribute("NAME", unit.Attribute("NAME").Value),
                new XAttribute("ICON", unit.Attribute("ICON").Value),
                new XAttribute("ICONID", iconid),
                new XAttribute("COLOR", unit.Attribute("COLOR").Value),
                new XAttribute("SIZE", unit.Attribute("SIZE").Value),
                new XAttribute("EXPERIENCE", unit.Attribute("EXPERIENCE").Value),
                new XAttribute("CHARACTERISTICS", unit.Attribute("CHARACTERISTICS").Value),
                new XAttribute("PROFICIENCY", unit.Attribute("PROFICIENCY").Value),
                new XAttribute("READINESS", unit.Attribute("READINESS").Value),
                new XAttribute("SUPPLY", unit.Attribute("SUPPLY").Value),
                new XAttribute("X", x),
                new XAttribute("Y", y),
                new XAttribute("EMPHASIS", unit.Attribute("EMPHASIS").Value),
                new XAttribute("NEXT", next),
                new XAttribute("STATUS", unit.Attribute("STATUS").Value),
                new XAttribute("REPLACEMENTPRIORITY", unit.Attribute("REPLACEMENTPRIORITY").Value),
                new XAttribute("CDR", unitcdrname),
                new XAttribute("RANK", "LT"),
                new XAttribute("RATING", "--"),
                new XAttribute("FORMDATE", date));

            Utils.AddEquipToTac(unit, date, unitName, formID, forceID);
            return newunit;
        }

        public static XElement AddEquipToTac(XElement unit, string date, string unitName, string formID, string forceID)
        {
            string equipcdrname;
            bool isFirstEqp = true;
            int n = 0;

            //EQUIPMENT
            foreach (XElement equip in unit.Descendants("EQUIPMENT")
                    .Where(z => z.Parent.Attribute("NAME").Value == unitName)
                    .Where(z => z.Parent.Parent.Attribute("ID").Value == formID)
                    .Where(z => z.Parent.Parent.Parent.Attribute("ID").Value == forceID))
            {
                int qty = Int32.Parse(equip.Attribute("NUMBER").Value);
                string eqpID = equip.Attribute("ID").Value;
                string eqpName = equip.Attribute("NAME").Value;
                string eqpNumber = equip.Attribute("NUMBER").Value;
                string eqpMax = equip.Attribute("MAX").Value;
                string eqpDamage = equip.Attribute("DAMAGE").Value;

                unit.Add(
                       new XElement("EQUIPMENT",
                       new XAttribute("ID", eqpID),
                       new XAttribute("NAME", eqpName),
                       new XAttribute("NUMBER", eqpNumber),
                       new XAttribute("MAX", eqpMax),
                       new XAttribute("DAMAGE", eqpDamage)));

                for (int i = 1; i <= qty; i++) //ITEM
                {
                    if (isFirstEqp == true)
                    {
                        //equipcdrname = unit.Attribute("CDR").Value;
                        equipcdrname = Utils.AssignCdrName(forceID);
                       
                    }
                    else if ((isFirstEqp != true) &&
                    (unit.Attribute("ICON").Value == "Air" ||
                    unit.Attribute("ICON").Value == "Fighter Bomber" ||
                    unit.Attribute("ICON").Value == "Light Bomber" ||
                    unit.Attribute("ICON").Value == "Medium Bomber" ||
                    unit.Attribute("ICON").Value == "Naval Bomber" ||
                    unit.Attribute("ICON").Value == "Heavy Bomber" ||
                    unit.Attribute("ICON").Value == "Jet Bomber" ||
                    unit.Attribute("ICON").Value == "Heavy Jet Bomber" ||
                    unit.Attribute("ICON").Value == "Fighter" ||
                    unit.Attribute("ICON").Value == "Jet Fighter" ||
                    unit.Attribute("ICON").Value == "Naval Fighter" ||
                    unit.Attribute("ICON").Value == "Riverine" ||
                    unit.Attribute("ICON").Value == "Light Naval" ||
                    unit.Attribute("ICON").Value == "Medium Naval" ||
                    unit.Attribute("ICON").Value == "Naval Task Force" ||
                    unit.Attribute("ICON").Value == "Naval Attack"))
                    {
                        equipcdrname = Utils.AssignCdrName(forceID);
                    }
                    else
                    {
                        equipcdrname = "--";
                    }
                    n++;

                    unit.Elements("EQUIPMENT").Last().Add(
                        new XElement("ITEM",
                        new XAttribute("ID", n),
                        new XAttribute("NAME", equip.Attribute("NAME").Value),
                        new XAttribute("ITEMCDR", equipcdrname),
                        new XAttribute("ITEMEXP", "40"),
                        new XAttribute("ITEMKILLS", "0"),
                        new XAttribute("CASUALTY", "None"),
                        new XAttribute("ITEMDAMAGE", "0"),
                        new XAttribute("ITEMFORMDATE", date),
                        new XAttribute("ITEMNOTE", "--")));

                    isFirstEqp = false;
                } //item
            }
            return unit;
        }
    }
}
