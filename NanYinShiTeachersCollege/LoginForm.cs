using NanYinShiTeachersCollege.EF6;
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
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            // 登录窗体加载时的初始化（如有需要可在此补充）
        }

        // 登录按钮：校验姓名 + 密码，成功后进入 Form1
        private void btnSave_Click(object sender, EventArgs e)
        {
            string userName = textBox1.Text.Trim();
            string password = textBox2.Text;

            if (string.IsNullOrEmpty(userName))
            {
                MessageBox.Show("请输入姓名");
                return;
            }
            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("请输入密码");
                return;
            }

            using (var db = new AppDbContext())
            {
                // 按姓名 + 密码匹配用户
                PersonnelTModel user = db.Personnels.FirstOrDefault(p => p.name == userName && p.Password == password);
                if (user == null)
                {
                    MessageBox.Show("姓名或密码错误");
                    return;
                }
               //用当前登录的用户信息，查询对应的权限信息
               GlobalClass.Instance.UserPermissions = db.permissios.Where(m=>m.RoleID == user.RoleID).ToList();

               GlobalClass.Instance.CurrentLoginUser = user;

                // 登录成功：打开主窗体并隐藏登录窗体
                MessageBox.Show("登录成功");
                Form1 form1 = new Form1();
                GlobalClass.Instance.CurrentMainForm = form1;
                form1.FormClosed += (s, args) => this.Close();
                form1.Show();
                this.Hide();
            }
        }
    }
}
