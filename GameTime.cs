using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;

namespace TOAWXML
{
    public class GameTime
    {
        //private string year;
        //private string month;
        //private string day;
        //private string hour;

        public GameTime()
        {

        }

        public static DateTime getCurrentGameDate()
        {
            XElement xelem = XElement.Load(TOAWXML.Properties.Settings.Default.FilePath);
            string dateXPath = "CALENDAR";
            var currentDate = xelem.XPathSelectElement(dateXPath);

            String currentYear = ((Convert.ToInt32(currentDate.Attribute("startYear").Value)) + 1).ToString();
            String currentMonth = (DateTimeFormatInfo.CurrentInfo.GetAbbreviatedMonthName((Convert.ToInt32(currentDate.Attribute("startMonth").Value)) + 1));
            String currentDay = ((Convert.ToInt32(currentDate.Attribute("startDay").Value)) + 1).ToString();
            String timeIndex = currentDate.Attribute("startHour").Value.ToString();
            String turnLengthIndex = currentDate.Attribute("turnLength").Value;
            String currentTime = "00:00";

            switch (turnLengthIndex)
            {
                case "9":
                    switch (timeIndex)
                    {
                        case "23":
                            currentTime = "00:00";
                            break;
                        case "1":
                            currentTime = "01:00";
                            break;
                        case "2":
                            currentTime = "02:00";
                            break;
                        case "3":
                            currentTime = "03:00";
                            break;
                        case "4":
                            currentTime = "04:00";
                            break;
                        case "5":
                            currentTime = "05:00";
                            break;
                        case "6":
                            currentTime = "06:00";
                            break;
                        case "7":
                            currentTime = "07:00";
                            break;
                        case "8":
                            currentTime = "08:00";
                            break;
                        case "9":
                            currentTime = "09:00";
                            break;
                        case "10":
                            currentTime = "10:00";
                            break;
                        case "11":
                            currentTime = "11:00";
                            break;
                        case "12":
                            currentTime = "12:00";
                            break;
                        case "13":
                            currentTime = "13:00";
                            break;
                        case "14":
                            currentTime = "14:00";
                            break;
                        case "15":
                            currentTime = "15:00";
                            break;
                        case "16":
                            currentTime = "16:00";
                            break;
                        case "17":
                            currentTime = "17:00";
                            break;
                        case "18":
                            currentTime = "18:00";
                            break;
                        case "19":
                            currentTime = "19:00";
                            break;
                        case "20":
                            currentTime = "20:00";
                            break;
                        case "21":
                            currentTime = "21:00";
                            break;
                        case "22":
                            currentTime = "23:00";
                            break;
                    }
                    break;

                case "10":
                    switch (timeIndex)
                    {
                        case "0":
                            currentTime = "00:00";
                            break;
                        case "3":
                            currentTime = "03:00";
                            break;
                        case "6":
                            currentTime = "06:00";
                            break;
                        case "9":
                            currentTime = "09:00";
                            break;
                        case "12":
                            currentTime = "12:00";
                            break;
                        case "15":
                            currentTime = "15:00";
                            break;
                        case "18":
                            currentTime = "18:00";
                            break;
                        case "21":
                            currentTime = "21:00";
                            break;
                    }
                    break;

                case "0":
                    switch (timeIndex)
                    {
                        case "0":
                            currentTime = "00:00"; //PRE-DAWN
                            break;
                        case "6":
                            currentTime = "06:00"; //MORNING
                            break;
                        case "12":
                            currentTime = "12:00"; //AFTERNOON
                            break;
                        case "18":
                            currentTime = "18:00"; //NIGHT
                            break;
                    }
                    break;

                case "1":
                    switch (timeIndex)
                    {
                        case "12":
                            currentTime = "00:00"; //AM
                            break;
                        case "0":
                            currentTime = "12:00"; //PM
                            break;
                    }
                    break;
                default:
                    currentTime = "00:00";
                    break;
            }


            String currentDateTime = currentDay + " " + currentMonth + " " + currentYear + " " + currentTime;
            String dateFormat = "d MMM yyyy HH:mm";

            CultureInfo culture = CultureInfo.InvariantCulture;
            DateTime gameDate = DateTime.ParseExact(currentDateTime, dateFormat, culture);

            return gameDate;
        }

        public static int getTurnLength()
        {
            XElement xelem = XElement.Load(TOAWXML.Properties.Settings.Default.FilePath);
            string dateXPath = "CALENDAR";
            var currentTime = xelem.XPathSelectElement(dateXPath);
            String turnLengthIndex = currentTime.Attribute("turnLength").Value.ToString();
            int turnLength = 0;

            switch (turnLengthIndex)
            {
                case "9":
                    turnLength = 1;
                    break;
                case "10":
                    turnLength = 3;
                    break;
                case "0":
                    turnLength = 6;
                    break;
                case "1":
                    turnLength = 12;
                    break;
                case "2":
                    turnLength = 24;
                    break;
                case "3":
                    turnLength = 84;
                    break;
                case "4":
                    turnLength = 168;
                    break;
                case "5":
                    turnLength = 336;
                    break;
                default:
                    turnLength = 0;
                    break;
            }

            return turnLength;
        }
        //}

        public static string getReleaseDate(string date)
        {
            DateTime gameDateTime = GameTime.getCurrentGameDate();
            int turnLength = GameTime.getTurnLength();
            string releasedate = "";

            if (turnLength > 0)
            {
                int turnSpan = ((Convert.ToInt32(date) - 1) * turnLength);
                TimeSpan timeSpan = TimeSpan.FromHours(turnSpan);
                DateTime reinfDateTime = gameDateTime + timeSpan;
                releasedate = reinfDateTime.ToString("d MMM yyyy" + " @ " + "HH:mm");
            }
            else
            {
                releasedate = "";
            }

            return releasedate;
        }
    }
}
