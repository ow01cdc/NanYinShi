using NanYinShiTeachersCollege.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NanYinShiTeachersCollege
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

     
        }

        private void menuUC1_LableClick(object sender, EventArgs e)
        {
            panelContent.Controls.Clear();

            HomePage homep = new HomePage();
       
            panelContent.Controls.Add(homep);
        }

        private void menuUC2_LableClick(object sender, EventArgs e)
        {
            panelContent.Controls.Clear();
            SettingPage settingP = new SettingPage();

            panelContent.Controls.Add((SettingPage)settingP);
        }

        private void menuUC1_Load(object sender, EventArgs e)
        {

        }
    }
}
