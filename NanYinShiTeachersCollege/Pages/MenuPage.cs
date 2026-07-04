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

        // 点击操作列的编辑/删除图标
        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            // 只处理操作列（ColOperation 索引为 3）的数据行
            if (e.ColumnIndex != 3 || e.RowIndex < 0) return;

            var menu = dataGridView1.Rows[e.RowIndex].DataBoundItem as MenuTModel;
            if (menu == null) return;

            // 与 CellPainting 中一致的图标位置计算（相对单元格左上角）
            Rectangle cellRect = dataGridView1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
            int imgSize = 24;
            int gap = 10;
            int totalWidth = imgSize * 2 + gap;
            int startX = (cellRect.Width - totalWidth) / 2;
            int startY = (cellRect.Height - imgSize) / 2;

            Rectangle editRelative = new Rectangle(startX, startY, imgSize, imgSize);
            Rectangle deleteRelative = new Rectangle(startX + imgSize + gap, startY, imgSize, imgSize);

            // e.X / e.Y 是相对于单元格左上角的坐标
            if (editRelative.Contains(e.X, e.Y))
            {
                // 点击编辑图标：打开编辑窗体（复用 AddMenuForm 的编辑模式）
                AddMenuForm editForm = new AddMenuForm(this, menu);
                editForm.ShowDialog();
            }
            else if (deleteRelative.Contains(e.X, e.Y))
            {
                // 点击删除图标：确认后从数据库删除
                DialogResult dr = MessageBox.Show(
                    $"确定要删除菜单「{menu.MenuText}」吗？",
                    "确认删除",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    using (var db = new AppDbContext())
                    {
                        var entity = db.Menus.FirstOrDefault(m => m.id == menu.id);
                        if (entity != null)
                        {
                            db.Menus.Remove(entity);
                            db.SaveChanges();
                        }
                    }
                    LoadMenu();
                    MessageBox.Show("删除成功");
                }
            }
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

        // 操作列中编辑、删除图标的区域，用于后续点击命中判断
        private Rectangle editRect = Rectangle.Empty;
        private Rectangle deleteRect = Rectangle.Empty;

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // 只处理操作列的数据行（ColOperation 索引为 3）
            if (e.ColumnIndex != 3 || e.RowIndex < 0)
                return;

            Image editImg = Properties.Resources.edit;
            Image deleteImg = Properties.Resources.delete;

            int imgSize = 24; // 图标显示尺寸
            int gap = 10;     // 两个图标之间的间距
            int totalWidth = imgSize * 2 + gap;
            int startX = e.CellBounds.X + (e.CellBounds.Width - totalWidth) / 2;
            int startY = e.CellBounds.Y + (e.CellBounds.Height - imgSize) / 2;

            editRect = new Rectangle(startX, startY, imgSize, imgSize);
            deleteRect = new Rectangle(startX + imgSize + gap, startY, imgSize, imgSize);

            // 绘制单元格背景和选中样式
            bool selected = (e.State & DataGridViewElementStates.Selected) == DataGridViewElementStates.Selected;
            Brush backBrush = selected
                ? new SolidBrush(dataGridView1.DefaultCellStyle.SelectionBackColor)
                : new SolidBrush(e.CellStyle.BackColor);
            try
            {
                e.Graphics.FillRectangle(backBrush, e.CellBounds);
            }
            finally
            {
                backBrush.Dispose();
            }

            // 绘制两个图标
            e.Graphics.DrawImage(editImg, editRect);
            e.Graphics.DrawImage(deleteImg, deleteRect);

            // 绘制单元格边框
            if (dataGridView1.CellBorderStyle != DataGridViewCellBorderStyle.None)
            {
                ControlPaint.DrawBorder(e.Graphics, e.CellBounds,
                    dataGridView1.GridColor, 0, ButtonBorderStyle.None,
                    dataGridView1.GridColor, 0, ButtonBorderStyle.None,
                    dataGridView1.GridColor, 0, ButtonBorderStyle.None,
                    dataGridView1.GridColor, 1, ButtonBorderStyle.Solid);
            }

            e.Handled = true;
        }
    }
}
