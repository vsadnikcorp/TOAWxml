using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Xml.XPath;

namespace TOAWXML
{
    public class Renumbering
    {

        public Renumbering()
        {
           
        }

        public static bool RenumberEventFormations(IEnumerable<XElement> formations, string formationid, string forceid)
        {
            int count = formations.Count();
            int deleted = Convert.ToInt32(formationid);
            int length = formationid.Length;
            int unitdigits = formationid.Length;
            int zeroes = 3 - unitdigits;
            //string zerostring = "";
            bool proceed = true;

            if (forceid == "1")
            {
                foreach (XElement formation in formations)
                {
                    string eventform = formation.Attribute("VALUE").Value;

                    if (eventform.Length <4)
                    {
                        //FORMATION DELETED IS PRIOR TO FORMATION IN EVENT--EVENT FORMATION REDUCED BY ONE
                        if (Convert.ToInt32(eventform) > (deleted - 1))
                        {
                            formation.Attribute("VALUE").Value = (Convert.ToInt32(eventform) - 1).ToString();
                        }

                        //FORMATION IN EVENT IS DELETED
                        if (Convert.ToInt32(eventform) == (deleted - 1))
                        {

                            string eventid = formation.Attribute("ID").Value;
                            if (MessageBox.Show("You are deleting a formation used in Event " + eventid + "." + Environment.NewLine +
                                    "If you proceed, the Event's Effect will be changed to 'No effect'." + Environment.NewLine + Environment.NewLine +
                                    "Proceed?",
                                "Deletion of Formation Used In Event",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Exclamation,
                                MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                                {
                                    formation.Attribute("EFFECT").Value = "No effect";
                                    formation.Attribute("NEWS").Value = "";
                                }
                             else
                            {
                                proceed = false;
                                return proceed;
                            }
                        }
                    }
                }
            }

           if (forceid == "2")
            {
                foreach (XElement formation in formations)
                {

                    int eventform = Convert.ToInt32(formation.Attribute("VALUE").Value);
                    int eventformid = 2000 + deleted;

                    if (eventform >= 2000)
                    {
                        //FORMATION DELETED IS PRIOR TO FORMATION IN EVENT--EVENT FORMATION REDUCED BY ONE
                        //if (Convert.ToInt32(eventform) > (eventform2 - 1))
                        if (eventform > (eventformid - 1))
                        {
                            formation.Attribute("VALUE").Value = (eventform - 1).ToString();
                            //proceed = true;
                        }

                        //FORMATION IN EVENT IS DELETED
                        //if (Convert.ToInt32(eventform) == (eventform2 - 1))
                        if (eventform == (eventformid - 1))
                        {
                            string eventid = formation.Attribute("ID").Value;
                            if (MessageBox.Show("You are deleting a formation used in Event " + eventid + "."  + Environment.NewLine + 
                                    "If you proceed, the Event's Effect will be changed to 'No effect'." + Environment.NewLine + Environment.NewLine +
                                    "Proceed?",
                                "Deletion of Formation Used In Event",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Exclamation,
                                MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                                    {
                                    formation.Attribute("EFFECT").Value = "No effect";
                                    formation.Attribute("NEWS").Value = "";
                                    }
                            else
                            {
                                proceed = false;
                                return proceed;
                            }
                        }
                    }
                }
            }
            return proceed;
        }
        
        public static bool RenumberEventFormationUnits(IEnumerable<XElement> subunits, IEnumerable<XElement> unitevents, string formationid, string forceid)
        {
            int delta = subunits.Count();
            int min = Convert.ToInt32(subunits.First().Attribute("ID").Value);
            int max = Convert.ToInt32(subunits.Last().Attribute("ID").Value);
            int eventunitid = 0;
            string eventid = "";
            bool proceed2 = true;

            if (forceid == "1")
            {
                foreach (XElement u in unitevents)
                {
                    eventunitid = (Convert.ToInt32(u.Attribute("VALUE").Value) + 1); //MUST ADD 1 TO CONVERT BETWEEN NORMAL AND EVENT IDs
                    eventid = u.Attribute("ID").Value;

                    if (eventunitid < 10000)
                    {
                        switch (eventunitid)
                        {
                            case int n when eventunitid < min:
                                //NOTHING SHOULD HAPPEN
                                proceed2 = true;
                                break;

                            case int n when eventunitid > max:
                                u.Attribute("VALUE").Value = ((eventunitid - delta - 1).ToString()); //MUST SUBTRACT 1 TO CONVERT BETWEEN EVENT AND NORMAL IDs
                                proceed2 = true;
                                break;

                            default:
                                foreach (XElement su in subunits)
                                {
                                    int uid = Convert.ToInt32(su.Attribute("ID").Value);
                                    if (eventunitid == uid)
                                    {
                                        if (MessageBox.Show("You are deleting a formation with a unit used in Event " + eventid + "." + Environment.NewLine +
                                            "If you proceed, the Event's Trigger/Effect will be changed to 'No trigger'/'No effect'." + Environment.NewLine + Environment.NewLine +
                                            "Proceed?",
                                        "Deletion of Unit Used In Event",
                                        MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Exclamation,
                                        MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                                        {
                                            //string eventid = formation.Attribute("ID").Value;
                                            SetFullAttributes(u);
                                            u.Attribute("TRIGGER").Value = "No trigger";
                                            u.Attribute("EFFECT").Value = "No effect";
                                            u.Attribute("VALUE").Value = "0";
                                            u.Attribute("NEWS").Value = "";
                                            proceed2 = true;
                                        }
                                        else
                                        {
                                            proceed2 = false;
                                            return proceed2;
                                        }
                                    }
                                }
                                break;
                        }
                    }
                }
            }

            if (forceid == "2")
            {
                foreach (XElement u in unitevents)
                {
                    eventunitid = (Convert.ToInt32(u.Attribute("VALUE").Value) + 1); //MUST ADD 1 TO CONVERT BETWEEN NORMAL AND EVENT IDs
                    eventid = u.Attribute("ID").Value;

                    if (eventunitid >= 10000)
                    {
                        switch (eventunitid)
                        {
                            case int n when (eventunitid-10000) < min:
                                //NOTHING SHOULD HAPPEN
                                proceed2 = true;
                                break;

                            case int n when (eventunitid - 10000) > max:
                                u.Attribute("VALUE").Value = ((eventunitid - delta - 1).ToString()); //MUST SUBTRACT 1 TO CONVERT BETWEEN EVENT AND NORMAL IDs
                                proceed2 = true;
                                break;

                            default:
                                foreach (XElement su in subunits)
                                {
                                    int uid = Convert.ToInt32(su.Attribute("ID").Value);
                                    if ((eventunitid-10000) == uid)
                                    {
                                        if (MessageBox.Show("You are deleting a formation with a unit used in Event " + eventid + "." + Environment.NewLine +
                                            "If you proceed, the Event's Trigger/Effect will be changed to 'No trigger'/'No effect'." + Environment.NewLine + Environment.NewLine +
                                            "Proceed?",
                                        "Deletion of Unit Used In Event",
                                        MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Exclamation,
                                        MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                                        {
                                            SetFullAttributes(u);
                                            u.Attribute("TRIGGER").Value = "No trigger";
                                            u.Attribute("EFFECT").Value = "No effect";
                                            u.Attribute("VALUE").Value = "0";
                                            u.Attribute("NEWS").Value = "";
                                            proceed2 = true;
                                        }
                                        else
                                        {
                                            proceed2 = false;
                                            return proceed2;
                                        }
                                    }
                                }
                                break;
                        }
                    }
                }
            }

            return proceed2;
        }

        public static bool RenumberEventUnits(IEnumerable<XElement> unitevents, string unitid, string forceid)
        {
            int eventunitid = 0;
            int unitidint = Convert.ToInt32(unitid);
            string eventid = "";
            bool proceed = true;

            if (forceid == "1")
            {
                foreach (XElement u in unitevents)
                {
                    eventunitid = (Convert.ToInt32(u.Attribute("VALUE").Value) + 1); //MUST ADD 1 TO CONVERT BETWEEN NORMAL AND EVENT IDs
                    eventid = u.Attribute("ID").Value;

                    if (eventunitid < 10000)
                    {
                        switch (eventunitid)
                        {
                            case int n when eventunitid < unitidint:
                                //NOTHING SHOULD HAPPEN
                                proceed = true;
                                break;

                            case int n when eventunitid > unitidint:
                                u.Attribute("VALUE").Value = ((eventunitid - 2).ToString()); //MUST SUBTRACT 1 TO CONVERT BETWEEN EVENT AND NORMAL IDs
                                proceed = true;
                                break;

                            case int n when eventunitid == unitidint:
                                    if (eventunitid == unitidint)
                                    {
                                        if (MessageBox.Show("You are deleting a unit used in Event " + eventid + "." + Environment.NewLine +
                                            "If you proceed, the Event's Trigger/Effect will be changed to 'No trigger'/'No effect'." + Environment.NewLine + Environment.NewLine +
                                            "Proceed?",
                                        "Deletion of Unit Used In Event",
                                        MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Exclamation,
                                        MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                                        {
                                            SetFullAttributes(u);
                                            u.Attribute("TRIGGER").Value = "No trigger";
                                            u.Attribute("EFFECT").Value = "No effect";
                                            u.Attribute("VALUE").Value = "0";
                                            u.Attribute("NEWS").Value = "";
                                            proceed = true;
                                        }
                                        else
                                        {
                                            proceed = false;
                                            return proceed;
                                        }
                                    }
                                    break;
                        }
                    }
                }
            }

            if (forceid == "2")
            {
                foreach (XElement u in unitevents)
                {
                    eventunitid = (Convert.ToInt32(u.Attribute("VALUE").Value) + 1); //MUST ADD 1 TO CONVERT BETWEEN NORMAL AND EVENT IDs
                    eventid = u.Attribute("ID").Value;

                    if (eventunitid >= 10000)
                    {
                        switch (eventunitid)
                        {
                            case int n when (eventunitid - 10000) < unitidint:
                                //NOTHING SHOULD HAPPEN
                                proceed = true;
                                break;

                            case int n when (eventunitid - 10000) > unitidint:
                                u.Attribute("VALUE").Value = ((eventunitid - 2).ToString()); //MUST SUBTRACT 1 TO CONVERT BETWEEN EVENT AND NORMAL IDs
                                proceed = true;
                                break;

                            default:
                                    //int uid = Convert.ToInt32(su.Attribute("ID").Value);
                                    if ((eventunitid - 10000) == unitidint)
                                    {
                                        if (MessageBox.Show("You are deleting a unit used in Event " + eventid + "." + Environment.NewLine +
                                            "If you proceed, the Event's Trigger/Effect will be changed to 'No trigger'/'No effect'." + Environment.NewLine + Environment.NewLine +
                                            "Proceed?",
                                        "Deletion of Unit Used In Event",
                                        MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Exclamation,
                                        MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                                        {
                                        //string eventid = formation.Attribute("ID").Value;
                                            SetFullAttributes(u);
                                            u.Attribute("TRIGGER").Value = "No trigger";
                                            u.Attribute("EFFECT").Value = "No effect";
                                            u.Attribute("VALUE").Value = "0";
                                            u.Attribute("NEWS").Value = "";
                                            proceed = true;
                                        }
                                        else
                                        {
                                            proceed = false;
                                            return proceed;
                                        }
                                    }
                                //}
                                break;
                        }
                    }
                }
            }

            return proceed;
        }

        public static void RenumberAll(IEnumerable<XElement>units)
        {
            //RENUMBER ALL FORMATIONS/UNITS IF ONE IS REMOVED
            int i = 0;
           
            foreach (XElement sib in units)
            {
                i = i + 1;
                sib.Attribute("ID").Value = i.ToString();
            }
        }

        public static void SetFullAttributes(XElement u)
        {
            if (u.Attribute("CONTINGENCY") == null) u.Add(new XAttribute("CONTINGENCY", "1"));
            if (u.Attribute("X") == null) u.Add(new XAttribute("X", "1"));
            if (u.Attribute("Y") == null) u.Add(new XAttribute("Y", "1"));
            if (u.Attribute("TURN") == null) u.Add(new XAttribute("TURN", "0"));
            if (u.Attribute("VARIABLE") == null) u.Add(new XAttribute("VARIABLE", "1"));
            if (u.Attribute("CHANCE") == null) u.Add(new XAttribute("CHANCE", "100"));
            if (u.Attribute("VALUE") == null) u.Add(new XAttribute("VALUE", "1"));
            if (u.Attribute("NEWS") == null) u.Add(new XAttribute("NEWS", ""));
        }

        public static void RenumberDividedUnit(IEnumerable<XElement> unitparents, string unitid)
        {
            foreach (XElement parent in unitparents)
            {
                int deletedid = Convert.ToInt32(unitid);
                int parentid = Convert.ToInt32(parent.Attribute("PARENT").Value);

                if (deletedid < parentid)
                {
                    parentid = parentid - 1;
                    parent.Attribute("PARENT").Value = parentid.ToString();

                    Console.WriteLine(parentid);
                }
            }
        }

        public static void RenumberDividedFormation(IEnumerable<XElement> subunits, IEnumerable<XElement> unitparents)
        {
            int delta = subunits.Count();
            int min = Convert.ToInt32(subunits.First().Attribute("ID").Value);
            int max = Convert.ToInt32(subunits.Last().Attribute("ID").Value);
            

            foreach (XElement parent in unitparents)
            {
                int unitparentid = (Convert.ToInt32(parent.Attribute("PARENT").Value));

                if(unitparentid > max)
                {
                    parent.Attribute("PARENT").Value = (unitparentid - delta).ToString();
                }

            }
        }

    }
}
