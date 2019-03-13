using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void connectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("hello");
            IpAddress ipAddress = new IpAddress();
            ipAddress.ShowDialog();
        }

        private void load2DFILEToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }


    }
}
