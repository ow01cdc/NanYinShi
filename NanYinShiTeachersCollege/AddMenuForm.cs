using NanYinShiTeachersCollege.EF6;
using NanYinShiTeachersCollege.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NanYinShiTeachersCollege
{
    public partial class AddMenuForm : Form
    {   
        MenuPage CurrentMenuPage { get; set; }
        public AddMenuForm(MenuPage menuPage)
        {
            CurrentMenuPage = menuPage;
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        //添加菜单数据到数据库
        private void btnSave_Click(object sender, EventArgs e)
        {
            string menuText = textBox1.Text;
            string menuImage = textBox2.Text;
            string menuPage = textBox3.Text;

            if (string.IsNullOrEmpty(menuText))
            {
                MessageBox.Show("请输入菜单名称");
                return;
            }

            if (string.IsNullOrEmpty(menuImage))
            {
                MessageBox.Show("请输入菜单图片");
                return;
            }
            if (string.IsNullOrEmpty(menuPage))
            {
                MessageBox.Show("请输入菜单页面");
                return;
            }

            using (var db = new AppDbContext())
            {
                MenuTModel menuTModel = new MenuTModel();

                menuTModel.MenuText = menuText;
                menuTModel.MenuImage = menuImage;
                menuTModel.MenuPage = menuPage;
                db.Menus.Add(menuTModel);
                db.SaveChanges();
            }
            MessageBox.Show("当前数据添加成功");
            this.Close();
            CurrentMenuPage.LoadMenu();

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
