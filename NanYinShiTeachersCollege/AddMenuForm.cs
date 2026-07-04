using NanYinShiTeachersCollege.EF6;
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
    public partial class AddMenuForm : Form
    {
        MenuPage CurrentMenuPage { get; set; }
        // 编辑模式下的原始数据，null 表示添加模式
        MenuTModel EditModel { get; set; }

        public AddMenuForm(MenuPage menuPage)
        {
            CurrentMenuPage = menuPage;
            EditModel = null;
            InitializeComponent();
        }

        // 编辑模式构造函数：传入要编辑的菜单数据
        internal AddMenuForm(MenuPage menuPage, MenuTModel editModel)
        {
            CurrentMenuPage = menuPage;
            EditModel = editModel;
            InitializeComponent();
        }

        private void AddMenuForm_Load(object sender, EventArgs e)
        {
            if (EditModel != null)
            {
                // 编辑模式：调整界面文案并预填数据
                this.Text = "编辑菜单";
                label1.Text = " 编辑菜单";
                btnSave.Text = " 保存";
                textBox1.Text = EditModel.MenuText ?? string.Empty;
                textBox2.Text = EditModel.MenuImage ?? string.Empty;
                textBox3.Text = EditModel.MenuPage ?? string.Empty;
            }
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
        //添加/修改菜单数据到数据库
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
                if (EditModel != null)
                {
                    // 编辑模式：根据 id 查出原记录并更新
                    var entity = db.Menus.FirstOrDefault(m => m.id == EditModel.id);
                    if (entity != null)
                    {
                        entity.MenuText = menuText;
                        entity.MenuImage = menuImage;
                        entity.MenuPage = menuPage;
                        db.SaveChanges();
                    }
                    MessageBox.Show("当前数据修改成功");
                }
                else
                {
                    // 添加模式
                    MenuTModel menuTModel = new MenuTModel();
                    menuTModel.MenuText = menuText;
                    menuTModel.MenuImage = menuImage;
                    menuTModel.MenuPage = menuPage;
                    db.Menus.Add(menuTModel);
                    db.SaveChanges();
                    MessageBox.Show("当前数据添加成功");
                }
            }

            this.Close();
            CurrentMenuPage.LoadMenu();
            GlobalClass.Instance.CurrentMainForm.LoadMenu();

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
