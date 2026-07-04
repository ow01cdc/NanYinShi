using NanYinShiTeachersCollege.EF6;
using NanYinShiTeachersCollege.Pages;
using NPOI.SS.Formula.Functions;
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

    public partial class AddPersonnelForm : Form
    {
        PersonnelPage CurrentPersonnelPage { get; set; }
        // 编辑模式下的原始数据，null 表示添加模式
        PersonnelTModel EditModel { get; set; }
        List<RoleMode> roles = new List<RoleMode>();


        public AddPersonnelForm(PersonnelPage personnelPage)
        {
            CurrentPersonnelPage = personnelPage;
            EditModel = null;
            InitializeComponent();

            InitRoleCombo();
        }

        // 编辑模式构造函数：传入要编辑的人员数据
        internal AddPersonnelForm(PersonnelPage personnelPage, PersonnelTModel editModel)
        {
            CurrentPersonnelPage = personnelPage;
            EditModel = editModel;
            InitializeComponent();
            InitRoleCombo();
        }

        private void AddPersonnelForm_Load(object sender, EventArgs e)
        {
           
            if (EditModel != null)
            {
                // 编辑模式：调整界面文案并预填数据
                this.Text = "编辑人员";
                label1.Text = " 编辑人员";
                btnSave.Text = " 保存";
                // 学号是主键，编辑模式下只读，不允许修改
                textBox4.Text = EditModel.PersonnelID.ToString();
                textBox4.ReadOnly = true;
                textBox1.Text = EditModel.name ?? string.Empty;
                textBox2.Text = EditModel.Gender ?? string.Empty;
                textBox3.Text = EditModel.Address ?? string.Empty;
                comboBox1.SelectedValue = EditModel.RoleID;
            }
            // 添加模式：学号由用户手动输入，不做限制
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
        //添加/修改人员数据到数据库
        private void btnSave_Click(object sender, EventArgs e)
        {
            string idText = textBox4.Text;
            string name = textBox1.Text;
            string gender = textBox2.Text;
            string address = textBox3.Text;
            //int roleId = int.Parse(comboBox1.SelectedValue.ToString());
            int roleId = (int)comboBox1.SelectedValue;

            // 添加模式下学号必填且必须为整数
            int personnelId = 0;
            if (EditModel == null)
            {
                if (string.IsNullOrEmpty(idText))
                {
                    MessageBox.Show("请输入学号");
                    return;
                }
                if (!int.TryParse(idText, out personnelId))
                {
                    MessageBox.Show("学号必须为整数");
                    return;
                }
            }
            else
            {
                personnelId = EditModel.PersonnelID;
            }

            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("请输入姓名");
                return;
            }

            if (string.IsNullOrEmpty(gender))
            {
                MessageBox.Show("请输入性别");
                return;
            }
            if (string.IsNullOrEmpty(address))
            {
                MessageBox.Show("请输入家庭住址");
                return;
            }

            using (var db = new AppDbContext())
            {
                if (EditModel != null)
                {
                    // 编辑模式：根据 PersonnelID 查出原记录并更新
                    var entity = db.Personnels.FirstOrDefault(p => p.PersonnelID == EditModel.PersonnelID);
                    if (entity != null)
                    {
                        entity.name = name;
                        entity.Gender = gender;
                        entity.Address = address;
                        entity.RoleID = roleId;
                        db.SaveChanges();
                    }
                    MessageBox.Show("当前数据修改成功");
                }
                else
                {
                    // 添加模式：检查学号是否已存在
                    if (db.Personnels.Any(p => p.PersonnelID == personnelId))
                    {
                        MessageBox.Show("该学号已存在，请重新输入");
                        return;
                    }
                    PersonnelTModel personnelTModel = new PersonnelTModel();
                    personnelTModel.PersonnelID = personnelId;
                    personnelTModel.name = name;
                    personnelTModel.Gender = gender;
                    personnelTModel.Address = address;
                    personnelTModel.RoleID = roleId;
                    db.Personnels.Add(personnelTModel);
                    db.SaveChanges();
                    MessageBox.Show("当前数据添加成功");
                }
            }
            this.Close();
            CurrentPersonnelPage.LoadPersonnel();

        }
        public class RoleMode
        {
            public int RoleId_ { get; set; }
            public string Name_ { get; set; }
            public override string ToString()
            {
                return Name_;
            }
        }
        private void InitRoleCombo()
        {
            roles = new List<RoleMode>()
            {
                new RoleMode { RoleId_ = 1, Name_ = "学生" },
                new RoleMode { RoleId_ = 2, Name_ = "教师" },
                new RoleMode { RoleId_ = 3, Name_ = "超级管理员" }
            };
            comboBox1.DataSource = roles;
            comboBox1.DisplayMember = nameof(RoleMode.Name_);
            comboBox1.ValueMember = nameof(RoleMode.RoleId_);
        }
        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
