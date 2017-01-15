﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JH.ACU.Model;
using JH.ACU.Tool;

namespace JH.ACU.DAL
{
    public static class DalConfig
    {
        private static readonly string SettingFileName = Environment.CurrentDirectory +
                                                 "\\InstrumentConfig\\InstrConfig.xml";

        public static void CreateInstrConfig(InstrConfig instrConfig)
        {
            XmlHelper.XmlSerializeToFile(instrConfig, SettingFileName, Encoding.UTF8);
        }

        public static List<Instr> GetInstrConfigs()
        {
            return XmlHelper.XmlDeserializeFromFile<InstrConfig>(SettingFileName, Encoding.UTF8);
        }

        public static Instr GetInstrConfig(InstrName name)
        {
            return GetInstrConfigs().Find(i => i.Name == name.ToString());
        }
    }
}
