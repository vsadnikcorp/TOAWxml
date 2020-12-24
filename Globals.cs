using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Xml;

namespace TOAWXML
{
    class Globals
    {
        public static class GlobalVariables
        {
            public static string PATH { get; set; }
            public static string EQPPATH { get; set; }
            public static string MICROICON { get; set; }
            public static bool TREEVIEWCHANGED { get; set; }
            public static string DRAGGEDTAG { get; set; }
            public static string TARGETTAG { get; set; }
            public static string DRAGGEDPARENTID { get; set; }
            public static string FORCE { get; set; }
            public static string PROFIC { get; set; }
            public static string SUPPLY { get; set; }
            public static string LOSSTOL { get; set; }
            public static string SUPPSCOPE { get; set; }
            public static string ORDERS { get; set; }
            public static string READINESS { get; set; }
            public static string DEPLOY { get; set; }
            public static string REPLACE { get; set; }
            public static string EXPERIENCE { get; set; }
            public static string ID { get; set; }
            public static string PARENTID { get; set; }
            public static string COPIEDID { get; set; }
            public static string COPIEDPARENTID { get; set; }
            public static string UNITDIVIDER { get; set; }
            public static int PREVCBODEPLOYINDEX { get; set; }
            public static string EVENTID { get; set; }
            public static string ICONCOLOR { get; set; }
        }

        [Flags]
        public enum FLAG0 : int
        {
            None = 0,
            Categories = 1,
            Armored = 2,
            ActiveDefend = 4,
            Recon = 8,
            Static = 16,
            Engineer = 32,
            HorseMove = 64,
            Fixed = 128
        }

        [Flags]
        public enum FLAG1 : int
        {
            None = 0,
            Transport = 1,
            Slow = 2,
            Motorized = 4,
            HelicopterMove = 8,
            HiAltAA = 16,
            LongRange = 32,
            LightNaval = 64,
            MediumNaval = 128
        }

        public enum FLAG2 : int
        {
            None = 0,
            HeavyNaval = 1,
            CarrierNaval = 2,
            LowAltAir = 4,
            HighAltAir = 8,
            Amphib = 16,
            NavalAir = 32,
            Riverine = 64,
            HiLowAltAA = 128
        }

        public enum FLAG3 : int
        {
            None = 0,
            AllWeather = 1,
            AntiShip = 2,
            FastHorse = 4,
            MajorFord = 8,
            Railbound = 16,
            AntiShipOnly = 32,
            SlowMotorized = 64,
            FastMotorized = 128
        }

        public enum FLAG4 : int
        {
            None = 0,
            CompositeArm = 1,
            LaminateArm = 2,
            NBC = 4,
            Nuclear = 8,
            Kinetic = 16,
            PGW = 32,
            InFlightRefuel = 64,
            Lightweight = 128
        }

        public enum FLAG5 : int
        {
            None = 0,
            Airborne = 1,
            Optics1 = 2,
            Optics2 = 4,
            Optics3 = 8,
            Optics4 = 16,
            Support = 32,
            Command = 64,
            Smoke = 128
        }

        public enum FLAG6 : int
        {
            None = 0,
            Reactive = 1,
            Police = 2,
            LtTransHelo = 4,
            MedTransHelo = 8,
            HvyTransHelo = 16,
            Agile = 32,
            Roadbound = 64,
            ExtendedRange = 128
        }

        public enum FLAG7 : int
        {
            None = 0,
            Standoff = 1,
            ShockCav = 2,
            RailRepair = 4,
            Infantry = 8,
            PoorGeom = 16,
            FairGeom = 32,
            DualPurpose = 64,
            TorpedoBomb = 128
        }

    }
       
}
