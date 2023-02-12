﻿using System.IO;

namespace GT3.DataSplitter
{
    class Program
    {
        public static IDStringTable IDStrings = new IDStringTable();
        public static StringTable Strings = new StringTable();
        public static StringTable UnicodeStrings = new StringTable();
        public static StringTable ColourStrings = new StringTable();

        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                BuildFile();
                return;
            }

            string filename = Path.GetFileName(args[0]);
            string extension = Path.GetExtension(filename);
            
            if (extension == ".db")
            {
                if (filename.Contains("_gtc"))
                {
                    SplitFileGTC();
                    return;
                }
                SplitFile();
            }
        }

        static void SplitFile()
        {
            IDStrings.Read(".id_db_idx.db", ".id_db_str.db");
            Strings.Read("paramstr.db");
            UnicodeStrings.Read("paramunistr.db");
            ColourStrings.Read("carcolor.sdb");

            var database = new ParamDB();
            database.ReadData("paramdb.db");

            var raceDetails = new RaceDetailDB();
            raceDetails.ReadData("racedetail.db");

            var raceModes = new RaceModeDB();
            raceModes.ReadData("racemode.db");

            Directory.CreateDirectory("Data");
            Directory.SetCurrentDirectory("Data");
            database.DumpData();
            raceDetails.DumpData();
            raceModes.DumpData();

            Strings.Export("Strings");
            UnicodeStrings.Export("UnicodeStrings");
            ColourStrings.Export("ColourStrings");
            IDStrings.Export();
        }

        static void BuildFile()
        {
            var database = new ParamDB();
            Directory.SetCurrentDirectory("Data");
            IDStrings.Import();
            Strings.Import("Strings");
            UnicodeStrings.Import("UnicodeStrings");
            ColourStrings.Import("ColourStrings");
            database.ImportData();
            Directory.CreateDirectory("..\\Output");
            Directory.SetCurrentDirectory("..\\Output");
            database.WriteData();
            IDStrings.Write();
            Strings.Write("paramstr.db", false);
            UnicodeStrings.Write("paramunistr.db", true);
            ColourStrings.Write("carcolor.sdb", true);
        }

        static void SplitFileGTC()
        {
            IDStrings.Read(".id_db_idx.db", ".id_db_str.db");
            Strings.Read("paramstr.db");
            UnicodeStrings.Read("paramunistr.db");
            ColourStrings.Read("carcolor.sdb");

            var database = new ParamDBConcept();
            database.ReadData("paramdb_gtc.db");

            var raceDetails = new RaceDetailDB();
            raceDetails.ReadData("racedetail.db");

            var raceModes = new RaceModeDB();
            raceModes.ReadData("racemode.db");

            Directory.CreateDirectory("Data");
            Directory.SetCurrentDirectory("Data");
            database.DumpData();
            raceDetails.DumpData();
            raceModes.DumpData();

            Strings.Export("Strings");
            UnicodeStrings.Export("UnicodeStrings");
            ColourStrings.Export("ColourStrings");
            IDStrings.Export();
        }
    }
}