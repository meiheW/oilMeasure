using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using System.IO.Ports;
using System.Text.RegularExpressions;


namespace oilMeasure
{
    class GPSSerials
    {
        private SerialPort mCom_ = new SerialPort();
        private Config gConfig = new Config();

        public double Lon { get; set; }
        public double Lat { get; set; }
        public bool init()
        {
            //打开串口，进行线程读取数据解析经纬度
            mCom_Open();
            return true;
        }

        public bool close()
        {
            mCom_Close();
            return true;
        }

        //open the serial port.
        private void mCom_Open()
        {
            if (mCom_.IsOpen) return;
            try
            {
                //read parameter from config.
                string portName = gConfig.getValue("rtu-device");
                string baudRate = gConfig.getValue("rtu-baud");
                string parity = gConfig.getValue("rtu-parity");
                string dataBits = gConfig.getValue("rtu-data_bit");
                string stopBits = gConfig.getValue("rtu-stop_bit");

                //assign the para and open the com.
                mCom_.PortName = portName;
                mCom_.BaudRate = int.Parse(baudRate);
                mCom_.Parity = (Parity)Enum.Parse(typeof(Parity), parityNo(parity));
                mCom_.DataBits = int.Parse(dataBits);
                mCom_.StopBits = (StopBits)Enum.Parse(typeof(StopBits), stopBits);
                mCom_.RtsEnable = true;
                mCom_.DtrEnable = true;
                mCom_.WriteTimeout = 1000;
                
                mCom_.Open();

                mCom_.DataReceived += new SerialDataReceivedEventHandler(serialPort_DataReceived);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        
        //close the serial port
        private void mCom_Close() 
        {
            try{
                if(mCom_.IsOpen)
                {
                    mCom_.Close();
                }
            }
            catch(System.Exception ex){
                MessageBox.Show(ex.ToString());
            }
        }

        //parse the data from GPS --> longtitude & latitude
        private void serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            int readsize = mCom_.BytesToRead;
            if (readsize > 0)
            {
                byte[] recvbyte = new byte[readsize];
                mCom_.Read(recvbyte, 0, readsize);
                String strrecv = Encoding.ASCII.GetString(recvbyte);

                string[] strarr = strrecv.Split(',');
                Lon = double.Parse(strarr[4]);
                Lat = double.Parse(strarr[2]);
            }

        }

        /*
         *Test for gps data, just for example.
         *The format should be modified due to the specific situation.
         */
        public void MyGPSFormatTest()
        {
            string data = "$GPGGA, 121252.000, 3937.3032, N, 11611.6064, E, 1, 05, 2.0, 45.9, M, -5.7, M, 1, 0012, 000";
            string[] strarr = data.Split(',');
            Lon = double.Parse(strarr[4]);  //the 5th is longtitude.
            Lat = double.Parse(strarr[2]);  //the 3rd is latitude. 

        }

        //if the parity in config is "n","N","none",change it to "None";
        //because Parity needs None,Odd,Even,Mark or Space
        private string parityNo(string parity) 
        {
            string res = "";
            switch(parity){
                case "n":
                case "N":
                case "none":
                    res = "None";
                    break;

                case "o":
                case "O":
                case "odd":
                    res = "Odd";
                    break;

                case "e":
                case "E":
                case "even":
                    res = "Even";
                    break;

                case "m":
                case "M":
                case "mark":
                    res = "Mark";
                    break;

                case "s":
                case "S":
                case "space":
                    res = "Space";
                    break;

            }
            return res;

   
   
        }
    
    
    }
}
