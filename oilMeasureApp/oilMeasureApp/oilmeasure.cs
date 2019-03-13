using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
//using FtdAdapter;
using Modbus.Utility;
using Modbus.Data;
using Modbus.IO;
using Modbus.Message;
using Modbus.Device;
using Unme.Common;

namespace oilMeasure
{
    class Oilmodbus
    {
        private Config oConfig;
        private string tcpHost;
        private int tcpPort;
        private ushort oilAddr;
        private TcpClient client;
        private ModbusIpMaster master;
        public double oilvalue { get; set; }

        public Oilmodbus() {
            //read parameter from config.
            oConfig = new Config();
            tcpHost = oConfig.getValue("tcp-host");
            tcpPort = int.Parse(oConfig.getValue("tcp-port"));
            oilAddr = (ushort)Convert.ToInt32(oConfig.getValue("oil-addr"), 16);
            initTcp();
        }

        private void initTcp() {
            //client = new TcpClient(tcpHost, tcpPort);
            //master = ModbusIpMaster.CreateIp(client);
        }
        public double readvalue()
        {
            //从modusb里度一个数值，具体地址从配置文件中读入
            
            //The first one is used to read coils, the rest read registers
            //To find out the specific content in ReadMe.txt.
            //bool[] res = master.ReadCoils(oilAddr, 1);
            ushort[] res = master.ReadHoldingRegisters(oilAddr, 1);
            //ushort[] res = master.ReadInputRegisters(oilAddr, 1);
            //bool[] res = master.ReadInputs(oilAddr, 1);

            return (double)res[0];
            //return 0.0;
        }
    }
}
