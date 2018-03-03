﻿using CsvHelper.Configuration;
using System.Runtime.InteropServices;

namespace GT2DataSplitter
{
    public class GenericEngineUpgrade : CarCsvDataStructure<GenericEngineUpgradeData, GenericEngineUpgradeCSVMap>
    {
        public override string CreateOutputFilename(byte[] data)
        {
            return CreateOutputFilename(Data.CarId, Data.Stage);
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)] // 0xC
    public struct GenericEngineUpgradeData
    {
        public uint CarId;
        public uint Price;
        public byte Stage;
        public byte PowerMultiplier;
        public byte PowerMultiplier2;
        public byte PowerMultiplier3;
    }

    public sealed class GenericEngineUpgradeCSVMap : ClassMap<GenericEngineUpgradeData>
    {
        public GenericEngineUpgradeCSVMap()
        {
            Map(m => m.CarId).TypeConverter(Utils.CarIdConverter);
            Map(m => m.Price);
            Map(m => m.Stage);
            Map(m => m.PowerMultiplier);
            Map(m => m.PowerMultiplier2);
            Map(m => m.PowerMultiplier3);
        }
    }
}
