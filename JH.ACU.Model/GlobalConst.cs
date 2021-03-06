﻿using System.Collections.Generic;

namespace JH.ACU.Model
{
    /// <summary>
    /// 常量参数
    /// </summary>
    public struct GlobalConst
    {
        static GlobalConst()
        {
        }

        /// <summary>
        /// 获取波特率集合
        /// </summary>
        public static int[] BaudRate
        {
            get { return new[] {300, 600, 1200, 9600, 10400}; }
        }

        /// <summary>
        /// 获取数据位集合
        /// </summary>
        public static short[] DataBits
        {
            get { return new short[] {7, 8}; }
        }

        /// <summary>
        /// 获取仪器字典
        /// </summary>
        public static Dictionary<string, InstrName> InstrNameString
        {
            get
            {
                return new Dictionary<string, InstrName>
                {
                    //{"----请选择----", (InstrName)0},
                    {"ACU", InstrName.Acu},
                    {"数字万用表", InstrName.Dmm},
                    {"电阻箱#1", InstrName.Prs0},
                    {"电阻箱#2", InstrName.Prs1},
                    {"程控电源", InstrName.Pwr},
                    {"温箱", InstrName.Chamber},
                };
            }
        }
    }

    /// <summary>
    /// 仪器名称枚举
    /// </summary>
    public enum InstrName
    {
        //None,
        Acu,

        /// <summary>
        /// 程控电源
        /// </summary>
        Pwr,

        /// <summary>
        /// 程控电阻箱0
        /// </summary>
        Prs0,

        /// <summary>
        /// 程控电阻箱1
        /// </summary>
        Prs1,

        /// <summary>
        /// 数字万用表
        /// </summary>
        Dmm,

        /// <summary>
        /// 温箱
        /// </summary>
        Chamber,

        /// <summary>
        /// 数据采集卡
        /// </summary>
        Daq,
    }

    /// <summary>
    /// 仪器类型枚举
    /// </summary>
    public enum InstrType
    {
        Gpib,
        Serial,
        Tcp,
    }

    /// <summary>
    /// 二分法查找结果枚举
    /// </summary>
    public enum FindResult
    {
        Error,
        InBetween,
        UnderMin,
        AboveMax,
    }

    /// <summary>
    /// 测试结果
    /// </summary>
    public enum TestResult
    {
        Passed = 0,
        Failed = -1,
        Cancelled = -2,
    }

    /// <summary>
    /// 回路测试时模式枚举,注意顺序
    /// 与测试规范顺序相同（SPEC_unit.txt）
    /// </summary>
    public enum SquibMode
    {
        /// <summary>
        /// Squib Resistance Too High
        /// </summary>
        TooHigh = 1,

        /// <summary>
        /// Squib Resistance Too Low
        /// </summary>
        TooLow = 2,

        /// <summary>
        /// Squib Short to Ground
        /// </summary>
        ToGround = 3,

        /// <summary>
        /// Squib Short to Battery
        /// </summary>
        ToBattery = 4
    }

    /// <summary>
    /// Belt测试时模式枚举,注意顺序
    /// 与测试规范顺序相同(SPEC_unit.txt)
    /// </summary>
    public enum BeltMode
    {
        UnbuckledOrDisabled = 1,
        BuckledOrEnabled = 2,
        ToGround = 3,
        ToBattery = 4,
    }

    /// <summary>
    /// SIS测试时模式枚举,注意顺序
    /// 与测试规范顺序相同(SPEC_unit.txt)
    /// </summary>
    public enum SisMode
    {
        ToBattery = 1,
        ToGround = 2,
    }

    /// <summary>
    /// Volt测试时模式枚举,注意顺序
    /// 与测试规范顺序相同(SPEC_unit.txt)
    /// </summary>
    public enum BatteryMode
    {
        TooHigh=1,
        TooLow=2,
    }
}