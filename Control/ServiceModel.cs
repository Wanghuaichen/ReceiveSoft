﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service
{
    /// <summary>
    /// 界面列表（明文）
    /// </summary>
    class UIModel
    {
        private string _serviceid;
        private string _stcd;
        private string _explain;
        private byte[] _data;
        private int _datatype;
       
        /// <summary>
        /// 服务标示
        /// </summary>		
        public string SERVICEID
        {
            get { return _serviceid; }
            set { _serviceid = value; }
        }

        /// <summary>
        /// 测站编码
        /// </summary>		
        public string STCD
        {
            get { return _stcd; }
            set { _stcd = value; }
        }

        /// <summary>
        /// 说明
        /// </summary>
        public string EXPLAIN 
        {
            get { return _explain; }
            set { _explain = value; }
        }

        /// <summary>
        /// 接收到的数据
        /// </summary>
        public byte[] Data
        {
            get { return _data; }
            set { _data = value; }
        }

        /// <summary>
        /// 数据类型
        /// </summary>
        public int DataType
        {
            get { return _datatype; }
            set { _datatype = value; }
        }
    }

    /// <summary>
    /// 与界面交互的tcp信息配置
    /// </summary>
    class UITcpModel 
    {
        private string _ip;
        private int _port;
        /// <summary>
        /// IP
        /// </summary>		
        public string IP
        {
            get { return _ip; }
            set { _ip = value; }
        }

        /// <summary>
        /// 端口
        /// </summary>
        public int PORT
        {
            get { return _port; }
            set { _port = value; }
        }
    }

    /// <summary>
    /// 透传用数据报
    /// </summary>
    class DataModel 
    {
        private byte[] _data;

        /// <summary>
        /// 接收到的数据
        /// </summary>
        public byte[] Data
        {
            get { return _data; }
            set { _data = value; }
        }
    } 

   

    /// <summary>
    /// 服务列表
    /// </summary>
    public class serviceModel 
    {
        private string _servicetype;
        private string _serviceid;
        private string _ip_portname;
        private int _port_baudrate;
        private string _num;

        /// <summary>
        /// 服务类型
        /// </summary>		
        public string SERVICETYPE
        {
            get { return _servicetype; }
            set { _servicetype = value; }
        } 

        /// <summary>
        /// 服务标示
        /// </summary>		
        public string SERVICEID
        {
            get { return _serviceid; }
            set { _serviceid = value; }
        }

        /// <summary>
        /// IP或串口号
        /// </summary>		
        public string IP_PORTNAME
        {
            get { return _ip_portname; }
            set { _ip_portname = value; }
        }

        /// <summary>
        /// 端口或波特率
        /// </summary>
        public int PORT_BAUDRATE
        {
            get { return _port_baudrate; }
            set { _port_baudrate = value; }
        }

        /// <summary>
        /// 号码（卫星有卫星标识、Gsm有手机卡号）
        /// </summary>		
        public string NUM
        {
            get { return _num; }
            set { _num = value; }
        }
    }

    /// <summary>
    /// RTU基础信息列表(未使用，YY_RTU_Basic代替)
    /// </summary>
    public class RTU 
    { 
        private string _stcd;
        private string _name;
        private string _pwd;
        /// <summary>
        /// 测站编码
        /// </summary>		
        public string STCD
        {
            get { return _stcd; }
            set { _stcd = value; }
        }

        /// <summary>
        /// 测站名称
        /// </summary>		
        public string NAME
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// 密码
        /// </summary>
        public string PWD
        {
            get { return _pwd; }
            set { _pwd = value; }
        }

      
    }

    /// <summary>
    /// 召测命令表
    /// </summary>
    public class Command 
    {
        private string _stcd;
        private string _servicetype;
        private string _commandid;
        private string _data;
        private DateTime _datetime;
        private int _state;


        /// <summary>
        /// 测站编码
        /// </summary>		
        public string STCD
        {
            get { return _stcd; }
            set { _stcd = value; }
        }

        /// <summary>
        /// 服务类型 tcp/udp/gsm/com
        /// </summary>		
        public string SERVICETYPE
        {
            get { return _servicetype; }
            set { _servicetype = value; }
        }

        /// <summary>
        /// 命令标识（功能码）
        /// </summary>
        public string CommandID
        {
            get { return _commandid; }
            set { _commandid = value; }
        }

        /// <summary>
        /// 召测命令数据
        /// </summary>
        public string Data
        {
            get { return _data; }
            set { _data = value; }
        }

        /// <summary>
        /// 命令状态变化时间
        /// </summary>
        public DateTime DATETIME 
        {
            get { return _datetime; }
            set { _datetime = value; }
        }

        /// <summary>
        /// 命令状态
        /// </summary>
        public int STATE
        {
            get { return _state; }
            set { _state = value; }
        }
    }
}
