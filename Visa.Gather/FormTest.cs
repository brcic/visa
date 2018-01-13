using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Visa.Gather
{
    public partial class FormTest : Form
    {
        public FormTest()
        {
            InitializeComponent();
        }
        [DllImport("user32.dll")]
        public static extern IntPtr GetDC(IntPtr hWnd);

        object FRegTemplate;
        int FingerCount;
        int fpcHandle;

        string[] FFingerNames = new string[1000];
        int FMatchType;

        string s_zkfp;
        char[] s_zkfp1 = new char[2000];
       
        private void button1_Click(object sender, EventArgs e)
        {
          
        }
        int InitId = -1;
        private void FormTest_Load(object sender, EventArgs e)
        {

            FMatchType = 0;
        }
    }
}
