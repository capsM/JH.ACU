﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using JH.ACU.Lib;
using JH.ACU.Model;
using JH.ACU.Model.Config.InstrumentConfig;
using JH.ACU.Model.Config.TestConfig;
using NationalInstruments.Visa;

namespace JH.ACU.BLL.Config
{
    /// <summary>
    /// 仪器配置类
    /// </summary>
    public static class BllConfig
    {
        #region InstrConfig

        private static readonly string SettingFileName = Environment.CurrentDirectory + "\\Config\\InstrConfig.xml";

        #region Visa Resource Converter

        /// <summary>
        /// 根据仪器类型查找本机资源
        /// </summary>
        /// <param name="instrType"></param>
        public static List<string> FindResources(InstrType instrType)
        {
            var list = new List<string>();
            try
            {
                using (var rm = new ResourceManager())
                {
                    string filter;
                    switch (instrType)
                    {
                        case InstrType.Gpib:
                            filter = "GPIB?*INSTR";
                            break;
                        case InstrType.Serial:
                            filter = "ASRL?*INSTR";
                            break;
                        case InstrType.Tcp:
                            filter = "TCPIP?*SOCKET";
                            break;
                        default:
                            throw new ArgumentOutOfRangeException("instrType", instrType, null);
                    }
                    var resources = rm.Find(filter);
                    foreach (var s in resources)
                    {
                        var parseResult = rm.Parse(s);
                        //TODO：待验证GPIB地址
                        list.Add(parseResult.InterfaceNumber.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return list.Count == 0 ? null : list;
        }

        /// <summary>
        /// 将端口号转换为相应的filter
        /// </summary>
        /// <param name="instr"></param>
        /// <returns></returns>
        public static string GetPortNumber(Instr instr)
        {
            switch (instr.Type)
            {
                case InstrType.Gpib:
                    return string.Format("GPIB0::{0}::INSTR", instr.Gpib.Port);
                case InstrType.Serial:
                    return string.Format("ASRL{0}::INSTR", instr.Serial.Port);
                case InstrType.Tcp:
                    return string.Format("TCPIP0::{0}::{1}::SOCKET", instr.TcpIp.IpAddress, instr.TcpIp.Port);
                default:
                    throw new ArgumentNullException("instr", string.Format("端口类型设置无效,错误值为：{0}", instr.Type));
            }
        }


        #endregion

        /// <summary>
        /// 获取所有配置
        /// </summary>
        /// <returns></returns>
        public static List<Instr> GetInstrConfigs()
        {
            return XmlHelper.XmlDeserializeFromFile<InstrConfig>(SettingFileName, Encoding.UTF8);
        }

        /// <summary>
        /// 保存配置
        /// </summary>
        /// <param name="instr"></param>
        /// <param name="instrs"></param>
        public static void SaveInstr(Instr instr, List<Instr> instrs = null)
        {
            var list = instrs ?? GetInstrConfigs();
            var i = list.Find(ins => ins.Name == instr.Name);
            if (i == null)
            {
                list.Add(instr);
            }
            else
            {
                list.Remove(i);
                list.Add(instr);
            }
            var resList = new InstrConfig();
            resList.AddRange(list.OrderBy(o => o.Name));
            XmlHelper.XmlSerializeToFile(resList, SettingFileName, Encoding.UTF8);
        }

        /// <summary>
        /// 获取配置
        /// </summary>
        /// <param name="name"></param>
        /// <param name="instrs">该值为Null时从文件中查找</param>
        /// <returns></returns>
        public static Instr GetInstr(InstrName name, List<Instr> instrs = null)
        {
            var list = instrs ?? GetInstrConfigs();
            var instr = list.Find(i => i.Name == name);
            return instr ?? new Instr {Name = name};
        }

        #endregion

        #region TestConditionConfig

        /// <summary>
        /// 保存测试条件至文件
        /// </summary>
        /// <param name="testCondition"></param>
        /// <param name="fileName"></param>
        public static void SaveTestCondition(TestCondition testCondition, string fileName)
        {
            if (!File.Exists(fileName)) throw new ArgumentException(string.Format("路径无效:{0}", fileName), "fileName");
            if (testCondition == null) throw new ArgumentNullException("testCondition");
            XmlHelper.XmlSerializeToFile(testCondition, fileName);
        }

        /// <summary>
        /// 获取指定路径下文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static TestCondition GetTestCondition(string fileName)
        {
            if (!File.Exists(fileName)) throw new ArgumentException(string.Format("路径无效:{0}", fileName), "fileName");
            return XmlHelper.XmlDeserializeFromFile<TestCondition>(fileName);
        }

        #endregion

    }
}
