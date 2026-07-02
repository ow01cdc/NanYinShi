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

namespace NanYinShiTeachersCollege.Pages
{
    public partial class MenuPage : UserControl
    {
        public MenuPage()
        {
            InitializeComponent();

            //美化dataGridViewl
            BeautifyDataGridView(dataGridView1);


        }

        private void MenuPage_Load(object sender, EventArgs e)
        {
            //查询数据，展示到界面
            using (var db = new AppDbContext())
            {
                List<MenuTModel> menus = db.Menus.ToList();
                dataGridView1.AutoGenerateColumns = false;
                dataGridView1.DataSource = menus;

            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
        public void BeautifyDataGridView(DataGridView dgv)
        {
            dgv.AutoGenerateColumns = false;
            dgv.RowHeadersVisible = false;

            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(46, 87, 162);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("微软雅黑", 10, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.ColumnHeadersHeight = 40; // 可根据需要调整高度

            dgv.DefaultCellStyle.BackColor = Color.White;
            dgv.DefaultCellStyle.ForeColor = Color.Black;
            dgv.DefaultCellStyle.SelectionBackColor = Color.LightBlue;
            dgv.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgv.DefaultCellStyle.Font = new Font("微软雅黑", 10);
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);

            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.RowTemplate.Height = 30;
            dgv.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dgv.GridColor = Color.LightGray;

            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.AllowUserToResizeRows = false;
            dgv.ReadOnly = true;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }
        //打开添加菜单的界面 
        private void btnAddMenu_Click(object sender, EventArgs e)
        {
            AddMenuForm addMenuForm = new AddMenuForm(this);
            addMenuForm.ShowDialog();
        }

        public void LoadMenu()
        {
            //查询数据，展示到界面
            using (var db = new AppDbContext())
            {
                List<MenuTModel> menus = db.Menus.ToList();
                dataGridView1.AutoGenerateColumns = false;
                dataGridView1.DataSource = menus;

            }
        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {

        }
    }
}
