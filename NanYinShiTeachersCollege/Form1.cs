using NanYinShiTeachersCollege.Models;
using NanYinShiTeachersCollege.Pages;
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
using Newtonsoft;
using Newtonsoft.Json;
using NanYinShiTeachersCollege.EF6;
using NanYinShiTeachersCollege.UserControles;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Reflection;

namespace NanYinShiTeachersCollege
{
    public partial class Form1 : Form
    {
        public Form1()
        {
           
            InitializeComponent();
            this.Text = GlobalClass.Instance.CurrentLoginUser.name;


        }


        public void LoadMenu()
        {
            flowLayoutPanel1.Controls.Clear();
            using (var db = new AppDbContext())
            {
                List<MenuTModel> ListMenu = new List<MenuTModel>();
                foreach (MenuTModel model in db.Menus.ToList())
                {

                    //看当前遍历的菜单，有没有权限
                    bool isInPermissions = false;

                    foreach (var item in GlobalClass.Instance.UserPermissions)
                    {
                        if (string.Equals(item.MenuPage, model.MenuPage, System.StringComparison.OrdinalIgnoreCase))
                        {
                            //验证功能是否有查看
                            if (!string.IsNullOrEmpty(item.Functions) && item.Functions.Contains(MenuFunctionEnum.查看.ToString()))
                            {
                                isInPermissions = true;
                                break;
                            }
                        }
                    }

                    if (!isInPermissions)
                    {
                        continue;
                    }
                    MenuUC menuUC = new MenuUC();
                    menuUC.MemuText = model.MenuText;
                    menuUC.MenuImage = GetBitmapFromResx(model.MenuImage);

                    menuUC.LableClick += (newSender, newE) =>
                    {
                        LoadPage(model.MenuPage);

                    };
                    flowLayoutPanel1.Controls.Add(button4);

                    flowLayoutPanel1.Controls.Add(menuUC);
                    flowLayoutPanel1.Controls.SetChildIndex(button4, 0);
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadMenu();
        }

            /* //读取菜单数据，决定哪一些隐藏
             string path = @"F:\C#\NanYinShiProjects\AllMenus.txt";
             string menuContent = File.ReadAllText(path);
             List<MenuModel> ListMenu = JsonConvert.DeserializeObject<List<MenuModel>>(menuContent);

             List<MenuTModel> menuts = new List<MenuTModel>();
             foreach (var item in ListMenu)
             {
                 MenuTModel menuTModel = new MenuTModel();
                 menuTModel.MenuText = item.MenuText;
                 menuTModel.MenuPage = item.MenuPage;
                 menuTModel.MenuImage = item.MenuImage;
                 menuts.Add(menuTModel);
             }
             //把数据对象转到数据库的数据对象、
             using (var db = new AppDbContext())
             {
                 db.Menus.AddRange(menuts);
                 db.SaveChanges(); 
             }
         }*/
        
        public Image GetBitmapFromResx(string name)
        {
            object obj = Properties.Resources.ResourceManager.GetObject(name);
            return obj as Image;
        }
        private void LoadPage(string pageName)
        {
            if (string.IsNullOrWhiteSpace(pageName))
                return;

            var assembly = Assembly.GetExecutingAssembly();

            Type pageType = assembly.GetTypes()
                .FirstOrDefault(t => t.Name == pageName);

            if (pageType == null)
            {
                MessageBox.Show($"找不到页面: {pageName}");
                return;
            }

            UserControl page = (UserControl)Activator.CreateInstance(pageType);

            panelContent.Controls.Clear();
            page.Dock = DockStyle.Fill;
            panelContent.Controls.Add(page);
        }

        private void panelContent_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }
    }
}
