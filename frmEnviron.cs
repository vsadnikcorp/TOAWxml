using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Globalization;

namespace TOAWXML
{
    public partial class frmEnviron : Form
    {
        public frmEnviron()
        {
            InitializeComponent();
        }

        private void frmEnviron_Load(object sender, EventArgs e)
        {
            //RETRIEVE CALENDAR DATA FROM XML
            string xpath = "CALENDAR";
            XElement xelem = XElement.Load(Globals.GlobalVariables.PATH);
            var calendar = xelem.XPathSelectElement(xpath);

            //RETRIEVE ENVIRONMENT DATA FROM XML
            string xpath2 = "ENVIRONMENT";
            var environment = xelem.XPathSelectElement(xpath2);

            //RETRIEVE WEATHERZONE1 DATA FROM XML
            string xpathwz1 = "WEATHERZONES/ZONE[@ID=1]";
            var weatherzone1 = xelem.XPathSelectElement(xpathwz1);

            //RETRIEVE WEATHERZONE2 DATA FROM XML
            string xpathwz2 = "WEATHERZONES/ZONE[@ID=2]";
            var weatherzone2 = xelem.XPathSelectElement(xpathwz2);

            //RETRIEVE WEATHERZONE1 DATA FROM XML
            string xpathwz3 = "WEATHERZONES/ZONE[@ID=3]";
            var weatherzone3 = xelem.XPathSelectElement(xpathwz3);

            //POPULATES TURN LENGTH COMBOBOX
            var turnlength = new BindingList<KeyValuePair<string, string>>();
            turnlength.Add(new KeyValuePair<string, string>("9", "1 Hour"));
            turnlength.Add(new KeyValuePair<string, string>("10", "3 Hours"));
            turnlength.Add(new KeyValuePair<string, string>("0", "6 Hours"));
            turnlength.Add(new KeyValuePair<string, string>("1", "12 Hours"));
            turnlength.Add(new KeyValuePair<string, string>("2", "24 Hours"));
            turnlength.Add(new KeyValuePair<string, string>("3", "Half Week"));
            turnlength.Add(new KeyValuePair<string, string>("4", "One Week"));
            turnlength.Add(new KeyValuePair<string, string>("5", "Two Weeks"));
            turnlength.Add(new KeyValuePair<string, string>("6", "Month"));
            turnlength.Add(new KeyValuePair<string, string>("7", "Season"));
            turnlength.Add(new KeyValuePair<string, string>("6", "Year"));

            cboTurnLength.DataSource = turnlength;
            cboTurnLength.ValueMember = "Key";
            cboTurnLength.DisplayMember = "Value";

            //SET CBOTURNLENGTH VALUE BASED ON XML
            cboTurnLength.SelectedValue = calendar.Attribute("turnLength").Value.ToString();

            //POPULATES START HOUR COMBOBOX
            var starthour = new BindingList<KeyValuePair<string, string>>();
            string strturnlength = cboTurnLength.SelectedValue.ToString();

            switch (strturnlength)
            {
                case "9":
                    starthour = new BindingList<KeyValuePair<string, string>>();
                    starthour.Add(new KeyValuePair<string, string>("23", "Midnight"));
                    starthour.Add(new KeyValuePair<string, string>("1", "01:00"));
                    starthour.Add(new KeyValuePair<string, string>("2", "02:00"));
                    starthour.Add(new KeyValuePair<string, string>("3", "03:00"));
                    starthour.Add(new KeyValuePair<string, string>("4", "04:00"));
                    starthour.Add(new KeyValuePair<string, string>("5", "05:00"));
                    starthour.Add(new KeyValuePair<string, string>("6", "06:00"));
                    starthour.Add(new KeyValuePair<string, string>("7", "07:00"));
                    starthour.Add(new KeyValuePair<string, string>("8", "08:00"));
                    starthour.Add(new KeyValuePair<string, string>("9", "09:00"));
                    starthour.Add(new KeyValuePair<string, string>("10", "10:00"));
                    starthour.Add(new KeyValuePair<string, string>("11", "11:00"));
                    starthour.Add(new KeyValuePair<string, string>("12", "12:00"));
                    starthour.Add(new KeyValuePair<string, string>("13", "13:00"));
                    starthour.Add(new KeyValuePair<string, string>("14", "14:00"));
                    starthour.Add(new KeyValuePair<string, string>("15", "15:00"));
                    starthour.Add(new KeyValuePair<string, string>("16", "16:00"));
                    starthour.Add(new KeyValuePair<string, string>("17", "17:00"));
                    starthour.Add(new KeyValuePair<string, string>("18", "18:00"));
                    starthour.Add(new KeyValuePair<string, string>("19", "19:00"));
                    starthour.Add(new KeyValuePair<string, string>("20", "20:00"));
                    starthour.Add(new KeyValuePair<string, string>("21", "21:00"));
                    starthour.Add(new KeyValuePair<string, string>("22", "23:00"));
                    break;

                case "10":
                    starthour = new BindingList<KeyValuePair<string, string>>();
                    starthour.Add(new KeyValuePair<string, string>("0", "Midnight"));
                    starthour.Add(new KeyValuePair<string, string>("3", "03:00"));
                    starthour.Add(new KeyValuePair<string, string>("6", "06:00"));
                    starthour.Add(new KeyValuePair<string, string>("9", "09:00"));
                    starthour.Add(new KeyValuePair<string, string>("12", "12:00"));
                    starthour.Add(new KeyValuePair<string, string>("15", "15:00"));
                    starthour.Add(new KeyValuePair<string, string>("18", "18:00"));
                    starthour.Add(new KeyValuePair<string, string>("21", "21:00"));
                    break;

                case "0":
                    starthour = new BindingList<KeyValuePair<string, string>>();
                    starthour.Add(new KeyValuePair<string, string>("0", "Pre-Dawn"));
                    starthour.Add(new KeyValuePair<string, string>("6", "Morning"));
                    starthour.Add(new KeyValuePair<string, string>("12", "Afternoon"));
                    starthour.Add(new KeyValuePair<string, string>("18", "Night"));
                    break;

                case "1":
                    starthour = new BindingList<KeyValuePair<string, string>>();
                    starthour.Add(new KeyValuePair<string, string>("12", "AM"));
                    starthour.Add(new KeyValuePair<string, string>("0", "PM"));
                    break;

                default:
                    starthour = new BindingList<KeyValuePair<string, string>>();
                    starthour.Add(new KeyValuePair<string, string>("0", "--"));
                    break;
            }

            cboStartHour.DataSource = starthour;
            cboStartHour.ValueMember = "Key";
            cboStartHour.DisplayMember = "Value";
            cboStartHour.SelectedValue = calendar.Attribute("startHour").Value.ToString();

            //POPULATES START MONTH COMBOBOX
            var startmonth = new BindingList<KeyValuePair<string, string>>();
            startmonth.Add(new KeyValuePair<string, string>("0", "January"));
            startmonth.Add(new KeyValuePair<string, string>("1", "February"));
            startmonth.Add(new KeyValuePair<string, string>("2", "March"));
            startmonth.Add(new KeyValuePair<string, string>("3", "April"));
            startmonth.Add(new KeyValuePair<string, string>("4", "May"));
            startmonth.Add(new KeyValuePair<string, string>("5", "June"));
            startmonth.Add(new KeyValuePair<string, string>("6", "July"));
            startmonth.Add(new KeyValuePair<string, string>("7", "August"));
            startmonth.Add(new KeyValuePair<string, string>("8", "September"));
            startmonth.Add(new KeyValuePair<string, string>("9", "October"));
            startmonth.Add(new KeyValuePair<string, string>("10", "November"));
            startmonth.Add(new KeyValuePair<string, string>("11", "December"));

            cboStartMonth.DataSource = startmonth;
            cboStartMonth.ValueMember = "Key";
            cboStartMonth.DisplayMember = "Value";

            //SET CBOSTARTMONTH VALUE BASED ON XML
            cboStartMonth.SelectedValue = calendar.Attribute("startMonth").Value.ToString();

            //POPULATES START DAY COMBOBOX
            string strStartmonth = calendar.Attribute("startMonth").Value.ToString();
            var startday = new BindingList<KeyValuePair<string, string>>();
            switch (strStartmonth)
            {
                case "0":
                    startday.Add(new KeyValuePair<string, string>("0", "1"));
                    startday.Add(new KeyValuePair<string, string>("1", "2"));
                    startday.Add(new KeyValuePair<string, string>("2", "3"));
                    startday.Add(new KeyValuePair<string, string>("3", "4"));
                    startday.Add(new KeyValuePair<string, string>("4", "5"));
                    startday.Add(new KeyValuePair<string, string>("5", "6"));
                    startday.Add(new KeyValuePair<string, string>("6", "7"));
                    startday.Add(new KeyValuePair<string, string>("7", "8"));
                    startday.Add(new KeyValuePair<string, string>("8", "9"));
                    startday.Add(new KeyValuePair<string, string>("9", "10"));
                    startday.Add(new KeyValuePair<string, string>("10", "11"));
                    startday.Add(new KeyValuePair<string, string>("11", "12"));
                    startday.Add(new KeyValuePair<string, string>("12", "13"));
                    startday.Add(new KeyValuePair<string, string>("13", "14"));
                    startday.Add(new KeyValuePair<string, string>("14", "15"));
                    startday.Add(new KeyValuePair<string, string>("15", "16"));
                    startday.Add(new KeyValuePair<string, string>("16", "17"));
                    startday.Add(new KeyValuePair<string, string>("17", "18"));
                    startday.Add(new KeyValuePair<string, string>("18", "19"));
                    startday.Add(new KeyValuePair<string, string>("19", "20"));
                    startday.Add(new KeyValuePair<string, string>("20", "21"));
                    startday.Add(new KeyValuePair<string, string>("21", "22"));
                    startday.Add(new KeyValuePair<string, string>("22", "23"));
                    startday.Add(new KeyValuePair<string, string>("23", "24"));
                    startday.Add(new KeyValuePair<string, string>("24", "25"));
                    startday.Add(new KeyValuePair<string, string>("25", "26"));
                    startday.Add(new KeyValuePair<string, string>("26", "27"));
                    startday.Add(new KeyValuePair<string, string>("27", "28"));
                    startday.Add(new KeyValuePair<string, string>("28", "29"));
                    startday.Add(new KeyValuePair<string, string>("29", "30"));
                    startday.Add(new KeyValuePair<string, string>("30", "31"));
                    break;
                case "1":
                    startday.Add(new KeyValuePair<string, string>("0", "1"));
                    startday.Add(new KeyValuePair<string, string>("1", "2"));
                    startday.Add(new KeyValuePair<string, string>("2", "3"));
                    startday.Add(new KeyValuePair<string, string>("3", "4"));
                    startday.Add(new KeyValuePair<string, string>("4", "5"));
                    startday.Add(new KeyValuePair<string, string>("5", "6"));
                    startday.Add(new KeyValuePair<string, string>("6", "7"));
                    startday.Add(new KeyValuePair<string, string>("7", "8"));
                    startday.Add(new KeyValuePair<string, string>("8", "9"));
                    startday.Add(new KeyValuePair<string, string>("9", "10"));
                    startday.Add(new KeyValuePair<string, string>("10", "11"));
                    startday.Add(new KeyValuePair<string, string>("11", "12"));
                    startday.Add(new KeyValuePair<string, string>("12", "13"));
                    startday.Add(new KeyValuePair<string, string>("13", "14"));
                    startday.Add(new KeyValuePair<string, string>("14", "15"));
                    startday.Add(new KeyValuePair<string, string>("15", "16"));
                    startday.Add(new KeyValuePair<string, string>("16", "17"));
                    startday.Add(new KeyValuePair<string, string>("17", "18"));
                    startday.Add(new KeyValuePair<string, string>("18", "19"));
                    startday.Add(new KeyValuePair<string, string>("19", "20"));
                    startday.Add(new KeyValuePair<string, string>("20", "21"));
                    startday.Add(new KeyValuePair<string, string>("21", "22"));
                    startday.Add(new KeyValuePair<string, string>("22", "23"));
                    startday.Add(new KeyValuePair<string, string>("23", "24"));
                    startday.Add(new KeyValuePair<string, string>("24", "25"));
                    startday.Add(new KeyValuePair<string, string>("25", "26"));
                    startday.Add(new KeyValuePair<string, string>("26", "27"));
                    startday.Add(new KeyValuePair<string, string>("27", "28"));
                    break;
                case "2":
                    startday.Add(new KeyValuePair<string, string>("0", "1"));
                    startday.Add(new KeyValuePair<string, string>("1", "2"));
                    startday.Add(new KeyValuePair<string, string>("2", "3"));
                    startday.Add(new KeyValuePair<string, string>("3", "4"));
                    startday.Add(new KeyValuePair<string, string>("4", "5"));
                    startday.Add(new KeyValuePair<string, string>("5", "6"));
                    startday.Add(new KeyValuePair<string, string>("6", "7"));
                    startday.Add(new KeyValuePair<string, string>("7", "8"));
                    startday.Add(new KeyValuePair<string, string>("8", "9"));
                    startday.Add(new KeyValuePair<string, string>("9", "10"));
                    startday.Add(new KeyValuePair<string, string>("10", "11"));
                    startday.Add(new KeyValuePair<string, string>("11", "12"));
                    startday.Add(new KeyValuePair<string, string>("12", "13"));
                    startday.Add(new KeyValuePair<string, string>("13", "14"));
                    startday.Add(new KeyValuePair<string, string>("14", "15"));
                    startday.Add(new KeyValuePair<string, string>("15", "16"));
                    startday.Add(new KeyValuePair<string, string>("16", "17"));
                    startday.Add(new KeyValuePair<string, string>("17", "18"));
                    startday.Add(new KeyValuePair<string, string>("18", "19"));
                    startday.Add(new KeyValuePair<string, string>("19", "20"));
                    startday.Add(new KeyValuePair<string, string>("20", "21"));
                    startday.Add(new KeyValuePair<string, string>("21", "22"));
                    startday.Add(new KeyValuePair<string, string>("22", "23"));
                    startday.Add(new KeyValuePair<string, string>("23", "24"));
                    startday.Add(new KeyValuePair<string, string>("24", "25"));
                    startday.Add(new KeyValuePair<string, string>("25", "26"));
                    startday.Add(new KeyValuePair<string, string>("26", "27"));
                    startday.Add(new KeyValuePair<string, string>("27", "28"));
                    startday.Add(new KeyValuePair<string, string>("28", "29"));
                    startday.Add(new KeyValuePair<string, string>("29", "30"));
                    startday.Add(new KeyValuePair<string, string>("30", "31"));
                    break;
                case "3":
                    startday.Add(new KeyValuePair<string, string>("0", "1"));
                    startday.Add(new KeyValuePair<string, string>("1", "2"));
                    startday.Add(new KeyValuePair<string, string>("2", "3"));
                    startday.Add(new KeyValuePair<string, string>("3", "4"));
                    startday.Add(new KeyValuePair<string, string>("4", "5"));
                    startday.Add(new KeyValuePair<string, string>("5", "6"));
                    startday.Add(new KeyValuePair<string, string>("6", "7"));
                    startday.Add(new KeyValuePair<string, string>("7", "8"));
                    startday.Add(new KeyValuePair<string, string>("8", "9"));
                    startday.Add(new KeyValuePair<string, string>("9", "10"));
                    startday.Add(new KeyValuePair<string, string>("10", "11"));
                    startday.Add(new KeyValuePair<string, string>("11", "12"));
                    startday.Add(new KeyValuePair<string, string>("12", "13"));
                    startday.Add(new KeyValuePair<string, string>("13", "14"));
                    startday.Add(new KeyValuePair<string, string>("14", "15"));
                    startday.Add(new KeyValuePair<string, string>("15", "16"));
                    startday.Add(new KeyValuePair<string, string>("16", "17"));
                    startday.Add(new KeyValuePair<string, string>("17", "18"));
                    startday.Add(new KeyValuePair<string, string>("18", "19"));
                    startday.Add(new KeyValuePair<string, string>("19", "20"));
                    startday.Add(new KeyValuePair<string, string>("20", "21"));
                    startday.Add(new KeyValuePair<string, string>("21", "22"));
                    startday.Add(new KeyValuePair<string, string>("22", "23"));
                    startday.Add(new KeyValuePair<string, string>("23", "24"));
                    startday.Add(new KeyValuePair<string, string>("24", "25"));
                    startday.Add(new KeyValuePair<string, string>("25", "26"));
                    startday.Add(new KeyValuePair<string, string>("26", "27"));
                    startday.Add(new KeyValuePair<string, string>("27", "28"));
                    startday.Add(new KeyValuePair<string, string>("28", "29"));
                    startday.Add(new KeyValuePair<string, string>("29", "30"));
                    break;
                case "4":
                    startday.Add(new KeyValuePair<string, string>("0", "1"));
                    startday.Add(new KeyValuePair<string, string>("1", "2"));
                    startday.Add(new KeyValuePair<string, string>("2", "3"));
                    startday.Add(new KeyValuePair<string, string>("3", "4"));
                    startday.Add(new KeyValuePair<string, string>("4", "5"));
                    startday.Add(new KeyValuePair<string, string>("5", "6"));
                    startday.Add(new KeyValuePair<string, string>("6", "7"));
                    startday.Add(new KeyValuePair<string, string>("7", "8"));
                    startday.Add(new KeyValuePair<string, string>("8", "9"));
                    startday.Add(new KeyValuePair<string, string>("9", "10"));
                    startday.Add(new KeyValuePair<string, string>("10", "11"));
                    startday.Add(new KeyValuePair<string, string>("11", "12"));
                    startday.Add(new KeyValuePair<string, string>("12", "13"));
                    startday.Add(new KeyValuePair<string, string>("13", "14"));
                    startday.Add(new KeyValuePair<string, string>("14", "15"));
                    startday.Add(new KeyValuePair<string, string>("15", "16"));
                    startday.Add(new KeyValuePair<string, string>("16", "17"));
                    startday.Add(new KeyValuePair<string, string>("17", "18"));
                    startday.Add(new KeyValuePair<string, string>("18", "19"));
                    startday.Add(new KeyValuePair<string, string>("19", "20"));
                    startday.Add(new KeyValuePair<string, string>("20", "21"));
                    startday.Add(new KeyValuePair<string, string>("21", "22"));
                    startday.Add(new KeyValuePair<string, string>("22", "23"));
                    startday.Add(new KeyValuePair<string, string>("23", "24"));
                    startday.Add(new KeyValuePair<string, string>("24", "25"));
                    startday.Add(new KeyValuePair<string, string>("25", "26"));
                    startday.Add(new KeyValuePair<string, string>("26", "27"));
                    startday.Add(new KeyValuePair<string, string>("27", "28"));
                    startday.Add(new KeyValuePair<string, string>("28", "29"));
                    startday.Add(new KeyValuePair<string, string>("29", "30"));
                    startday.Add(new KeyValuePair<string, string>("30", "31"));
                    break;
                case "5":
                    startday.Add(new KeyValuePair<string, string>("0", "1"));
                    startday.Add(new KeyValuePair<string, string>("1", "2"));
                    startday.Add(new KeyValuePair<string, string>("2", "3"));
                    startday.Add(new KeyValuePair<string, string>("3", "4"));
                    startday.Add(new KeyValuePair<string, string>("4", "5"));
                    startday.Add(new KeyValuePair<string, string>("5", "6"));
                    startday.Add(new KeyValuePair<string, string>("6", "7"));
                    startday.Add(new KeyValuePair<string, string>("7", "8"));
                    startday.Add(new KeyValuePair<string, string>("8", "9"));
                    startday.Add(new KeyValuePair<string, string>("9", "10"));
                    startday.Add(new KeyValuePair<string, string>("10", "11"));
                    startday.Add(new KeyValuePair<string, string>("11", "12"));
                    startday.Add(new KeyValuePair<string, string>("12", "13"));
                    startday.Add(new KeyValuePair<string, string>("13", "14"));
                    startday.Add(new KeyValuePair<string, string>("14", "15"));
                    startday.Add(new KeyValuePair<string, string>("15", "16"));
                    startday.Add(new KeyValuePair<string, string>("16", "17"));
                    startday.Add(new KeyValuePair<string, string>("17", "18"));
                    startday.Add(new KeyValuePair<string, string>("18", "19"));
                    startday.Add(new KeyValuePair<string, string>("19", "20"));
                    startday.Add(new KeyValuePair<string, string>("20", "21"));
                    startday.Add(new KeyValuePair<string, string>("21", "22"));
                    startday.Add(new KeyValuePair<string, string>("22", "23"));
                    startday.Add(new KeyValuePair<string, string>("23", "24"));
                    startday.Add(new KeyValuePair<string, string>("24", "25"));
                    startday.Add(new KeyValuePair<string, string>("25", "26"));
                    startday.Add(new KeyValuePair<string, string>("26", "27"));
                    startday.Add(new KeyValuePair<string, string>("27", "28"));
                    startday.Add(new KeyValuePair<string, string>("28", "29"));
                    startday.Add(new KeyValuePair<string, string>("29", "30"));
                    break;
                case "6":
                    startday.Add(new KeyValuePair<string, string>("0", "1"));
                    startday.Add(new KeyValuePair<string, string>("1", "2"));
                    startday.Add(new KeyValuePair<string, string>("2", "3"));
                    startday.Add(new KeyValuePair<string, string>("3", "4"));
                    startday.Add(new KeyValuePair<string, string>("4", "5"));
                    startday.Add(new KeyValuePair<string, string>("5", "6"));
                    startday.Add(new KeyValuePair<string, string>("6", "7"));
                    startday.Add(new KeyValuePair<string, string>("7", "8"));
                    startday.Add(new KeyValuePair<string, string>("8", "9"));
                    startday.Add(new KeyValuePair<string, string>("9", "10"));
                    startday.Add(new KeyValuePair<string, string>("10", "11"));
                    startday.Add(new KeyValuePair<string, string>("11", "12"));
                    startday.Add(new KeyValuePair<string, string>("12", "13"));
                    startday.Add(new KeyValuePair<string, string>("13", "14"));
                    startday.Add(new KeyValuePair<string, string>("14", "15"));
                    startday.Add(new KeyValuePair<string, string>("15", "16"));
                    startday.Add(new KeyValuePair<string, string>("16", "17"));
                    startday.Add(new KeyValuePair<string, string>("17", "18"));
                    startday.Add(new KeyValuePair<string, string>("18", "19"));
                    startday.Add(new KeyValuePair<string, string>("19", "20"));
                    startday.Add(new KeyValuePair<string, string>("20", "21"));
                    startday.Add(new KeyValuePair<string, string>("21", "22"));
                    startday.Add(new KeyValuePair<string, string>("22", "23"));
                    startday.Add(new KeyValuePair<string, string>("23", "24"));
                    startday.Add(new KeyValuePair<string, string>("24", "25"));
                    startday.Add(new KeyValuePair<string, string>("25", "26"));
                    startday.Add(new KeyValuePair<string, string>("26", "27"));
                    startday.Add(new KeyValuePair<string, string>("27", "28"));
                    startday.Add(new KeyValuePair<string, string>("28", "29"));
                    startday.Add(new KeyValuePair<string, string>("29", "30"));
                    startday.Add(new KeyValuePair<string, string>("30", "31"));
                    break;
                case "7":
                    startday.Add(new KeyValuePair<string, string>("0", "1"));
                    startday.Add(new KeyValuePair<string, string>("1", "2"));
                    startday.Add(new KeyValuePair<string, string>("2", "3"));
                    startday.Add(new KeyValuePair<string, string>("3", "4"));
                    startday.Add(new KeyValuePair<string, string>("4", "5"));
                    startday.Add(new KeyValuePair<string, string>("5", "6"));
                    startday.Add(new KeyValuePair<string, string>("6", "7"));
                    startday.Add(new KeyValuePair<string, string>("7", "8"));
                    startday.Add(new KeyValuePair<string, string>("8", "9"));
                    startday.Add(new KeyValuePair<string, string>("9", "10"));
                    startday.Add(new KeyValuePair<string, string>("10", "11"));
                    startday.Add(new KeyValuePair<string, string>("11", "12"));
                    startday.Add(new KeyValuePair<string, string>("12", "13"));
                    startday.Add(new KeyValuePair<string, string>("13", "14"));
                    startday.Add(new KeyValuePair<string, string>("14", "15"));
                    startday.Add(new KeyValuePair<string, string>("15", "16"));
                    startday.Add(new KeyValuePair<string, string>("16", "17"));
                    startday.Add(new KeyValuePair<string, string>("17", "18"));
                    startday.Add(new KeyValuePair<string, string>("18", "19"));
                    startday.Add(new KeyValuePair<string, string>("19", "20"));
                    startday.Add(new KeyValuePair<string, string>("20", "21"));
                    startday.Add(new KeyValuePair<string, string>("21", "22"));
                    startday.Add(new KeyValuePair<string, string>("22", "23"));
                    startday.Add(new KeyValuePair<string, string>("23", "24"));
                    startday.Add(new KeyValuePair<string, string>("24", "25"));
                    startday.Add(new KeyValuePair<string, string>("25", "26"));
                    startday.Add(new KeyValuePair<string, string>("26", "27"));
                    startday.Add(new KeyValuePair<string, string>("27", "28"));
                    startday.Add(new KeyValuePair<string, string>("28", "29"));
                    startday.Add(new KeyValuePair<string, string>("29", "30"));
                    startday.Add(new KeyValuePair<string, string>("30", "31"));
                    break;
                case "8":
                    startday.Add(new KeyValuePair<string, string>("0", "1"));
                    startday.Add(new KeyValuePair<string, string>("1", "2"));
                    startday.Add(new KeyValuePair<string, string>("2", "3"));
                    startday.Add(new KeyValuePair<string, string>("3", "4"));
                    startday.Add(new KeyValuePair<string, string>("4", "5"));
                    startday.Add(new KeyValuePair<string, string>("5", "6"));
                    startday.Add(new KeyValuePair<string, string>("6", "7"));
                    startday.Add(new KeyValuePair<string, string>("7", "8"));
                    startday.Add(new KeyValuePair<string, string>("8", "9"));
                    startday.Add(new KeyValuePair<string, string>("9", "10"));
                    startday.Add(new KeyValuePair<string, string>("10", "11"));
                    startday.Add(new KeyValuePair<string, string>("11", "12"));
                    startday.Add(new KeyValuePair<string, string>("12", "13"));
                    startday.Add(new KeyValuePair<string, string>("13", "14"));
                    startday.Add(new KeyValuePair<string, string>("14", "15"));
                    startday.Add(new KeyValuePair<string, string>("15", "16"));
                    startday.Add(new KeyValuePair<string, string>("16", "17"));
                    startday.Add(new KeyValuePair<string, string>("17", "18"));
                    startday.Add(new KeyValuePair<string, string>("18", "19"));
                    startday.Add(new KeyValuePair<string, string>("19", "20"));
                    startday.Add(new KeyValuePair<string, string>("20", "21"));
                    startday.Add(new KeyValuePair<string, string>("21", "22"));
                    startday.Add(new KeyValuePair<string, string>("22", "23"));
                    startday.Add(new KeyValuePair<string, string>("23", "24"));
                    startday.Add(new KeyValuePair<string, string>("24", "25"));
                    startday.Add(new KeyValuePair<string, string>("25", "26"));
                    startday.Add(new KeyValuePair<string, string>("26", "27"));
                    startday.Add(new KeyValuePair<string, string>("27", "28"));
                    startday.Add(new KeyValuePair<string, string>("28", "29"));
                    startday.Add(new KeyValuePair<string, string>("29", "30"));
                    break;
                case "9":
                    startday.Add(new KeyValuePair<string, string>("0", "1"));
                    startday.Add(new KeyValuePair<string, string>("1", "2"));
                    startday.Add(new KeyValuePair<string, string>("2", "3"));
                    startday.Add(new KeyValuePair<string, string>("3", "4"));
                    startday.Add(new KeyValuePair<string, string>("4", "5"));
                    startday.Add(new KeyValuePair<string, string>("5", "6"));
                    startday.Add(new KeyValuePair<string, string>("6", "7"));
                    startday.Add(new KeyValuePair<string, string>("7", "8"));
                    startday.Add(new KeyValuePair<string, string>("8", "9"));
                    startday.Add(new KeyValuePair<string, string>("9", "10"));
                    startday.Add(new KeyValuePair<string, string>("10", "11"));
                    startday.Add(new KeyValuePair<string, string>("11", "12"));
                    startday.Add(new KeyValuePair<string, string>("12", "13"));
                    startday.Add(new KeyValuePair<string, string>("13", "14"));
                    startday.Add(new KeyValuePair<string, string>("14", "15"));
                    startday.Add(new KeyValuePair<string, string>("15", "16"));
                    startday.Add(new KeyValuePair<string, string>("16", "17"));
                    startday.Add(new KeyValuePair<string, string>("17", "18"));
                    startday.Add(new KeyValuePair<string, string>("18", "19"));
                    startday.Add(new KeyValuePair<string, string>("19", "20"));
                    startday.Add(new KeyValuePair<string, string>("20", "21"));
                    startday.Add(new KeyValuePair<string, string>("21", "22"));
                    startday.Add(new KeyValuePair<string, string>("22", "23"));
                    startday.Add(new KeyValuePair<string, string>("23", "24"));
                    startday.Add(new KeyValuePair<string, string>("24", "25"));
                    startday.Add(new KeyValuePair<string, string>("25", "26"));
                    startday.Add(new KeyValuePair<string, string>("26", "27"));
                    startday.Add(new KeyValuePair<string, string>("27", "28"));
                    startday.Add(new KeyValuePair<string, string>("28", "29"));
                    startday.Add(new KeyValuePair<string, string>("29", "30"));
                    startday.Add(new KeyValuePair<string, string>("30", "31"));
                    break;
                case "10":
                    startday.Add(new KeyValuePair<string, string>("0", "1"));
                    startday.Add(new KeyValuePair<string, string>("1", "2"));
                    startday.Add(new KeyValuePair<string, string>("2", "3"));
                    startday.Add(new KeyValuePair<string, string>("3", "4"));
                    startday.Add(new KeyValuePair<string, string>("4", "5"));
                    startday.Add(new KeyValuePair<string, string>("5", "6"));
                    startday.Add(new KeyValuePair<string, string>("6", "7"));
                    startday.Add(new KeyValuePair<string, string>("7", "8"));
                    startday.Add(new KeyValuePair<string, string>("8", "9"));
                    startday.Add(new KeyValuePair<string, string>("9", "10"));
                    startday.Add(new KeyValuePair<string, string>("10", "11"));
                    startday.Add(new KeyValuePair<string, string>("11", "12"));
                    startday.Add(new KeyValuePair<string, string>("12", "13"));
                    startday.Add(new KeyValuePair<string, string>("13", "14"));
                    startday.Add(new KeyValuePair<string, string>("14", "15"));
                    startday.Add(new KeyValuePair<string, string>("15", "16"));
                    startday.Add(new KeyValuePair<string, string>("16", "17"));
                    startday.Add(new KeyValuePair<string, string>("17", "18"));
                    startday.Add(new KeyValuePair<string, string>("18", "19"));
                    startday.Add(new KeyValuePair<string, string>("19", "20"));
                    startday.Add(new KeyValuePair<string, string>("20", "21"));
                    startday.Add(new KeyValuePair<string, string>("21", "22"));
                    startday.Add(new KeyValuePair<string, string>("22", "23"));
                    startday.Add(new KeyValuePair<string, string>("23", "24"));
                    startday.Add(new KeyValuePair<string, string>("24", "25"));
                    startday.Add(new KeyValuePair<string, string>("25", "26"));
                    startday.Add(new KeyValuePair<string, string>("26", "27"));
                    startday.Add(new KeyValuePair<string, string>("27", "28"));
                    startday.Add(new KeyValuePair<string, string>("28", "29"));
                    startday.Add(new KeyValuePair<string, string>("29", "30"));
                    break;
                case "11":
                    startday.Add(new KeyValuePair<string, string>("0", "1"));
                    startday.Add(new KeyValuePair<string, string>("1", "2"));
                    startday.Add(new KeyValuePair<string, string>("2", "3"));
                    startday.Add(new KeyValuePair<string, string>("3", "4"));
                    startday.Add(new KeyValuePair<string, string>("4", "5"));
                    startday.Add(new KeyValuePair<string, string>("5", "6"));
                    startday.Add(new KeyValuePair<string, string>("6", "7"));
                    startday.Add(new KeyValuePair<string, string>("7", "8"));
                    startday.Add(new KeyValuePair<string, string>("8", "9"));
                    startday.Add(new KeyValuePair<string, string>("9", "10"));
                    startday.Add(new KeyValuePair<string, string>("10", "11"));
                    startday.Add(new KeyValuePair<string, string>("11", "12"));
                    startday.Add(new KeyValuePair<string, string>("12", "13"));
                    startday.Add(new KeyValuePair<string, string>("13", "14"));
                    startday.Add(new KeyValuePair<string, string>("14", "15"));
                    startday.Add(new KeyValuePair<string, string>("15", "16"));
                    startday.Add(new KeyValuePair<string, string>("16", "17"));
                    startday.Add(new KeyValuePair<string, string>("17", "18"));
                    startday.Add(new KeyValuePair<string, string>("18", "19"));
                    startday.Add(new KeyValuePair<string, string>("19", "20"));
                    startday.Add(new KeyValuePair<string, string>("20", "21"));
                    startday.Add(new KeyValuePair<string, string>("21", "22"));
                    startday.Add(new KeyValuePair<string, string>("22", "23"));
                    startday.Add(new KeyValuePair<string, string>("23", "24"));
                    startday.Add(new KeyValuePair<string, string>("24", "25"));
                    startday.Add(new KeyValuePair<string, string>("25", "26"));
                    startday.Add(new KeyValuePair<string, string>("26", "27"));
                    startday.Add(new KeyValuePair<string, string>("27", "28"));
                    startday.Add(new KeyValuePair<string, string>("28", "29"));
                    startday.Add(new KeyValuePair<string, string>("29", "30"));
                    startday.Add(new KeyValuePair<string, string>("30", "31"));
                    break;
            }
            cboStartDay.DataSource = startday;
            cboStartDay.ValueMember = "Key";
            cboStartDay.DisplayMember = "Value";

            //SET CBOSTARTMONTH VALUE BASED ON XML
            cboStartDay.SelectedValue = calendar.Attribute("startDay").Value.ToString();

            //SET TXTSTARTYEAR VALUE BASED ON XML
            //txtStartYear.Text = (Int32.Parse(calendar.Attribute("startYear").Value)+1).ToString();
            txtStartYear.Text = (Convert.ToInt32(calendar.Attribute("startYear").Value)+1).ToString();

            //SET TXTLASTTURN VALUE BASED ON XML
            //txtLastTurn.Text = (Int32.Parse(calendar.Attribute("finalTurn").Value) + 1).ToString();
            txtLastTurn.Text = (Convert.ToInt32(calendar.Attribute("finalTurn").Value) + 1).ToString();

            //SET TXTCURRENTTURN VALUE BASED ON XML
            //txtCurrentTurn.Text = (Int32.Parse(calendar.Attribute("currentTurn").Value) + 1).ToString();
            txtCurrentTurn.Text = (Convert.ToInt32(calendar.Attribute("currentTurn").Value) + 1).ToString();
            //Int32.Parse(fu.Attribute("ID").Value) + 1

            ////SET TXTLASTTURN VALUE BASED ON XML
            //txtLastTurn.Text = (Int32.Parse(calendar.Attribute("finalTurn").Value)+1).ToString();
            txtLastTurn.Text = (Convert.ToInt32(calendar.Attribute("finalTurn").Value)+1).ToString();

            //POPULATES MAP SCALE COMBOBOX
            var mapscale = new BindingList<KeyValuePair<string, string>>();
            mapscale.Add(new KeyValuePair<string, string>("7", "0.25"));
            mapscale.Add(new KeyValuePair<string, string>("8", "0.5"));
            mapscale.Add(new KeyValuePair<string, string>("9", "1"));
            mapscale.Add(new KeyValuePair<string, string>("0", "2.5"));
            mapscale.Add(new KeyValuePair<string, string>("1", "5"));
            mapscale.Add(new KeyValuePair<string, string>("2", "10"));
            mapscale.Add(new KeyValuePair<string, string>("3", "15"));
            mapscale.Add(new KeyValuePair<string, string>("4", "20"));
            mapscale.Add(new KeyValuePair<string, string>("5", "25"));
            mapscale.Add(new KeyValuePair<string, string>("6", "50"));
            mapscale.Add(new KeyValuePair<string, string>("10", "100"));
            mapscale.Add(new KeyValuePair<string, string>("11", "200"));

            cboMapScale.DataSource = mapscale;
            cboMapScale.ValueMember = "Key";
            cboMapScale.DisplayMember = "Value";

            //SET CBOMAPSCALE VALUE BASED ON XML
            cboMapScale.SelectedValue = environment.Attribute("scale").Value.ToString();

            //POPULATES CLIMATE AREA COMBOBOX
            var climatearea = new BindingList<KeyValuePair<string, string>>();
            climatearea.Add(new KeyValuePair<string, string>("0", "Northern"));
            climatearea.Add(new KeyValuePair<string, string>("2", "Equatorial"));
            climatearea.Add(new KeyValuePair<string, string>("1", "Southern"));

            cboClimateArea.DataSource = climatearea;
            cboClimateArea.ValueMember = "Key";
            cboClimateArea.DisplayMember = "Value";

            //SET CBOCLIMATEAREA BASED ON XML
            cboClimateArea.SelectedValue = environment.Attribute("zone").Value.ToString();

            //POPULATES PRECIPITATION COMBOBOX
            var precipitation = new BindingList<KeyValuePair<string, string>>();
            precipitation.Add(new KeyValuePair<string, string>("4", "None"));
            precipitation.Add(new KeyValuePair<string, string>("3", "Occasional"));
            precipitation.Add(new KeyValuePair<string, string>("2", "Light"));
            precipitation.Add(new KeyValuePair<string, string>("1", "Moderate"));
            precipitation.Add(new KeyValuePair<string, string>("0", "Heavy"));

            cboPrecipitation.DataSource = precipitation;
            cboPrecipitation.ValueMember = "Key";
            cboPrecipitation.DisplayMember = "Value";

            //SET CBOPRECIPITATION BASED ON XML
            cboPrecipitation.SelectedValue = environment.Attribute("precipitation").Value.ToString();

            //POPULATES TEMPERATURE COMBOBOX
            var temperature = new BindingList<KeyValuePair<string, string>>();
            temperature.Add(new KeyValuePair<string, string>("7", "Hot"));
            temperature.Add(new KeyValuePair<string, string>("6", "Warm"));
            temperature.Add(new KeyValuePair<string, string>("5", "Temperate"));
            temperature.Add(new KeyValuePair<string, string>("4", "Cool"));
            temperature.Add(new KeyValuePair<string, string>("3", "Cold"));
            temperature.Add(new KeyValuePair<string, string>("2", "Frozen1"));
            temperature.Add(new KeyValuePair<string, string>("1", "Frozen2"));
            temperature.Add(new KeyValuePair<string, string>("0", "Frozen3"));

            cboTemperature.DataSource = temperature;
            cboTemperature.ValueMember = "Key";
            cboTemperature.DisplayMember = "Value";

            //SET CBOTEMPERATURE BASED ON XML
            cboTemperature.SelectedValue = environment.Attribute("temperature").Value.ToString();

            //WEATHERZONE DATA
            string strWZ2Border = environment.Attribute("boundary1").Value.ToString();
            string strWZ3Border = environment.Attribute("boundary2").Value.ToString();

            //WZ1 VISIBILITY
            var wz1vis = new BindingList<KeyValuePair<string, string>>();
            wz1vis.Add(new KeyValuePair<string, string>("0", "Fair"));
            wz1vis.Add(new KeyValuePair<string, string>("1", "Hazy"));
            wz1vis.Add(new KeyValuePair<string, string>("2", "Overcast"));

            cboWZ1Vis.DataSource = wz1vis;
            cboWZ1Vis.ValueMember = "Key";
            cboWZ1Vis.DisplayMember = "Value";

            cboWZ1Vis.SelectedValue = weatherzone1.Attribute("visibility").Value.ToString();

            //POPULATES WZ2 COMBOBOX
            if (environment.Attribute("boundary1").Value == "999")
            {
                //gbWZ.Visible = false;
                gbWZ2.Visible = false;
                gbWZ3.Visible = false;
                this.Size = new Size(435, 460);
                btnCloseEnviron.Top = 375;
                btnSaveEnviron.Top = 375;
            }
            else
            {
                gbWZ2.Visible = true;

                //POPULATES WZ2 COMBOBOXES
                //WZ2PRECIPITATION
                var wz2precip = new BindingList<KeyValuePair<string, string>>();
                wz2precip.Add(new KeyValuePair<string, string>("4", "None"));
                wz2precip.Add(new KeyValuePair<string, string>("3", "Occasional"));
                wz2precip.Add(new KeyValuePair<string, string>("2", "Light"));
                wz2precip.Add(new KeyValuePair<string, string>("1", "Moderate"));
                wz2precip.Add(new KeyValuePair<string, string>("0", "Heavy"));

                cboWZ2Precip.DataSource = wz2precip;
                cboWZ2Precip.ValueMember = "Key";
                cboWZ2Precip.DisplayMember = "Value";

                cboWZ2Precip.SelectedValue = weatherzone2.Attribute("precipitation").Value.ToString();

                //WZ2 TEMPERATURE
                var wz2temp = new BindingList<KeyValuePair<string, string>>();
                wz2temp.Add(new KeyValuePair<string, string>("7", "Hot"));
                wz2temp.Add(new KeyValuePair<string, string>("6", "Warm"));
                wz2temp.Add(new KeyValuePair<string, string>("5", "Temperate"));
                wz2temp.Add(new KeyValuePair<string, string>("4", "Cool"));
                wz2temp.Add(new KeyValuePair<string, string>("3", "Cold"));
                wz2temp.Add(new KeyValuePair<string, string>("2", "Frozen1"));
                wz2temp.Add(new KeyValuePair<string, string>("1", "Frozen2"));
                wz2temp.Add(new KeyValuePair<string, string>("0", "Frozen3"));

                cboWZ2Temp.DataSource = wz2temp;
                cboWZ2Temp.ValueMember = "Key";
                cboWZ2Temp.DisplayMember = "Value";

                cboWZ2Temp.SelectedValue = weatherzone2.Attribute("temperature").Value.ToString();

                //WZ2 VISIBILITY
                var wz2vis = new BindingList<KeyValuePair<string, string>>();
                wz2vis.Add(new KeyValuePair<string, string>("0", "Fair"));
                wz2vis.Add(new KeyValuePair<string, string>("1", "Hazy"));
                wz2vis.Add(new KeyValuePair<string, string>("2", "Overcast"));

                cboWZ2Vis.DataSource = wz2vis;
                cboWZ2Vis.ValueMember = "Key";
                cboWZ2Vis.DisplayMember = "Value";

                cboWZ2Vis.SelectedValue = weatherzone2.Attribute("visibility").Value.ToString();

                //WZ2 TEXTBOXES
                txtWZ2Border.Text = environment.Attribute("boundary1").Value.ToString();

                //POPULATES WZ3 COMBOBOXES
                if (environment.Attribute("boundary2").Value == "999")
                {
                    gbWZ3.Visible = false;
                    this.Size = new Size(435, 460);
                    btnCloseEnviron.Top = 375;
                    btnSaveEnviron.Top = 375;
                }
                else
                {
                    gbWZ3.Visible = true;
                    this.Size = new Size(435, 600);
                    btnCloseEnviron.Top = 515;
                    btnSaveEnviron.Top = 515;

                    //POPULATES WZ3 COMBOBOXES
                    //WZ3PRECIPITATION
                    var wz3precip = new BindingList<KeyValuePair<string, string>>();
                    wz3precip.Add(new KeyValuePair<string, string>("4", "None"));
                    wz3precip.Add(new KeyValuePair<string, string>("3", "Occasional"));
                    wz3precip.Add(new KeyValuePair<string, string>("2", "Light"));
                    wz3precip.Add(new KeyValuePair<string, string>("1", "Moderate"));
                    wz3precip.Add(new KeyValuePair<string, string>("0", "Heavy"));

                    cboWZ3Precip.DataSource = wz3precip;
                    cboWZ3Precip.ValueMember = "Key";
                    cboWZ3Precip.DisplayMember = "Value";

                    cboWZ3Precip.SelectedValue = weatherzone3.Attribute("precipitation").Value.ToString();

                    //WZ3 TEMPERATURE
                    var wz3temp = new BindingList<KeyValuePair<string, string>>();
                    wz3temp.Add(new KeyValuePair<string, string>("7", "Hot"));
                    wz3temp.Add(new KeyValuePair<string, string>("6", "Warm"));
                    wz3temp.Add(new KeyValuePair<string, string>("5", "Temperate"));
                    wz3temp.Add(new KeyValuePair<string, string>("4", "Cool"));
                    wz3temp.Add(new KeyValuePair<string, string>("3", "Cold"));
                    wz3temp.Add(new KeyValuePair<string, string>("2", "Frozen1"));
                    wz3temp.Add(new KeyValuePair<string, string>("1", "Frozen2"));
                    wz3temp.Add(new KeyValuePair<string, string>("0", "Frozen3"));

                    cboWZ3Temp.DataSource = wz3temp;
                    cboWZ3Temp.ValueMember = "Key";
                    cboWZ3Temp.DisplayMember = "Value";

                    cboWZ3Temp.SelectedValue = weatherzone3.Attribute("temperature").Value.ToString();

                    //WZ3 VISIBILITY
                    var wz3vis = new BindingList<KeyValuePair<string, string>>();
                    wz3vis.Add(new KeyValuePair<string, string>("0", "Fair"));
                    wz3vis.Add(new KeyValuePair<string, string>("1", "Hazy"));
                    wz3vis.Add(new KeyValuePair<string, string>("2", "Overcast"));

                    cboWZ3Vis.DataSource = wz3vis;
                    cboWZ3Vis.ValueMember = "Key";
                    cboWZ3Vis.DisplayMember = "Value";

                    cboWZ3Vis.SelectedValue = weatherzone3.Attribute("visibility").Value.ToString();

                    //WZ3 TEXTBOXES
                    txtWZ3Border.Text = environment.Attribute("boundary2").Value.ToString();
                }
            }

        }

        private void cboTurnLength_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string xpath = "CALENDAR";
            XElement xelem = XElement.Load(Globals.GlobalVariables.PATH);
            var calendar = xelem.XPathSelectElement(xpath);

            //POPULATES START HOUR COMBOBOX
            var starthour = new BindingList<KeyValuePair<string, string>>();
            string strturnlength = cboTurnLength.SelectedValue.ToString();

            switch (strturnlength)
            {
                case "9":
                    starthour = new BindingList<KeyValuePair<string, string>>();
                    starthour.Add(new KeyValuePair<string, string>("23", "Midnight"));
                    starthour.Add(new KeyValuePair<string, string>("1", "01:00"));
                    starthour.Add(new KeyValuePair<string, string>("2", "02:00"));
                    starthour.Add(new KeyValuePair<string, string>("3", "03:00"));
                    starthour.Add(new KeyValuePair<string, string>("4", "04:00"));
                    starthour.Add(new KeyValuePair<string, string>("5", "05:00"));
                    starthour.Add(new KeyValuePair<string, string>("6", "06:00"));
                    starthour.Add(new KeyValuePair<string, string>("7", "07:00"));
                    starthour.Add(new KeyValuePair<string, string>("8", "08:00"));
                    starthour.Add(new KeyValuePair<string, string>("9", "09:00"));
                    starthour.Add(new KeyValuePair<string, string>("10", "10:00"));
                    starthour.Add(new KeyValuePair<string, string>("11", "11:00"));
                    starthour.Add(new KeyValuePair<string, string>("12", "12:00"));
                    starthour.Add(new KeyValuePair<string, string>("13", "13:00"));
                    starthour.Add(new KeyValuePair<string, string>("14", "14:00"));
                    starthour.Add(new KeyValuePair<string, string>("15", "15:00"));
                    starthour.Add(new KeyValuePair<string, string>("16", "16:00"));
                    starthour.Add(new KeyValuePair<string, string>("17", "17:00"));
                    starthour.Add(new KeyValuePair<string, string>("18", "18:00"));
                    starthour.Add(new KeyValuePair<string, string>("19", "19:00"));
                    starthour.Add(new KeyValuePair<string, string>("20", "20:00"));
                    starthour.Add(new KeyValuePair<string, string>("21", "21:00"));
                    starthour.Add(new KeyValuePair<string, string>("22", "23:00"));
                    break;

                case "10":
                    starthour = new BindingList<KeyValuePair<string, string>>();
                    starthour.Add(new KeyValuePair<string, string>("0", "Midnight"));
                    starthour.Add(new KeyValuePair<string, string>("3", "03:00"));
                    starthour.Add(new KeyValuePair<string, string>("6", "06:00"));
                    starthour.Add(new KeyValuePair<string, string>("9", "09:00"));
                    starthour.Add(new KeyValuePair<string, string>("12", "12:00"));
                    starthour.Add(new KeyValuePair<string, string>("15", "15:00"));
                    starthour.Add(new KeyValuePair<string, string>("18", "18:00"));
                    starthour.Add(new KeyValuePair<string, string>("21", "21:00"));
                    break;

                case "0":
                    starthour = new BindingList<KeyValuePair<string, string>>();
                    starthour.Add(new KeyValuePair<string, string>("0", "Pre-Dawn"));
                    starthour.Add(new KeyValuePair<string, string>("6", "Morning"));
                    starthour.Add(new KeyValuePair<string, string>("9", "Afternoon"));
                    starthour.Add(new KeyValuePair<string, string>("18", "Night"));
                    break;

                case "1":
                    starthour = new BindingList<KeyValuePair<string, string>>();
                    starthour.Add(new KeyValuePair<string, string>("12", "AM"));
                    starthour.Add(new KeyValuePair<string, string>("0", "PM"));
                    break;

                default:
                    starthour = new BindingList<KeyValuePair<string, string>>();
                    starthour.Add(new KeyValuePair<string, string>("0", "--"));
                    break;
            }

            cboStartHour.DataSource = starthour;
            cboStartHour.ValueMember = "Key";
            cboStartHour.DisplayMember = "Value";

            calendar.Attribute("turnLength").Value = cboTurnLength.SelectedValue.ToString();
            calendar.Attribute("startHour").Value = cboStartHour.SelectedValue.ToString();
            xelem.Save(Globals.GlobalVariables.PATH);
        }

        private void cboStartHour_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string xpath = "CALENDAR";
            XElement xelem = XElement.Load(Globals.GlobalVariables.PATH);
            var calendar = xelem.XPathSelectElement(xpath);
            calendar.Attribute("startHour").Value = cboStartHour.SelectedValue.ToString();
            xelem.Save(Globals.GlobalVariables.PATH);
        }

        private void cboStartMonth_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string xpath = "CALENDAR";
            XElement xelem = XElement.Load(Globals.GlobalVariables.PATH);
            var calendar = xelem.XPathSelectElement(xpath);

            string strStartmonth = cboStartMonth.SelectedValue.ToString();
            var startday = new BindingList<KeyValuePair<string, string>>();
            switch (strStartmonth)
            {
                case "0":
                    startday.Add(new KeyValuePair<string, string>("0", "1"));
                    startday.Add(new KeyValuePair<string, string>("1", "2"));
                    startday.Add(new KeyValuePair<string, string>("2", "3"));
                    startday.Add(new KeyValuePair<string, string>("3", "4"));
                    startday.Add(new KeyValuePair<string, string>("4", "5"));
                    startday.Add(new KeyValuePair<string, string>("5", "6"));
                    startday.Add(new KeyValuePair<string, string>("6", "7"));
                    startday.Add(new KeyValuePair<string, string>("7", "8"));
                    startday.Add(new KeyValuePair<string, string>("8", "9"));
                    startday.Add(new KeyValuePair<string, string>("9", "10"));
                    startday.Add(new KeyValuePair<string, string>("10", "11"));
                    startday.Add(new KeyValuePair<string, string>("11", "12"));
                    startday.Add(new KeyValuePair<string, string>("12", "13"));
                    startday.Add(new KeyValuePair<string, string>("13", "14"));
                    startday.Add(new KeyValuePair<string, string>("14", "15"));
                    startday.Add(new KeyValuePair<string, string>("15", "16"));
                    startday.Add(new KeyValuePair<string, string>("16", "17"));
                    startday.Add(new KeyValuePair<string, string>("17", "18"));
                    startday.Add(new KeyValuePair<string, string>("18", "19"));
                    startday.Add(new KeyValuePair<string, string>("19", "20"));
                    startday.Add(new KeyValuePair<string, string>("20", "21"));
                    startday.Add(new KeyValuePair<string, string>("21", "22"));
                    startday.Add(new KeyValuePair<string, string>("22", "23"));
                    startday.Add(new KeyValuePair<string, string>("23", "24"));
                    startday.Add(new KeyValuePair<string, string>("24", "25"));
                    startday.Add(new KeyValuePair<string, string>("25", "26"));
                    startday.Add(new KeyValuePair<string, string>("26", "27"));
                    startday.Add(new KeyValuePair<string, string>("27", "28"));
                    startday.Add(new KeyValuePair<string, string>("28", "29"));
                    startday.Add(new KeyValuePair<string, string>("29", "30"));
                    startday.Add(new KeyValuePair<string, string>("30", "31"));
                    break;
                case "1":
                    startday.Add(new KeyValuePair<string, string>("0", "1"));
                    startday.Add(new KeyValuePair<string, string>("1", "2"));
                    startday.Add(new KeyValuePair<string, string>("2", "3"));
                    startday.Add(new KeyValuePair<string, string>("3", "4"));
                    startday.Add(new KeyValuePair<string, string>("4", "5"));
                    startday.Add(new KeyValuePair<string, string>("5", "6"));
                    startday.Add(new KeyValuePair<string, string>("6", "7"));
                    startday.Add(new KeyValuePair<string, string>("7", "8"));
                    startday.Add(new KeyValuePair<string, string>("8", "9"));
                    startday.Add(new KeyValuePair<string, string>("9", "10"));
                    startday.Add(new KeyValuePair<string, string>("10", "11"));
                    startday.Add(new KeyValuePair<string, string>("11", "12"));
                    startday.Add(new KeyValuePair<string, string>("12", "13"));
                    startday.Add(new KeyValuePair<string, string>("13", "14"));
                    startday.Add(new KeyValuePair<string, string>("14", "15"));
                    startday.Add(new KeyValuePair<string, string>("15", "16"));
                    startday.Add(new KeyValuePair<string, string>("16", "17"));
                    startday.Add(new KeyValuePair<string, string>("17", "18"));
                    startday.Add(new KeyValuePair<string, string>("18", "19"));
                    startday.Add(new KeyValuePair<string, string>("19", "20"));
                    startday.Add(new KeyValuePair<string, string>("20", "21"));
                    startday.Add(new KeyValuePair<string, string>("21", "22"));
                    startday.Add(new KeyValuePair<string, string>("22", "23"));
                    startday.Add(new KeyValuePair<string, string>("23", "24"));
                    startday.Add(new KeyValuePair<string, string>("24", "25"));
                    startday.Add(new KeyValuePair<string, string>("25", "26"));
                    startday.Add(new KeyValuePair<string, string>("26", "27"));
                    startday.Add(new KeyValuePair<string, string>("27", "28"));
                    break;
                case "2":
                    startday.Add(new KeyValuePair<string, string>("0", "1"));
                    startday.Add(new KeyValuePair<string, string>("1", "2"));
                    startday.Add(new KeyValuePair<string, string>("2", "3"));
                    startday.Add(new KeyValuePair<string, string>("3", "4"));
                    startday.Add(new KeyValuePair<string, string>("4", "5"));
                    startday.Add(new KeyValuePair<string, string>("5", "6"));
                    startday.Add(new KeyValuePair<string, string>("6", "7"));
                    startday.Add(new KeyValuePair<string, string>("7", "8"));
                    startday.Add(new KeyValuePair<string, string>("8", "9"));
                    startday.Add(new KeyValuePair<string, string>("9", "10"));
                    startday.Add(new KeyValuePair<string, string>("10", "11"));
                    startday.Add(new KeyValuePair<string, string>("11", "12"));
                    startday.Add(new KeyValuePair<string, string>("12", "13"));
                    startday.Add(new KeyValuePair<string, string>("13", "14"));
                    startday.Add(new KeyValuePair<string, string>("14", "15"));
                    startday.Add(new KeyValuePair<string, string>("15", "16"));
                    startday.Add(new KeyValuePair<string, string>("16", "17"));
                    startday.Add(new KeyValuePair<string, string>("17", "18"));
                    startday.Add(new KeyValuePair<string, string>("18", "19"));
                    startday.Add(new KeyValuePair<string, string>("19", "20"));
                    startday.Add(new KeyValuePair<string, string>("20", "21"));
                    startday.Add(new KeyValuePair<string, string>("21", "22"));
                    startday.Add(new KeyValuePair<string, string>("22", "23"));
                    startday.Add(new KeyValuePair<string, string>("23", "24"));
                    startday.Add(new KeyValuePair<string, string>("24", "25"));
                    startday.Add(new KeyValuePair<string, string>("25", "26"));
                    startday.Add(new KeyValuePair<string, string>("26", "27"));
                    startday.Add(new KeyValuePair<string, string>("27", "28"));
                    startday.Add(new KeyValuePair<string, string>("28", "29"));
                    startday.Add(new KeyValuePair<string, string>("29", "30"));
                    startday.Add(new KeyValuePair<string, string>("30", "31"));
                    break;
                case "3":
                    startday.Add(new KeyValuePair<string, string>("0", "1"));
                    startday.Add(new KeyValuePair<string, string>("1", "2"));
                    startday.Add(new KeyValuePair<string, string>("2", "3"));
                    startday.Add(new KeyValuePair<string, string>("3", "4"));
                    startday.Add(new KeyValuePair<string, string>("4", "5"));
                    startday.Add(new KeyValuePair<string, string>("5", "6"));
                    startday.Add(new KeyValuePair<string, string>("6", "7"));
                    startday.Add(new KeyValuePair<string, string>("7", "8"));
                    startday.Add(new KeyValuePair<string, string>("8", "9"));
                    startday.Add(new KeyValuePair<string, string>("9", "10"));
                    startday.Add(new KeyValuePair<string, string>("10", "11"));
                    startday.Add(new KeyValuePair<string, string>("11", "12"));
                    startday.Add(new KeyValuePair<string, string>("12", "13"));
                    startday.Add(new KeyValuePair<string, string>("13", "14"));
                    startday.Add(new KeyValuePair<string, string>("14", "15"));
                    startday.Add(new KeyValuePair<string, string>("15", "16"));
                    startday.Add(new KeyValuePair<string, string>("16", "17"));
                    startday.Add(new KeyValuePair<string, string>("17", "18"));
                    startday.Add(new KeyValuePair<string, string>("18", "19"));
                    startday.Add(new KeyValuePair<string, string>("19", "20"));
                    startday.Add(new KeyValuePair<string, string>("20", "21"));
                    startday.Add(new KeyValuePair<string, string>("21", "22"));
                    startday.Add(new KeyValuePair<string, string>("22", "23"));
                    startday.Add(new KeyValuePair<string, string>("23", "24"));
                    startday.Add(new KeyValuePair<string, string>("24", "25"));
                    startday.Add(new KeyValuePair<string, string>("25", "26"));
                    startday.Add(new KeyValuePair<string, string>("26", "27"));
                    startday.Add(new KeyValuePair<string, string>("27", "28"));
                    startday.Add(new KeyValuePair<string, string>("28", "29"));
                    startday.Add(new KeyValuePair<string, string>("29", "30"));
                    break;
                case "4":
                    startday.Add(new KeyValuePair<string, string>("0", "1"));
                    startday.Add(new KeyValuePair<string, string>("1", "2"));
                    startday.Add(new KeyValuePair<string, string>("2", "3"));
                    startday.Add(new KeyValuePair<string, string>("3", "4"));
                    startday.Add(new KeyValuePair<string, string>("4", "5"));
                    startday.Add(new KeyValuePair<string, string>("5", "6"));
                    startday.Add(new KeyValuePair<string, string>("6", "7"));
                    startday.Add(new KeyValuePair<string, string>("7", "8"));
                    startday.Add(new KeyValuePair<string, string>("8", "9"));
                    startday.Add(new KeyValuePair<string, string>("9", "10"));
                    startday.Add(new KeyValuePair<string, string>("10", "11"));
                    startday.Add(new KeyValuePair<string, string>("11", "12"));
                    startday.Add(new KeyValuePair<string, string>("12", "13"));
                    startday.Add(new KeyValuePair<string, string>("13", "14"));
                    startday.Add(new KeyValuePair<string, string>("14", "15"));
                    startday.Add(new KeyValuePair<string, string>("15", "16"));
                    startday.Add(new KeyValuePair<string, string>("16", "17"));
                    startday.Add(new KeyValuePair<string, string>("17", "18"));
                    startday.Add(new KeyValuePair<string, string>("18", "19"));
                    startday.Add(new KeyValuePair<string, string>("19", "20"));
                    startday.Add(new KeyValuePair<string, string>("20", "21"));
                    startday.Add(new KeyValuePair<string, string>("21", "22"));
                    startday.Add(new KeyValuePair<string, string>("22", "23"));
                    startday.Add(new KeyValuePair<string, string>("23", "24"));
                    startday.Add(new KeyValuePair<string, string>("24", "25"));
                    startday.Add(new KeyValuePair<string, string>("25", "26"));
                    startday.Add(new KeyValuePair<string, string>("26", "27"));
                    startday.Add(new KeyValuePair<string, string>("27", "28"));
                    startday.Add(new KeyValuePair<string, string>("28", "29"));
                    startday.Add(new KeyValuePair<string, string>("29", "30"));
                    startday.Add(new KeyValuePair<string, string>("30", "31"));
                    break;
                case "5":
                    startday.Add(new KeyValuePair<string, string>("0", "1"));
                    startday.Add(new KeyValuePair<string, string>("1", "2"));
                    startday.Add(new KeyValuePair<string, string>("2", "3"));
                    startday.Add(new KeyValuePair<string, string>("3", "4"));
                    startday.Add(new KeyValuePair<string, string>("4", "5"));
                    startday.Add(new KeyValuePair<string, string>("5", "6"));
                    startday.Add(new KeyValuePair<string, string>("6", "7"));
                    startday.Add(new KeyValuePair<string, string>("7", "8"));
                    startday.Add(new KeyValuePair<string, string>("8", "9"));
                    startday.Add(new KeyValuePair<string, string>("9", "10"));
                    startday.Add(new KeyValuePair<string, string>("10", "11"));
                    startday.Add(new KeyValuePair<string, string>("11", "12"));
                    startday.Add(new KeyValuePair<string, string>("12", "13"));
                    startday.Add(new KeyValuePair<string, string>("13", "14"));
                    startday.Add(new KeyValuePair<string, string>("14", "15"));
                    startday.Add(new KeyValuePair<string, string>("15", "16"));
                    startday.Add(new KeyValuePair<string, string>("16", "17"));
                    startday.Add(new KeyValuePair<string, string>("17", "18"));
                    startday.Add(new KeyValuePair<string, string>("18", "19"));
                    startday.Add(new KeyValuePair<string, string>("19", "20"));
                    startday.Add(new KeyValuePair<string, string>("20", "21"));
                    startday.Add(new KeyValuePair<string, string>("21", "22"));
                    startday.Add(new KeyValuePair<string, string>("22", "23"));
                    startday.Add(new KeyValuePair<string, string>("23", "24"));
                    startday.Add(new KeyValuePair<string, string>("24", "25"));
                    startday.Add(new KeyValuePair<string, string>("25", "26"));
                    startday.Add(new KeyValuePair<string, string>("26", "27"));
                    startday.Add(new KeyValuePair<string, string>("27", "28"));
                    startday.Add(new KeyValuePair<string, string>("28", "29"));
                    startday.Add(new KeyValuePair<string, string>("29", "30"));
                    break;
                case "6":
                    startday.Add(new KeyValuePair<string, string>("0", "1"));
                    startday.Add(new KeyValuePair<string, string>("1", "2"));
                    startday.Add(new KeyValuePair<string, string>("2", "3"));
                    startday.Add(new KeyValuePair<string, string>("3", "4"));
                    startday.Add(new KeyValuePair<string, string>("4", "5"));
                    startday.Add(new KeyValuePair<string, string>("5", "6"));
                    startday.Add(new KeyValuePair<string, string>("6", "7"));
                    startday.Add(new KeyValuePair<string, string>("7", "8"));
                    startday.Add(new KeyValuePair<string, string>("8", "9"));
                    startday.Add(new KeyValuePair<string, string>("9", "10"));
                    startday.Add(new KeyValuePair<string, string>("10", "11"));
                    startday.Add(new KeyValuePair<string, string>("11", "12"));
                    startday.Add(new KeyValuePair<string, string>("12", "13"));
                    startday.Add(new KeyValuePair<string, string>("13", "14"));
                    startday.Add(new KeyValuePair<string, string>("14", "15"));
                    startday.Add(new KeyValuePair<string, string>("15", "16"));
                    startday.Add(new KeyValuePair<string, string>("16", "17"));
                    startday.Add(new KeyValuePair<string, string>("17", "18"));
                    startday.Add(new KeyValuePair<string, string>("18", "19"));
                    startday.Add(new KeyValuePair<string, string>("19", "20"));
                    startday.Add(new KeyValuePair<string, string>("20", "21"));
                    startday.Add(new KeyValuePair<string, string>("21", "22"));
                    startday.Add(new KeyValuePair<string, string>("22", "23"));
                    startday.Add(new KeyValuePair<string, string>("23", "24"));
                    startday.Add(new KeyValuePair<string, string>("24", "25"));
                    startday.Add(new KeyValuePair<string, string>("25", "26"));
                    startday.Add(new KeyValuePair<string, string>("26", "27"));
                    startday.Add(new KeyValuePair<string, string>("27", "28"));
                    startday.Add(new KeyValuePair<string, string>("28", "29"));
                    startday.Add(new KeyValuePair<string, string>("29", "30"));
                    startday.Add(new KeyValuePair<string, string>("30", "31"));
                    break;
                case "7":
                    startday.Add(new KeyValuePair<string, string>("0", "1"));
                    startday.Add(new KeyValuePair<string, string>("1", "2"));
                    startday.Add(new KeyValuePair<string, string>("2", "3"));
                    startday.Add(new KeyValuePair<string, string>("3", "4"));
                    startday.Add(new KeyValuePair<string, string>("4", "5"));
                    startday.Add(new KeyValuePair<string, string>("5", "6"));
                    startday.Add(new KeyValuePair<string, string>("6", "7"));
                    startday.Add(new KeyValuePair<string, string>("7", "8"));
                    startday.Add(new KeyValuePair<string, string>("8", "9"));
                    startday.Add(new KeyValuePair<string, string>("9", "10"));
                    startday.Add(new KeyValuePair<string, string>("10", "11"));
                    startday.Add(new KeyValuePair<string, string>("11", "12"));
                    startday.Add(new KeyValuePair<string, string>("12", "13"));
                    startday.Add(new KeyValuePair<string, string>("13", "14"));
                    startday.Add(new KeyValuePair<string, string>("14", "15"));
                    startday.Add(new KeyValuePair<string, string>("15", "16"));
                    startday.Add(new KeyValuePair<string, string>("16", "17"));
                    startday.Add(new KeyValuePair<string, string>("17", "18"));
                    startday.Add(new KeyValuePair<string, string>("18", "19"));
                    startday.Add(new KeyValuePair<string, string>("19", "20"));
                    startday.Add(new KeyValuePair<string, string>("20", "21"));
                    startday.Add(new KeyValuePair<string, string>("21", "22"));
                    startday.Add(new KeyValuePair<string, string>("22", "23"));
                    startday.Add(new KeyValuePair<string, string>("23", "24"));
                    startday.Add(new KeyValuePair<string, string>("24", "25"));
                    startday.Add(new KeyValuePair<string, string>("25", "26"));
                    startday.Add(new KeyValuePair<string, string>("26", "27"));
                    startday.Add(new KeyValuePair<string, string>("27", "28"));
                    startday.Add(new KeyValuePair<string, string>("28", "29"));
                    startday.Add(new KeyValuePair<string, string>("29", "30"));
                    startday.Add(new KeyValuePair<string, string>("30", "31"));
                    break;
                case "8":
                    startday.Add(new KeyValuePair<string, string>("0", "1"));
                    startday.Add(new KeyValuePair<string, string>("1", "2"));
                    startday.Add(new KeyValuePair<string, string>("2", "3"));
                    startday.Add(new KeyValuePair<string, string>("3", "4"));
                    startday.Add(new KeyValuePair<string, string>("4", "5"));
                    startday.Add(new KeyValuePair<string, string>("5", "6"));
                    startday.Add(new KeyValuePair<string, string>("6", "7"));
                    startday.Add(new KeyValuePair<string, string>("7", "8"));
                    startday.Add(new KeyValuePair<string, string>("8", "9"));
                    startday.Add(new KeyValuePair<string, string>("9", "10"));
                    startday.Add(new KeyValuePair<string, string>("10", "11"));
                    startday.Add(new KeyValuePair<string, string>("11", "12"));
                    startday.Add(new KeyValuePair<string, string>("12", "13"));
                    startday.Add(new KeyValuePair<string, string>("13", "14"));
                    startday.Add(new KeyValuePair<string, string>("14", "15"));
                    startday.Add(new KeyValuePair<string, string>("15", "16"));
                    startday.Add(new KeyValuePair<string, string>("16", "17"));
                    startday.Add(new KeyValuePair<string, string>("17", "18"));
                    startday.Add(new KeyValuePair<string, string>("18", "19"));
                    startday.Add(new KeyValuePair<string, string>("19", "20"));
                    startday.Add(new KeyValuePair<string, string>("20", "21"));
                    startday.Add(new KeyValuePair<string, string>("21", "22"));
                    startday.Add(new KeyValuePair<string, string>("22", "23"));
                    startday.Add(new KeyValuePair<string, string>("23", "24"));
                    startday.Add(new KeyValuePair<string, string>("24", "25"));
                    startday.Add(new KeyValuePair<string, string>("25", "26"));
                    startday.Add(new KeyValuePair<string, string>("26", "27"));
                    startday.Add(new KeyValuePair<string, string>("27", "28"));
                    startday.Add(new KeyValuePair<string, string>("28", "29"));
                    startday.Add(new KeyValuePair<string, string>("29", "30"));
                    break;
                case "9":
                    startday.Add(new KeyValuePair<string, string>("0", "1"));
                    startday.Add(new KeyValuePair<string, string>("1", "2"));
                    startday.Add(new KeyValuePair<string, string>("2", "3"));
                    startday.Add(new KeyValuePair<string, string>("3", "4"));
                    startday.Add(new KeyValuePair<string, string>("4", "5"));
                    startday.Add(new KeyValuePair<string, string>("5", "6"));
                    startday.Add(new KeyValuePair<string, string>("6", "7"));
                    startday.Add(new KeyValuePair<string, string>("7", "8"));
                    startday.Add(new KeyValuePair<string, string>("8", "9"));
                    startday.Add(new KeyValuePair<string, string>("9", "10"));
                    startday.Add(new KeyValuePair<string, string>("10", "11"));
                    startday.Add(new KeyValuePair<string, string>("11", "12"));
                    startday.Add(new KeyValuePair<string, string>("12", "13"));
                    startday.Add(new KeyValuePair<string, string>("13", "14"));
                    startday.Add(new KeyValuePair<string, string>("14", "15"));
                    startday.Add(new KeyValuePair<string, string>("15", "16"));
                    startday.Add(new KeyValuePair<string, string>("16", "17"));
                    startday.Add(new KeyValuePair<string, string>("17", "18"));
                    startday.Add(new KeyValuePair<string, string>("18", "19"));
                    startday.Add(new KeyValuePair<string, string>("19", "20"));
                    startday.Add(new KeyValuePair<string, string>("20", "21"));
                    startday.Add(new KeyValuePair<string, string>("21", "22"));
                    startday.Add(new KeyValuePair<string, string>("22", "23"));
                    startday.Add(new KeyValuePair<string, string>("23", "24"));
                    startday.Add(new KeyValuePair<string, string>("24", "25"));
                    startday.Add(new KeyValuePair<string, string>("25", "26"));
                    startday.Add(new KeyValuePair<string, string>("26", "27"));
                    startday.Add(new KeyValuePair<string, string>("27", "28"));
                    startday.Add(new KeyValuePair<string, string>("28", "29"));
                    startday.Add(new KeyValuePair<string, string>("29", "30"));
                    startday.Add(new KeyValuePair<string, string>("30", "31"));
                    break;
                case "10":
                    startday.Add(new KeyValuePair<string, string>("0", "1"));
                    startday.Add(new KeyValuePair<string, string>("1", "2"));
                    startday.Add(new KeyValuePair<string, string>("2", "3"));
                    startday.Add(new KeyValuePair<string, string>("3", "4"));
                    startday.Add(new KeyValuePair<string, string>("4", "5"));
                    startday.Add(new KeyValuePair<string, string>("5", "6"));
                    startday.Add(new KeyValuePair<string, string>("6", "7"));
                    startday.Add(new KeyValuePair<string, string>("7", "8"));
                    startday.Add(new KeyValuePair<string, string>("8", "9"));
                    startday.Add(new KeyValuePair<string, string>("9", "10"));
                    startday.Add(new KeyValuePair<string, string>("10", "11"));
                    startday.Add(new KeyValuePair<string, string>("11", "12"));
                    startday.Add(new KeyValuePair<string, string>("12", "13"));
                    startday.Add(new KeyValuePair<string, string>("13", "14"));
                    startday.Add(new KeyValuePair<string, string>("14", "15"));
                    startday.Add(new KeyValuePair<string, string>("15", "16"));
                    startday.Add(new KeyValuePair<string, string>("16", "17"));
                    startday.Add(new KeyValuePair<string, string>("17", "18"));
                    startday.Add(new KeyValuePair<string, string>("18", "19"));
                    startday.Add(new KeyValuePair<string, string>("19", "20"));
                    startday.Add(new KeyValuePair<string, string>("20", "21"));
                    startday.Add(new KeyValuePair<string, string>("21", "22"));
                    startday.Add(new KeyValuePair<string, string>("22", "23"));
                    startday.Add(new KeyValuePair<string, string>("23", "24"));
                    startday.Add(new KeyValuePair<string, string>("24", "25"));
                    startday.Add(new KeyValuePair<string, string>("25", "26"));
                    startday.Add(new KeyValuePair<string, string>("26", "27"));
                    startday.Add(new KeyValuePair<string, string>("27", "28"));
                    startday.Add(new KeyValuePair<string, string>("28", "29"));
                    startday.Add(new KeyValuePair<string, string>("29", "30"));
                    break;
                case "11":
                    startday.Add(new KeyValuePair<string, string>("0", "1"));
                    startday.Add(new KeyValuePair<string, string>("1", "2"));
                    startday.Add(new KeyValuePair<string, string>("2", "3"));
                    startday.Add(new KeyValuePair<string, string>("3", "4"));
                    startday.Add(new KeyValuePair<string, string>("4", "5"));
                    startday.Add(new KeyValuePair<string, string>("5", "6"));
                    startday.Add(new KeyValuePair<string, string>("6", "7"));
                    startday.Add(new KeyValuePair<string, string>("7", "8"));
                    startday.Add(new KeyValuePair<string, string>("8", "9"));
                    startday.Add(new KeyValuePair<string, string>("9", "10"));
                    startday.Add(new KeyValuePair<string, string>("10", "11"));
                    startday.Add(new KeyValuePair<string, string>("11", "12"));
                    startday.Add(new KeyValuePair<string, string>("12", "13"));
                    startday.Add(new KeyValuePair<string, string>("13", "14"));
                    startday.Add(new KeyValuePair<string, string>("14", "15"));
                    startday.Add(new KeyValuePair<string, string>("15", "16"));
                    startday.Add(new KeyValuePair<string, string>("16", "17"));
                    startday.Add(new KeyValuePair<string, string>("17", "18"));
                    startday.Add(new KeyValuePair<string, string>("18", "19"));
                    startday.Add(new KeyValuePair<string, string>("19", "20"));
                    startday.Add(new KeyValuePair<string, string>("20", "21"));
                    startday.Add(new KeyValuePair<string, string>("21", "22"));
                    startday.Add(new KeyValuePair<string, string>("22", "23"));
                    startday.Add(new KeyValuePair<string, string>("23", "24"));
                    startday.Add(new KeyValuePair<string, string>("24", "25"));
                    startday.Add(new KeyValuePair<string, string>("25", "26"));
                    startday.Add(new KeyValuePair<string, string>("26", "27"));
                    startday.Add(new KeyValuePair<string, string>("27", "28"));
                    startday.Add(new KeyValuePair<string, string>("28", "29"));
                    startday.Add(new KeyValuePair<string, string>("29", "30"));
                    startday.Add(new KeyValuePair<string, string>("30", "31"));
                    break;
            }
            cboStartDay.DataSource = startday;
            cboStartDay.ValueMember = "Key";
            cboStartDay.DisplayMember = "Value";

            calendar.Attribute("startMonth").Value = cboStartMonth.SelectedValue.ToString();
            xelem.Save(Globals.GlobalVariables.PATH);
        }

        private void cboMapScale_SelectionChangeCommitted(object sender, EventArgs e)
        {//*1
            XElement xelem = XElement.Load(Globals.GlobalVariables.PATH);

            string xpathenviron = "ENVIRONMENT";
            var environ = xelem.XPathSelectElement(xpathenviron);

            string xpathmap = "MAP";
            var map = xelem.XPathSelectElement(xpathmap);

            string strClimateArea = cboClimateArea.SelectedValue.ToString();
            string strMaxY = map.Attribute("maxy").Value.ToString();
            string strMapScale = cboMapScale.Text;

            //int iMaxY = int.Parse(strMaxY);
            int iMaxY = Convert.ToInt32(strMaxY);
            iMaxY += 1;
            //decimal dMapScale = decimal.Parse(strMapScale);
            decimal dMapScale = Convert.ToDecimal(strMapScale);
            int iFullY = (short)(iMaxY * dMapScale);

            environ.Attribute("scale").Value = cboMapScale.SelectedValue.ToString();

            //RETRIEVE WEATHERZONE2 DATA FROM XML
            string xpathwz2 = "WEATHERZONES/ZONE[@ID=2]";
            var weatherzone2 = xelem.XPathSelectElement(xpathwz2);

            //RETRIEVE WEATHERZONE3 DATA FROM XML
            string xpathwz3 = "WEATHERZONES/ZONE[@ID=3]";
            var weatherzone3 = xelem.XPathSelectElement(xpathwz3);

            if (strClimateArea == "0" || strClimateArea == "1")
            {//*2CLIMATEAREA != EQUATORIAL
                if (iFullY >= 900)
                {//*3 >=900
                    int iBoundary1 = (iMaxY / 2) + 1;
                    environ.Attribute("boundary1").Value = iBoundary1.ToString();

                    //SET WEATHERZONE VIZ AND FORM SIZE
                    gbWZ2.Visible = true;
                    gbWZ3.Visible = false;
                    //environ.Attribute("boundary2").Value = "999";
                    this.Size = new Size(435, 460);
                    btnCloseEnviron.Top = 375;
                    btnSaveEnviron.Top = 375;

                    //POPULATES WZ2 COMBOBOXES
                    //WZ2PRECIPITATION
                    var wz2precip = new BindingList<KeyValuePair<string, string>>();
                    wz2precip.Add(new KeyValuePair<string, string>("4", "None"));
                    wz2precip.Add(new KeyValuePair<string, string>("3", "Occasional"));
                    wz2precip.Add(new KeyValuePair<string, string>("2", "Light"));
                    wz2precip.Add(new KeyValuePair<string, string>("1", "Moderate"));
                    wz2precip.Add(new KeyValuePair<string, string>("0", "Heavy"));

                    cboWZ2Precip.DataSource = wz2precip;
                    cboWZ2Precip.ValueMember = "Key";
                    cboWZ2Precip.DisplayMember = "Value";

                    ////cboWZ2Precip.SelectedValue = weatherzone2.Attribute("precipitation").Value.ToString();
                    cboWZ2Precip.SelectedValue = cboPrecipitation.SelectedValue;
                    weatherzone2.Attribute("precipitation").Value = cboWZ2Precip.SelectedValue.ToString();

                    //WZ2 TEMPERATURE
                    var wz2temp = new BindingList<KeyValuePair<string, string>>();
                    wz2temp.Add(new KeyValuePair<string, string>("7", "Hot"));
                    wz2temp.Add(new KeyValuePair<string, string>("6", "Warm"));
                    wz2temp.Add(new KeyValuePair<string, string>("5", "Temperate"));
                    wz2temp.Add(new KeyValuePair<string, string>("4", "Cool"));
                    wz2temp.Add(new KeyValuePair<string, string>("3", "Cold"));
                    wz2temp.Add(new KeyValuePair<string, string>("2", "Frozen1"));
                    wz2temp.Add(new KeyValuePair<string, string>("1", "Frozen2"));
                    wz2temp.Add(new KeyValuePair<string, string>("0", "Frozen3"));

                    cboWZ2Temp.DataSource = wz2temp;
                    cboWZ2Temp.ValueMember = "Key";
                    cboWZ2Temp.DisplayMember = "Value";
                    //**********************
                    //SET PROGRESSION OF TEMPS THROUGH WEATHER ZONES
                    int intWZModifier;
                    //int iTemp = Int32.Parse(cboTemperature.SelectedValue.ToString());
                    int iTemp = Convert.ToInt32(cboTemperature.SelectedValue.ToString());
                    if (strClimateArea == "0")//NORTHERN CLIMATE AREA
                    {//*4 NORTH
                        intWZModifier = 1;
                        if (iTemp <= 6)
                        {//*5
                            //weatherzone2.Attribute("temperature").Value = (iTemp + 1).ToString();
                            cboWZ2Temp.SelectedValue = (iTemp + intWZModifier).ToString();
                        }//**5
                        else
                        {//*6
                            //weatherzone2.Attribute("temperature").Value = "7";
                            cboWZ2Temp.SelectedValue = "7";
                        }//*6
                    }//**4NORTH
                    else  //SOUTHERN CLIMATE AREA
                    {//*7 SOUTH
                        intWZModifier = -1;

                        if (iTemp >= 1)
                        {//*8
                            cboWZ2Temp.SelectedValue = (iTemp + intWZModifier).ToString();
                        }//**8
                        else
                        {//*9
                            cboWZ2Temp.SelectedValue = "0";
                        }//**9
                    }//**7
                    //cboWZ2Temp.SelectedValue = (iTemp + intWZModifier).ToString();
                    weatherzone2.Attribute("temperature").Value = cboWZ2Temp.SelectedValue.ToString();

                    //WZ2 VISIBILITY
                    var wz2vis = new BindingList<KeyValuePair<string, string>>();
                    wz2vis.Add(new KeyValuePair<string, string>("0", "Fair"));
                    wz2vis.Add(new KeyValuePair<string, string>("1", "Hazy"));
                    wz2vis.Add(new KeyValuePair<string, string>("2", "Overcast"));

                    cboWZ2Vis.DataSource = wz2vis;
                    cboWZ2Vis.ValueMember = "Key";
                    cboWZ2Vis.DisplayMember = "Value";

                    ////cboWZ2Vis.SelectedValue = weatherzone2.Attribute("visibility").Value.ToString();
                    cboWZ2Vis.SelectedValue = cboWZ1Vis.SelectedValue;
                    weatherzone2.Attribute("visibility").Value = cboWZ2Vis.SelectedValue.ToString();

                    //WZ2 TEXTBOXES
                    txtWZ2Border.Text = iBoundary1.ToString();
                ///}//**3 >900
                if (iFullY >= 1500)
                    {//*10>1500
                        gbWZ2.Visible = true;
                        gbWZ3.Visible = true;
                        this.Size = new Size(435, 600);
                        btnCloseEnviron.Top = 515;
                        btnSaveEnviron.Top = 515;

                        iBoundary1 = (iMaxY / 3) + 1;
                        environ.Attribute("boundary1").Value = iBoundary1.ToString();

                        int iBoundary2 = (((iMaxY / 3) + 1) * 2);
                        environ.Attribute("boundary2").Value = iBoundary2.ToString();

                        //POPULATES WZ3 COMBOBOXES
                        //WZ3PRECIPITATION
                        var wz3precip = new BindingList<KeyValuePair<string, string>>();
                        wz3precip.Add(new KeyValuePair<string, string>("4", "None"));
                        wz3precip.Add(new KeyValuePair<string, string>("3", "Occasional"));
                        wz3precip.Add(new KeyValuePair<string, string>("2", "Light"));
                        wz3precip.Add(new KeyValuePair<string, string>("1", "Moderate"));
                        wz3precip.Add(new KeyValuePair<string, string>("0", "Heavy"));

                        cboWZ3Precip.DataSource = wz3precip;
                        cboWZ3Precip.ValueMember = "Key";
                        cboWZ3Precip.DisplayMember = "Value";

                        cboWZ3Precip.SelectedValue = weatherzone3.Attribute("precipitation").Value.ToString();

                        //WZ3 TEMPERATURE
                        var wz3temp = new BindingList<KeyValuePair<string, string>>();
                        wz3temp.Add(new KeyValuePair<string, string>("7", "Hot"));
                        wz3temp.Add(new KeyValuePair<string, string>("6", "Warm"));
                        wz3temp.Add(new KeyValuePair<string, string>("5", "Temperate"));
                        wz3temp.Add(new KeyValuePair<string, string>("4", "Cool"));
                        wz3temp.Add(new KeyValuePair<string, string>("3", "Cold"));
                        wz3temp.Add(new KeyValuePair<string, string>("2", "Frozen1"));
                        wz3temp.Add(new KeyValuePair<string, string>("1", "Frozen2"));
                        wz3temp.Add(new KeyValuePair<string, string>("0", "Frozen3"));

                        cboWZ3Temp.DataSource = wz3temp;
                        cboWZ3Temp.ValueMember = "Key";
                        cboWZ3Temp.DisplayMember = "Value";

                        if (strClimateArea == "0")//NORTHERN CLIMATE AREA
                        {//*11
                            intWZModifier = 1;
                            if (iTemp <= 5)
                            {//*12
                                //weatherzone2.Attribute("temperature").Value = (iTemp + 1).ToString();
                                cboWZ2Temp.SelectedValue = (iTemp + intWZModifier).ToString();
                                cboWZ3Temp.SelectedValue = (iTemp + (2 * intWZModifier)).ToString();
                            }//**12
                            else
                            {//*13
                                //weatherzone2.Attribute("temperature").Value = "7";
                                cboWZ2Temp.SelectedValue = "7";
                                cboWZ3Temp.SelectedValue = "7";
                            }//*13
                        }//**11
                        else  //SOUTHERN CLIMATE AREA
                        {//*14
                            intWZModifier = -1;

                            if (iTemp >= 2)
                            {//*15
                                cboWZ2Temp.SelectedValue = (iTemp + intWZModifier).ToString();
                                cboWZ3Temp.SelectedValue = (iTemp + (2 * intWZModifier)).ToString();
                            }//**15
                            else
                            {//*16
                                cboWZ2Temp.SelectedValue = "0";
                                cboWZ3Temp.SelectedValue = "0";
                            }//**16
                            weatherzone2.Attribute("temperature").Value = cboWZ2Temp.SelectedValue.ToString();
                            weatherzone3.Attribute("temperature").Value = cboWZ3Temp.SelectedValue.ToString();
                        }//**14
                            //WZ3 VISIBILITY
                            var wz3vis = new BindingList<KeyValuePair<string, string>>();
                            wz3vis.Add(new KeyValuePair<string, string>("0", "Fair"));
                            wz3vis.Add(new KeyValuePair<string, string>("1", "Hazy"));
                            wz3vis.Add(new KeyValuePair<string, string>("2", "Overcast"));

                            cboWZ3Vis.DataSource = wz3vis;
                            cboWZ3Vis.ValueMember = "Key";
                            cboWZ3Vis.DisplayMember = "Value";

                            cboWZ2Vis.SelectedValue = cboWZ1Vis.SelectedValue;
                            cboWZ3Vis.SelectedValue = cboWZ1Vis.SelectedValue;
                            weatherzone2.Attribute("visibility").Value = cboWZ2Vis.SelectedValue.ToString();
                            weatherzone3.Attribute("visibility").Value = cboWZ3Vis.SelectedValue.ToString();

                            //WZ3 TEXTBOXES
                            txtWZ2Border.Text = iBoundary1.ToString();
                            txtWZ3Border.Text = iBoundary2.ToString();
                            //}
                    }//**10>1500
                }//**3 >900
                else //<900
                {//*17
                      gbWZ2.Visible = false;
                      gbWZ3.Visible = false;
                      this.Size = new Size(435, 460);
                      btnCloseEnviron.Top = 375;
                      btnSaveEnviron.Top = 375;
                      environ.Attribute("boundary1").Value = "999";
                      environ.Attribute("boundary2").Value = "999";
                }//**17
                
            }//**2climatearea************
            xelem.Save(Globals.GlobalVariables.PATH);
           
        }//**1sub
    
        private void cboClimateArea_SelectionChangeCommitted(object sender, EventArgs e)
        {
            XElement xelem = XElement.Load(Globals.GlobalVariables.PATH);

            string xpathenviron = "ENVIRONMENT";
            var environ = xelem.XPathSelectElement(xpathenviron);

            string xpathmap = "MAP";
            var map = xelem.XPathSelectElement(xpathmap);

            string strClimateArea = cboClimateArea.SelectedValue.ToString();
            string strMaxY = map.Attribute("maxy").Value.ToString();
            string strMapScale = cboMapScale.Text;

            //int iMaxY = int.Parse(strMaxY);
            int iMaxY = Convert.ToInt32(strMaxY);
            iMaxY += 1;
            //decimal dMapScale = decimal.Parse(strMapScale);
            decimal dMapScale = Convert.ToDecimal(strMapScale);
            int iFullY = (short)(iMaxY * dMapScale);
            //int iTemp = Int32.Parse(cboTemperature.SelectedValue.ToString());
            int iTemp = Convert.ToInt32(cboTemperature.SelectedValue.ToString());

            environ.Attribute("scale").Value = cboMapScale.SelectedValue.ToString();

            //RETRIEVE WEATHERZONE2 DATA FROM XML
            string xpathwz2 = "WEATHERZONES/ZONE[@ID=2]";
            var weatherzone2 = xelem.XPathSelectElement(xpathwz2);

            //RETRIEVE WEATHERZONE3 DATA FROM XML
            string xpathwz3 = "WEATHERZONES/ZONE[@ID=3]";
            var weatherzone3 = xelem.XPathSelectElement(xpathwz3);

            //IF EQUATORIAL CLIMATE AREA, NO WEATHERZONES
            if (strClimateArea == "2")
            {
                gbWZ2.Visible = false;
                gbWZ3.Visible = false;
                this.Size = new Size(435, 460);
                btnCloseEnviron.Top = 375;
                btnSaveEnviron.Top = 375;
                environ.Attribute("boundary1").Value = "999";
                environ.Attribute("boundary2").Value = "999";
            }
            else //OTHER CLIMATE AREAS
            {

                if (iFullY >= 900)
                {//*3 >=900
                    int iBoundary1 = (iMaxY / 2) + 1;
                    environ.Attribute("boundary1").Value = iBoundary1.ToString();

                    //SET WEATHERZONE VIZ AND FORM SIZE
                    gbWZ2.Visible = true;
                    gbWZ3.Visible = false;
                    //environ.Attribute("boundary2").Value = "999";
                    this.Size = new Size(435, 460);
                    btnCloseEnviron.Top = 375;
                    btnSaveEnviron.Top = 375;

                    //POPULATES WZ2 COMBOBOXES
                    //WZ2PRECIPITATION
                    var wz2precip = new BindingList<KeyValuePair<string, string>>();
                    wz2precip.Add(new KeyValuePair<string, string>("4", "None"));
                    wz2precip.Add(new KeyValuePair<string, string>("3", "Occasional"));
                    wz2precip.Add(new KeyValuePair<string, string>("2", "Light"));
                    wz2precip.Add(new KeyValuePair<string, string>("1", "Moderate"));
                    wz2precip.Add(new KeyValuePair<string, string>("0", "Heavy"));

                    cboWZ2Precip.DataSource = wz2precip;
                    cboWZ2Precip.ValueMember = "Key";
                    cboWZ2Precip.DisplayMember = "Value";

                    ////cboWZ2Precip.SelectedValue = weatherzone2.Attribute("precipitation").Value.ToString();
                    cboWZ2Precip.SelectedValue = cboPrecipitation.SelectedValue;
                    weatherzone2.Attribute("precipitation").Value = cboWZ2Precip.SelectedValue.ToString();

                    //WZ2 TEMPERATURE
                    var wz2temp = new BindingList<KeyValuePair<string, string>>();
                    wz2temp.Add(new KeyValuePair<string, string>("7", "Hot"));
                    wz2temp.Add(new KeyValuePair<string, string>("6", "Warm"));
                    wz2temp.Add(new KeyValuePair<string, string>("5", "Temperate"));
                    wz2temp.Add(new KeyValuePair<string, string>("4", "Cool"));
                    wz2temp.Add(new KeyValuePair<string, string>("3", "Cold"));
                    wz2temp.Add(new KeyValuePair<string, string>("2", "Frozen1"));
                    wz2temp.Add(new KeyValuePair<string, string>("1", "Frozen2"));
                    wz2temp.Add(new KeyValuePair<string, string>("0", "Frozen3"));

                    cboWZ2Temp.DataSource = wz2temp;
                    cboWZ2Temp.ValueMember = "Key";
                    cboWZ2Temp.DisplayMember = "Value";
                    //**********************
                    //SET PROGRESSION OF TEMPS THROUGH WEATHER ZONES
                    int intWZModifier;
                    //iTemp = Int32.Parse(cboTemperature.SelectedValue.ToString());
                    iTemp = Convert.ToInt32(cboTemperature.SelectedValue.ToString());
                    if (strClimateArea == "0")//NORTHERN CLIMATE AREA
                    {//*4 NORTH
                        intWZModifier = 1;
                        if (iTemp <= 6)
                        {//*5
                            //weatherzone2.Attribute("temperature").Value = (iTemp + 1).ToString();
                            cboWZ2Temp.SelectedValue = (iTemp + intWZModifier).ToString();
                        }//**5
                        else
                        {//*6
                            //weatherzone2.Attribute("temperature").Value = "7";
                            cboWZ2Temp.SelectedValue = "7";
                        }//*6
                    }//**4NORTH
                    else  //SOUTHERN CLIMATE AREA
                    {//*7 SOUTH
                        intWZModifier = -1;

                        if (iTemp >= 1)
                        {//*8
                            cboWZ2Temp.SelectedValue = (iTemp + intWZModifier).ToString();
                        }//**8
                        else
                        {//*9
                            cboWZ2Temp.SelectedValue = "0";
                        }//**9
                    }//**7
                    //cboWZ2Temp.SelectedValue = (iTemp + intWZModifier).ToString();
                    weatherzone2.Attribute("temperature").Value = cboWZ2Temp.SelectedValue.ToString();

                    //WZ2 VISIBILITY
                    var wz2vis = new BindingList<KeyValuePair<string, string>>();
                    wz2vis.Add(new KeyValuePair<string, string>("0", "Fair"));
                    wz2vis.Add(new KeyValuePair<string, string>("1", "Hazy"));
                    wz2vis.Add(new KeyValuePair<string, string>("2", "Overcast"));

                    cboWZ2Vis.DataSource = wz2vis;
                    cboWZ2Vis.ValueMember = "Key";
                    cboWZ2Vis.DisplayMember = "Value";

                    ////cboWZ2Vis.SelectedValue = weatherzone2.Attribute("visibility").Value.ToString();
                    cboWZ2Vis.SelectedValue = cboWZ1Vis.SelectedValue;
                    weatherzone2.Attribute("visibility").Value = cboWZ2Vis.SelectedValue.ToString();

                    //WZ2 TEXTBOXES
                    txtWZ2Border.Text = iBoundary1.ToString();
                    ///}//**3 >900
                    if (iFullY >= 1500)
                    {//*10>1500
                        gbWZ2.Visible = true;
                        gbWZ3.Visible = true;
                        this.Size = new Size(435, 600);
                        btnCloseEnviron.Top = 515;
                        btnSaveEnviron.Top = 515;

                        iBoundary1 = (iMaxY / 3) + 1;
                        environ.Attribute("boundary1").Value = iBoundary1.ToString();

                        int iBoundary2 = (((iMaxY / 3) + 1) * 2);
                        environ.Attribute("boundary2").Value = iBoundary2.ToString();

                        //POPULATES WZ3 COMBOBOXES
                        //WZ3PRECIPITATION
                        var wz3precip = new BindingList<KeyValuePair<string, string>>();
                        wz3precip.Add(new KeyValuePair<string, string>("4", "None"));
                        wz3precip.Add(new KeyValuePair<string, string>("3", "Occasional"));
                        wz3precip.Add(new KeyValuePair<string, string>("2", "Light"));
                        wz3precip.Add(new KeyValuePair<string, string>("1", "Moderate"));
                        wz3precip.Add(new KeyValuePair<string, string>("0", "Heavy"));

                        cboWZ3Precip.DataSource = wz3precip;
                        cboWZ3Precip.ValueMember = "Key";
                        cboWZ3Precip.DisplayMember = "Value";

                        cboWZ3Precip.SelectedValue = weatherzone3.Attribute("precipitation").Value.ToString();

                        //WZ3 TEMPERATURE
                        var wz3temp = new BindingList<KeyValuePair<string, string>>();
                        wz3temp.Add(new KeyValuePair<string, string>("7", "Hot"));
                        wz3temp.Add(new KeyValuePair<string, string>("6", "Warm"));
                        wz3temp.Add(new KeyValuePair<string, string>("5", "Temperate"));
                        wz3temp.Add(new KeyValuePair<string, string>("4", "Cool"));
                        wz3temp.Add(new KeyValuePair<string, string>("3", "Cold"));
                        wz3temp.Add(new KeyValuePair<string, string>("2", "Frozen1"));
                        wz3temp.Add(new KeyValuePair<string, string>("1", "Frozen2"));
                        wz3temp.Add(new KeyValuePair<string, string>("0", "Frozen3"));

                        cboWZ3Temp.DataSource = wz3temp;
                        cboWZ3Temp.ValueMember = "Key";
                        cboWZ3Temp.DisplayMember = "Value";

                        if (strClimateArea == "0")//NORTHERN CLIMATE AREA
                        {//*11
                            intWZModifier = 1;
                            if (iTemp <= 5)
                            {//*12
                                //weatherzone2.Attribute("temperature").Value = (iTemp + 1).ToString();
                                cboWZ2Temp.SelectedValue = (iTemp + intWZModifier).ToString();
                                cboWZ3Temp.SelectedValue = (iTemp + (2 * intWZModifier)).ToString();
                            }//**12
                            else
                            {//*13
                                //weatherzone2.Attribute("temperature").Value = "7";
                                cboWZ2Temp.SelectedValue = "7";
                                cboWZ3Temp.SelectedValue = "7";
                            }//*13
                        }//**11
                        else  //SOUTHERN CLIMATE AREA
                        {//*14
                            intWZModifier = -1;

                            if (iTemp >= 2)
                            {//*15
                                cboWZ2Temp.SelectedValue = (iTemp + intWZModifier).ToString();
                                cboWZ3Temp.SelectedValue = (iTemp + (2 * intWZModifier)).ToString();
                            }//**15
                            else
                            {//*16
                                cboWZ2Temp.SelectedValue = "0";
                                cboWZ3Temp.SelectedValue = "0";
                            }//**16
                            weatherzone2.Attribute("temperature").Value = cboWZ2Temp.SelectedValue.ToString();
                            weatherzone3.Attribute("temperature").Value = cboWZ3Temp.SelectedValue.ToString();
                        }//**14
                         //WZ3 VISIBILITY
                        var wz3vis = new BindingList<KeyValuePair<string, string>>();
                        wz3vis.Add(new KeyValuePair<string, string>("0", "Fair"));
                        wz3vis.Add(new KeyValuePair<string, string>("1", "Hazy"));
                        wz3vis.Add(new KeyValuePair<string, string>("2", "Overcast"));

                        cboWZ3Vis.DataSource = wz3vis;
                        cboWZ3Vis.ValueMember = "Key";
                        cboWZ3Vis.DisplayMember = "Value";

                        cboWZ2Vis.SelectedValue = cboWZ1Vis.SelectedValue;
                        cboWZ3Vis.SelectedValue = cboWZ1Vis.SelectedValue;
                        weatherzone2.Attribute("visibility").Value = cboWZ2Vis.SelectedValue.ToString();
                        weatherzone3.Attribute("visibility").Value = cboWZ3Vis.SelectedValue.ToString();

                        //WZ3 TEXTBOXES
                        txtWZ2Border.Text = iBoundary1.ToString();
                        txtWZ3Border.Text = iBoundary2.ToString();
                        //}
                    }//**10>1500
                }//**3 >900
                else //<900
                {//*17
                    gbWZ2.Visible = false;
                    gbWZ3.Visible = false;
                    this.Size = new Size(435, 460);
                    btnCloseEnviron.Top = 375;
                    btnSaveEnviron.Top = 375;
                    environ.Attribute("boundary1").Value = "999";
                    environ.Attribute("boundary2").Value = "999";
                }//**17
            }//****

            environ.Attribute("zone").Value = cboClimateArea.SelectedValue.ToString();
            xelem.Save(Globals.GlobalVariables.PATH);
        }

        private void cboTemperature_SelectionChangeCommitted(object sender, EventArgs e)
        {
            XElement xelem = XElement.Load(Globals.GlobalVariables.PATH);
            string xpathenviron = "ENVIRONMENT";
            var environ = xelem.XPathSelectElement(xpathenviron);
            //int iTemp = Int32.Parse(cboTemperature.SelectedValue.ToString());
            int iTemp = Convert.ToInt32(cboTemperature.SelectedValue.ToString());
            //string strClimateArea;
            string strClimateArea = cboClimateArea.SelectedValue.ToString();
            int intWZModifier;

            //RETRIEVE WEATHERZONE1 DATA FROM XML
            string xpathwz1 = "WEATHERZONES/ZONE[@ID=1]";
            var weatherzone1 = xelem.XPathSelectElement(xpathwz1);

            //RETRIEVE WEATHERZONE2 DATA FROM XML
            string xpathwz2 = "WEATHERZONES/ZONE[@ID=2]";
            var weatherzone2 = xelem.XPathSelectElement(xpathwz2);

            //RETRIEVE WEATHERZONE3 DATA FROM XML
            string xpathwz3 = "WEATHERZONES/ZONE[@ID=3]";
            var weatherzone3 = xelem.XPathSelectElement(xpathwz3);

            //WRITE CBOSETTING TO XML
            environ.Attribute("temperature").Value = cboTemperature.SelectedValue.ToString();
            weatherzone1.Attribute("temperature").Value = cboTemperature.SelectedValue.ToString();

            //cboWZ1Temp.SelectedValue = iTemp.ToString();
            //cboWZ1Temp.SelectedValue = cboTemperature.SelectedValue;

            //SET WZ2 TEMPERATURE
            if (environ.Attribute("boundary1").Value.ToString() != "999")
            {
                if (strClimateArea == "0")//NORTHERN CLIMATE AREA
                {//*4 NORTH
                    intWZModifier = 1;
                    if (iTemp <= 6)
                    {//*5
                        cboWZ2Temp.SelectedValue = (iTemp + intWZModifier).ToString();
                    }//**5
                    else
                    {//*6
                        cboWZ2Temp.SelectedValue = "7";
                    }//*6
                }//**4NORTH
                else  //SOUTHERN CLIMATE AREA
                {//*7 SOUTH
                    intWZModifier = -1;

                    if (iTemp >= 1)
                    {//*8
                        cboWZ2Temp.SelectedValue = (iTemp + intWZModifier).ToString();
                    }//**8
                    else
                    {//*9
                        cboWZ2Temp.SelectedValue = "0";
                    }//**9
                }//**7
                weatherzone2.Attribute("temperature").Value = cboWZ2Temp.SelectedValue.ToString();
            }

            if (environ.Attribute("boundary2").Value.ToString() != "999")
            {
                if (strClimateArea == "0")//NORTHERN CLIMATE AREA
                {//*11
                    intWZModifier = 1;
                    if (iTemp <= 5)
                    {//*12
                        cboWZ3Temp.SelectedValue = (iTemp + (2 * intWZModifier)).ToString();
                    }//**12
                    else
                    {//*13
                        cboWZ3Temp.SelectedValue = "7";
                    }//*13
                }//**11

                else  //SOUTHERN CLIMATE AREA
                {//*14
                    intWZModifier = -1;

                    if (iTemp >= 2)
                    {//*15
                        cboWZ3Temp.SelectedValue = (iTemp + (2 * intWZModifier)).ToString();
                    }//**15
                    else
                    {//*16
                        cboWZ3Temp.SelectedValue = "0";
                    }//**16
                    weatherzone3.Attribute("temperature").Value = cboWZ3Temp.SelectedValue.ToString();
                }//**14
            }
            xelem.Save(Globals.GlobalVariables.PATH);
        }

        private void cboPrecipitation_SelectionChangeCommitted(object sender, EventArgs e)
        {
            XElement xelem = XElement.Load(Globals.GlobalVariables.PATH);
            string xpathenviron = "ENVIRONMENT";
            var environ = xelem.XPathSelectElement(xpathenviron);

            //RETRIEVE WEATHERZONE1 DATA FROM XML
            string xpathwz1 = "WEATHERZONES/ZONE[@ID=1]";
            var weatherzone1 = xelem.XPathSelectElement(xpathwz1);

            //RETRIEVE WEATHERZONE2 DATA FROM XML
            string xpathwz2 = "WEATHERZONES/ZONE[@ID=2]";
            var weatherzone2 = xelem.XPathSelectElement(xpathwz2);

            //RETRIEVE WEATHERZONE3 DATA FROM XML
            string xpathwz3 = "WEATHERZONES/ZONE[@ID=3]";
            var weatherzone3 = xelem.XPathSelectElement(xpathwz3);

            //AS DEFAULT, SET PRECIPATION IN ALL WEATHER ZONES TO THE SAME
            //cboWZ1Precip.SelectedValue = cboPrecipitation.SelectedValue;
            cboWZ2Precip.SelectedValue = cboPrecipitation.SelectedValue;
            cboWZ3Precip.SelectedValue = cboPrecipitation.SelectedValue;

            environ.Attribute("precipitation").Value = cboPrecipitation.SelectedValue.ToString();
            weatherzone1.Attribute("precipitation").Value = cboPrecipitation.SelectedValue.ToString();
            weatherzone2.Attribute("precipitation").Value = cboPrecipitation.SelectedValue.ToString();
            weatherzone3.Attribute("precipitation").Value = cboPrecipitation.SelectedValue.ToString();

            xelem.Save(Globals.GlobalVariables.PATH);
        }

        private void cboWZ1Vis_SelectionChangeCommitted(object sender, EventArgs e)
        {
            XElement xelem = XElement.Load(Globals.GlobalVariables.PATH);
            string xpathenviron = "ENVIRONMENT";
            var environ = xelem.XPathSelectElement(xpathenviron);

            //RETRIEVE WEATHERZONE1 DATA FROM XML
            string xpathwz1 = "WEATHERZONES/ZONE[@ID=1]";
            var weatherzone1 = xelem.XPathSelectElement(xpathwz1);

            //RETRIEVE WEATHERZONE2 DATA FROM XML
            string xpathwz2 = "WEATHERZONES/ZONE[@ID=2]";
            var weatherzone2 = xelem.XPathSelectElement(xpathwz2);

            //RETRIEVE WEATHERZONE3 DATA FROM XML
            string xpathwz3 = "WEATHERZONES/ZONE[@ID=3]";
            var weatherzone3 = xelem.XPathSelectElement(xpathwz3);

            //AS DEFAULT, SET VIZ IN ALL WEATHER ZONES TO THE SAME
            cboWZ2Vis.SelectedValue = cboWZ1Vis.SelectedValue;
            cboWZ3Vis.SelectedValue = cboWZ1Vis.SelectedValue;

            weatherzone1.Attribute("visibility").Value = cboWZ1Vis.SelectedValue.ToString();
            weatherzone2.Attribute("visibility").Value = cboWZ1Vis.SelectedValue.ToString();
            weatherzone3.Attribute("visibility").Value = cboWZ1Vis.SelectedValue.ToString();

            xelem.Save(Globals.GlobalVariables.PATH);
        }

        private void cboWZ2Precip_SelectionChangeCommitted(object sender, EventArgs e)
        {
            XElement xelem = XElement.Load(Globals.GlobalVariables.PATH);
            string xpathenviron = "ENVIRONMENT";
            var environ = xelem.XPathSelectElement(xpathenviron);

            //RETRIEVE WEATHERZONE2 DATA FROM XML
            string xpathwz2 = "WEATHERZONES/ZONE[@ID=2]";
            var weatherzone2 = xelem.XPathSelectElement(xpathwz2);

            weatherzone2.Attribute("precipitation").Value = cboWZ2Precip.SelectedValue.ToString();

            xelem.Save(Globals.GlobalVariables.PATH);
        }

        private void cboWZ3Precip_SelectionChangeCommitted(object sender, EventArgs e)
        {
            XElement xelem = XElement.Load(Globals.GlobalVariables.PATH);
            string xpathenviron = "ENVIRONMENT";
            var environ = xelem.XPathSelectElement(xpathenviron);

            //RETRIEVE WEATHERZONE3 DATA FROM XML
            string xpathwz3 = "WEATHERZONES/ZONE[@ID=3]";
            var weatherzone3 = xelem.XPathSelectElement(xpathwz3);

            weatherzone3.Attribute("precipitation").Value = cboWZ3Precip.SelectedValue.ToString();

            xelem.Save(Globals.GlobalVariables.PATH);
        }

        private void cboWZ2Temp_SelectionChangeCommitted(object sender, EventArgs e)
        {
            XElement xelem = XElement.Load(Globals.GlobalVariables.PATH);
            string xpathenviron = "ENVIRONMENT";
            var environ = xelem.XPathSelectElement(xpathenviron);

            //RETRIEVE WEATHERZONE2 DATA FROM XML
            string xpathwz2 = "WEATHERZONES/ZONE[@ID=2]";
            var weatherzone2 = xelem.XPathSelectElement(xpathwz2);

            weatherzone2.Attribute("temperature").Value = cboWZ2Temp.SelectedValue.ToString();

            xelem.Save(Globals.GlobalVariables.PATH);
        }

        private void cboWZ3Temp_SelectionChangeCommitted(object sender, EventArgs e)
        {
            XElement xelem = XElement.Load(Globals.GlobalVariables.PATH);
            string xpathenviron = "ENVIRONMENT";
            var environ = xelem.XPathSelectElement(xpathenviron);

            //RETRIEVE WEATHERZONE2 DATA FROM XML
            string xpathwz3 = "WEATHERZONES/ZONE[@ID=3]";
            var weatherzone3 = xelem.XPathSelectElement(xpathwz3);

            weatherzone3.Attribute("temperature").Value = cboWZ3Temp.SelectedValue.ToString();

            xelem.Save(Globals.GlobalVariables.PATH);
        }

        private void cboWZ2Vis_SelectionChangeCommitted(object sender, EventArgs e)
        {
            XElement xelem = XElement.Load(Globals.GlobalVariables.PATH);
            string xpathenviron = "ENVIRONMENT";
            var environ = xelem.XPathSelectElement(xpathenviron);

            //RETRIEVE WEATHERZONE2 DATA FROM XML
            string xpathwz2 = "WEATHERZONES/ZONE[@ID=2]";
            var weatherzone2 = xelem.XPathSelectElement(xpathwz2);

            weatherzone2.Attribute("visibility").Value = cboWZ2Vis.SelectedValue.ToString();

            xelem.Save(Globals.GlobalVariables.PATH);
        }

        private void cboWZ3Vis_SelectionChangeCommitted(object sender, EventArgs e)
        {
            XElement xelem = XElement.Load(Globals.GlobalVariables.PATH);
            string xpathenviron = "ENVIRONMENT";
            var environ = xelem.XPathSelectElement(xpathenviron);

            //RETRIEVE WEATHERZONE3 DATA FROM XML
            string xpathwz3 = "WEATHERZONES/ZONE[@ID=3]";
            var weatherzone3 = xelem.XPathSelectElement(xpathwz3);

            weatherzone3.Attribute("visibility").Value = cboWZ3Vis.SelectedValue.ToString();

            xelem.Save(Globals.GlobalVariables.PATH);
        }

        private void btnCloseEnviron_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSaveEnviron_Click(object sender, EventArgs e)
        {
            //RETRIEVE CALENDAR DATA FROM XML
            string xpath = "CALENDAR";
            XElement xelem = XElement.Load(Globals.GlobalVariables.PATH);
            var calendar = xelem.XPathSelectElement(xpath);

            //RETRIEVE ENVIRONMENT DATA FROM XML
            string xpath2 = "ENVIRONMENT";
            var environment = xelem.XPathSelectElement(xpath2);

            calendar.Attribute("turnLength").Value = cboTurnLength.SelectedValue.ToString();
            calendar.Attribute("startHour").Value = cboStartHour.SelectedValue.ToString();
            calendar.Attribute("startDay").Value = cboStartDay.SelectedValue.ToString();
            calendar.Attribute("startMonth").Value = cboStartMonth.SelectedValue.ToString();
            calendar.Attribute("startYear").Value = (Convert.ToInt32(txtStartYear.Text) - 1).ToString();
            calendar.Attribute("currentTurn").Value = (Convert.ToInt32(txtCurrentTurn.Text) - 1).ToString();
            calendar.Attribute("finalTurn").Value = (Convert.ToInt32(txtLastTurn.Text) - 1).ToString();

            environment.Attribute("scale").Value = cboMapScale.SelectedValue.ToString();
            environment.Attribute("zone").Value = cboClimateArea.SelectedValue.ToString();
            environment.Attribute("precipitation").Value = cboPrecipitation.SelectedValue.ToString();
            environment.Attribute("temperature").Value = cboTemperature.SelectedValue.ToString();

            xelem.Save(Globals.GlobalVariables.PATH);
        }

        private void cboStartDay_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string xpath = "CALENDAR";
            XElement xelem = XElement.Load(Globals.GlobalVariables.PATH);
            var calendar = xelem.XPathSelectElement(xpath);
            calendar.Attribute("startDay").Value = cboStartDay.SelectedValue.ToString();
            xelem.Save(Globals.GlobalVariables.PATH);
        }

        private void txtStartYear_TextChanged(object sender, EventArgs e)
        {
            //EMPTY TEXTBOX
            if (string.IsNullOrEmpty(txtStartYear.Text))
            {
                btnSaveEnviron.Enabled = false;
                return;
            }
            //NON-NUMERIC INPUT
            if (!int.TryParse(txtStartYear.Text, out var intValue))
            {
                errorProvider1.SetError(txtStartYear, "Please enter a year between 1750 and 2100.");
                btnSaveEnviron.Enabled = false;
                return;
            }
            else  //NUMERIC INPUT, CHECKS INPUT RANGE
            {
                //INPUT RANGE DEPENDS ON SPECIFIC TRIGGERS
                if (intValue >= 1750 && (intValue <= 2100))
                {
                    errorProvider1.SetError(txtStartYear, "");
                    btnSaveEnviron.Enabled = true;
                }
                else
                {
                    errorProvider1.SetError(txtStartYear, "Please enter a year between 1750 and 2100.");
                    btnSaveEnviron.Enabled = false;
                }
            }
        }

        private void txtLastTurn_TextChanged(object sender, EventArgs e)
        {
            //EMPTY TEXTBOX
            if (string.IsNullOrEmpty(txtLastTurn.Text))
            {
                btnSaveEnviron.Enabled = false;
                return;
            }
            //NON-NUMERIC INPUT
            if (!int.TryParse(txtLastTurn.Text, out var intValue))
            {
                errorProvider1.SetError(txtLastTurn, "Please enter a number of turns between 2 and 999.");
                btnSaveEnviron.Enabled = false;
                return;
            }
            else  //NUMERIC INPUT, CHECKS INPUT RANGE
            {
                if (intValue >= 2 && (intValue <= 999)) //
                {
                    errorProvider1.SetError(txtLastTurn, "");
                    btnSaveEnviron.Enabled = true;
                }
                else
                {
                    errorProvider1.SetError(txtLastTurn, "Please enter a number of turns between 2 and 999.");
                    btnSaveEnviron.Enabled = false;
                }
            }
        }

        private void txtCurrentTurn_TextChanged(object sender, EventArgs e)
        {
            //EMPTY TEXTBOX
            if (string.IsNullOrEmpty(txtCurrentTurn.Text))
            {
                btnSaveEnviron.Enabled = false;
                return;
            }
            //NON-NUMERIC INPUT
            if (!int.TryParse(txtCurrentTurn.Text, out var intValue))
            {
                errorProvider1.SetError(txtCurrentTurn, "Please enter a turn not higher than the last turn.");
                btnSaveEnviron.Enabled = false;
                return;
            }
            else  //NUMERIC INPUT, CHECKS INPUT RANGE
            {
                string strCurrentturn = txtCurrentTurn.Text;
                //int x = int.Parse(strCurrentturn);
                int x = Convert.ToInt32(strCurrentturn);

                if (x >= 1 && x <= (Convert.ToInt32(txtLastTurn.Text)))
                {
                    errorProvider1.SetError(txtCurrentTurn, "");
                    btnSaveEnviron.Enabled = true;
                }
                else
                {
                    errorProvider1.SetError(txtCurrentTurn, "Please enter a turn not higher than the last turn.");
                    btnSaveEnviron.Enabled = false;
                }
            }
        }
    }
}
