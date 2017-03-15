﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace JH.ACU.Lib
{
    /// <summary>
    /// 日志帮助类
    /// </summary>
    public static class LogHelper
    {
        //TODO:获取调用方法的参数的值
        private static readonly string RootPath = AppDomain.CurrentDomain.BaseDirectory + "Log";

        /// <summary>
        /// 创建日志文件夹
        /// </summary>
        /// <returns></returns>
        private static void CreateLog_Directory(string path, string strfile)
        {
            try
            {
                if (!Directory.Exists(path))
                {
                    //创建日志文件夹
                    Directory.CreateDirectory(path);
                }
                if (!File.Exists(path + strfile))
                {
                    //创建日志文件夹
                    File.Create(path + strfile).Dispose();
                }
            }
            catch(Exception ex)
            {
                //QUES:待确认
                MessageBoxHelper.ShowError(ex.Message);
            }

        }

        public static void WriteErrorLog(string fileName, Exception ex, string description = null)
        {
            var path = RootPath;
            var strFile = "\\ErrorLog_" + fileName + DateTime.Now.ToString("yyyyMMdd") + ".log";
            CreateLog_Directory(path, strFile);
            using (var sw = new StreamWriter(path + strFile, true, Encoding.Default))
            {
                sw.WriteLine("*****************************************【"
                             + DateTime.Now
                             + "】*****************************************");
                if (ex != null)
                {
                    //sw.WriteLine("【FunctionName】" + functionName);
                    sw.WriteLine("【ErrorType】" + ex.GetType());
                    sw.WriteLine("【TargetSite】" + ex.TargetSite);
                    sw.WriteLine("【Message】" + ex.Message);
                    sw.WriteLine("【Source】" + ex.Source);
                    sw.WriteLine("【StackTrace】" + ex.StackTrace);
                    if (!description.IsNullOrEmpty())
                    {
                        sw.WriteLine("【Extras】" + description);
                    }
                }
                else
                {
                    sw.WriteLine("Exception is NULL");
                }
                sw.WriteLine();
            }
        }

        public static void WriteWarningLog(string fileName, string message)
        {
            var path = RootPath;
            var strFile = "\\WarningLog_" + fileName + DateTime.Now.ToString("yyyyMMdd") + ".log";
            CreateLog_Directory(path, strFile);
            using (var sw = new StreamWriter(path + strFile, true, Encoding.Default))
            {
                sw.WriteLine("*****************************************【"
                             + DateTime.Now
                             + "】*****************************************");
                //sw.WriteLine("【FunctionName】" + functionName);
                sw.WriteLine("【WraningMessage】" + message);
                sw.WriteLine();
            }
        }

    }
}