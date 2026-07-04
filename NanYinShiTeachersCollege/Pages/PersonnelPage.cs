using NanYinShiTeachersCollege.EF6;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NanYinShiTeachersCollege.Pages
{
    public partial class PersonnelPage : UserControl
    {
        public PersonnelPage()
        {
            InitializeComponent();

            //美化dataGridViewl
            BeautifyDataGridView(dataGridView1);


        }

        private void PersonnelPage_Load(object sender, EventArgs e)
        {
            //查询数据，展示到界面
            LoadPersonnel();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        // 点击操作列的编辑/删除图标
        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            // 只处理操作列（Column1）的数据行，通过列名判断，避免新增列导致索引偏移
            if (e.RowIndex < 0 || dataGridView1.Columns[e.ColumnIndex].Name != "Column1") return;

            var personnel = dataGridView1.Rows[e.RowIndex].DataBoundItem as PersonnelTModel;
            if (personnel == null) return;

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
                // 点击编辑图标：打开编辑窗体（复用 AddPersonnelForm 的编辑模式）
                AddPersonnelForm editForm = new AddPersonnelForm(this, personnel);
                editForm.ShowDialog();
            }
            else if (deleteRelative.Contains(e.X, e.Y))
            {
                // 点击删除图标：确认后从数据库删除
                DialogResult dr = MessageBox.Show(
                    $"确定要删除人员「{personnel.name}」吗？",
                    "确认删除",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    using (var db = new AppDbContext())
                    {
                        var entity = db.Personnels.FirstOrDefault(p => p.PersonnelID == personnel.PersonnelID);
                        if (entity != null)
                        {
                            db.Personnels.Remove(entity);
                            db.SaveChanges();
                        }
                    }
                    LoadPersonnel();
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
        //打开添加人员的界面
        private void btnAddMenu_Click(object sender, EventArgs e)
        {
            //验证当前登录的用户有没有此功能的操作权限
            bool isInpermission = false;
            foreach(var item in GlobalClass.Instance.UserPermissions)
            {
                if(item.MenuPage == "PersonnelPage")
                {
                    if(item.Functions.Contains(MenuFunctionEnum.增加.ToString()))
                    {
                        isInpermission = true;
                        break;
                    }
                }
            }

            if(isInpermission)
            {
                MessageBox.Show("你当前没有操作权限");
                return;
            }
            AddPersonnelForm addPerssonelForm = new AddPersonnelForm(this);
            addPerssonelForm.ShowDialog();
        }

        public void LoadPersonnel()
        {
            //查询数据，展示到界面
            using (var db = new AppDbContext())
            {
                List<PersonnelTModel> personnels = db.Personnels.ToList();
                dataGridView1.AutoGenerateColumns = false;
                dataGridView1.DataSource = personnels;

            }
        }

        // 操作列中编辑、删除图标的区域，用于后续点击命中判断
        private Rectangle editRect = Rectangle.Empty;
        private Rectangle deleteRect = Rectangle.Empty;

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // 只处理操作列（Column1）的数据行，通过列名判断，避免新增列导致索引偏移
            if (e.RowIndex < 0 || e.ColumnIndex < 0 ||
                dataGridView1.Columns[e.ColumnIndex].Name != "Column1")
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
        //导入Excel数据
        private void button1_Click(object sender, EventArgs e)
        {
            List<PersonnelTModel> lst = ReadExcelToPersonnels("path");
            using (var db = new AppDbContext())
            {
              PersonnelTModel model =    db.Personnels.OrderByDescending(m => m.PersonnelID).First();
                int maxPersonnelID = model.PersonnelID;
                foreach(var itm  in lst)
                {
                    maxPersonnelID++;
                    itm.PersonnelID = maxPersonnelID;
               
                }

                db.Personnels.AddRange(lst);
                db.SaveChanges();
            }
            LoadPersonnel();
        }
        //导出Excel数据
        private void button2_Click(object sender, EventArgs e)
        {
            //获取数据库数据
            using (var db = new AppDbContext())
            {
               List<PersonnelTModel> list =  db.Personnels.ToList();
                ExportPersonnelToExcel(list, " ");
            }

           
        }

        /// 读取Excel表格，返回List<PersonnelTModel>模型数据
		/// </summary>
		/// <param name="filePath">Excel文件路径</param>
		/// <returns></returns>
		public static List<PersonnelTModel> ReadExcelToPersonnels(string filePath)
        {
            List<PersonnelTModel> list = new List<PersonnelTModel>();

            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                //读取工作簿对象 .xlsx（Excel 2007+，XSSF） .xls（Excel 2003，HSSF）
                //IWorkbook workbook = new HSSFWorkbook(); // 2003
                IWorkbook workbook = new XSSFWorkbook(fs);
                //读取工作簿对象下的Sheet页面对象
                ISheet sheet = workbook.GetSheetAt(0);

                //循环遍历所有行数据（直接跳过第一行）
                for (int i = sheet.FirstRowNum; i <= sheet.LastRowNum; i++)
                {
                    if (i == 0)
                        continue;

                    IRow row = sheet.GetRow(i);
                    if (row == null) continue;

                    PersonnelTModel model = new PersonnelTModel
                    {
                        name = row.GetCell(0).ToString(),
                        Gender = row.GetCell(1).ToString(),
                        Address = row.GetCell(2).ToString()
                    };

                    list.Add(model);
                }
            }
            return list;
        }


        /// <summary>
        /// 导出List<PersonnelTModel>模型数据到Excel
        /// </summary>
        /// <param name="list"></param>
        /// <param name="filePath"></param>
        public static void ExportPersonnelToExcel(List<PersonnelTModel> list, string filePath)
        {
            //创建xlsx格式的工作簿
            IWorkbook workbook = new XSSFWorkbook(); // xlsx
            ISheet sheet = workbook.CreateSheet("Personnel");

            //创建表头
            IRow headerRow = sheet.CreateRow(0);
            headerRow.CreateCell(0).SetCellValue("学号");
            headerRow.CreateCell(1).SetCellValue("姓名");
            headerRow.CreateCell(2).SetCellValue("性别");
            headerRow.CreateCell(3).SetCellValue("家庭住址");

            //已有表头，直接从第二行开始创建
            int i = 1;
            foreach (var model in list)
            {
                IRow row = sheet.CreateRow(i);

                row.CreateCell(0).SetCellValue(model.PersonnelID);
                row.CreateCell(1).SetCellValue(model.name);
                row.CreateCell(2).SetCellValue(model.Gender);
                row.CreateCell(3).SetCellValue(model.Address);

                //每次创建一行，i++，下次创建下一行
                i++;
            }

            //写入文件
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                workbook.Write(fs);
            }
        }
  
       

    }
}
