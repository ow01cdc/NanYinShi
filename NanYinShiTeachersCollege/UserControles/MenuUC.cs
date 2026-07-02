using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NanYinShiTeachersCollege.UserControles
{
    public partial class MenuUC : UserControl


    {
        public event EventHandler LableClick;
        public MenuUC()
        {
            InitializeComponent();
            foreach (Control item in this.Controls)
            {
                item.Click += AllControlsClick;
                item.MouseEnter += MenuUC_MouseEnter;
                item.MouseLeave += MenuUC_MouseLeave;
                item.MouseDown += MenuUC_MouseDown;
                item.MouseUp += MenuUC_MouseUp;
            }

            //添加当前菜单用户控件的鼠标移入事件
            this.MouseEnter += MenuUC_MouseEnter;

            //添加当前菜单用户控件的鼠标移出事件
            this.MouseLeave += MenuUC_MouseLeave;
            //添加当前菜单用户控件的鼠标按下/弹起事件
            this.MouseDown += MenuUC_MouseDown;
            this.MouseUp+= MenuUC_MouseUp;

        }

        private void MenuUC_MouseDown(object sender, MouseEventArgs e)
        {
            this.BackColor = MenuPressColor;
        }

        private void MenuUC_MouseUp(object sender, MouseEventArgs e)
        {
            this.BackColor = MenuBaseColor;
        }
        private void MenuUC_MouseLeave(object sender, EventArgs e)
        {
            this.BackColor = MenuBaseColor;
        }

        private void MenuUC_MouseEnter(object sender, EventArgs e)
        {
            this.BackColor = MenuHoverColor;
        }

        public void AllControlsClick(object sender, EventArgs e)
        {
            LableClick?.Invoke(sender, e);

            
        }

     

        private void MenuUC_Load(object sender, EventArgs e)
        {

        }
        [Description("这是设置当前菜单名称的属性")]

        public string MemuText
        {
            get
            {
                return label1.Text;
            }
            set
            {
                label1.Text = value;
            }
        }
        [Description("这是设置当前菜单图片的属性")]
        public Image MenuImage
        {
            get
            {
                return pictureBox1.Image;
            }
            set
            {
                pictureBox1.Image = value;
            }
        }
        [Description("控件基本颜色")]
        public Color MenuBaseColor { get; set; } = Color.Black;
        
        [Description("鼠标移入控件颜色")]
        public Color MenuHoverColor
        {
            get; set;
        } = Color.FromArgb(55, 60, 70);
        [Description("鼠标点击控件颜色")]
        public Color MenuPressColor
        {
            get; set;
        } = Color.FromArgb(25, 30, 35);

   
    }
}
