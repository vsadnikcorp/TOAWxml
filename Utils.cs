using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace TOAWXML
{
    class Utils
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
    }
}
