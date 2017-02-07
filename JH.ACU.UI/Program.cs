﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using JH.ACU.Model;

namespace JH.ACU.UI
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //log4net.Config.XmlConfigurator.Configure();
            Application.Run(new MainForm());
            //Application.Run(new TestForm());
            //Application.Run(new InstrConfigForm(InstrName.Chamber));
        }
    }
}
