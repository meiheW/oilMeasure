using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace oilMeasure
{
    public partial class MainForm : Form
    {
        bool _bsaved = false;
        bool _bstartm = false;

        Config config = new Config();
        FileLog filelog = new FileLog();
        GPSSerials gps = new GPSSerials();
        Oilmodbus oil = new Oilmodbus();
        

        public MainForm()
        {
            InitializeComponent();
        }

        //开始测量
        private void btnStart_Click(object sender, EventArgs e)
        {
            _bstartm = !_bstartm;
            if (_bstartm)
            {
                //打开串口读取数据，GPS的位置信息
                gps.init();
                //btnStart.BackColor = Color.Red;
                btnStart.Text = "停止测量";
                timer1.Interval = 1000;
                timer1.Enabled = true;

            }
            else
            {
                //btnStart.BackColor = Color.Green;
                btnStart.Text = "开始测量";
                //timer1.Interval = 1000;
                timer1.Enabled = false;
                //关闭串口读取数据
                gps.close();
            }

        }

        //开始记录
        private void btnSave_Click(object sender, EventArgs e)
        {
            _bsaved = !_bsaved;
            
            if( _bsaved )
            {
                btnSave.BackColor = Color.Red;
                btnSave.Text = "停止记录";
            }
            else
            {
                btnSave.BackColor = Color.Green;
                btnSave.Text = "开始记录";
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;

            //从TCP modus 读取指定的数据，并显示在界面上
            //从串口读出数据并显示

            double v = oil.readvalue();
            lablon.Text = gps.Lon.ToString();
            lablat.Text = gps.Lat.ToString();
            labMeasure.Text = v.ToString();

            if ( _bsaved )
            {
                //文件管理
                filelog.LogToFile(gps.Lon, gps.Lat,v );
            }

            timer1.Enabled = true;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer1.Enabled = false;
            gps.close();
        }

    }
}
