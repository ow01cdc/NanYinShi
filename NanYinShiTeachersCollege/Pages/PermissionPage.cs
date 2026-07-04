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
    public partial class PermissionPage : UserControl
    {
        public PermissionPage()
        {
            InitializeComponent();

            comboBox1.DataSource = Enum.GetValues(typeof(RoleEnum));

            using (var db = new AppDbContext())
            {
                List<MenuTModel> menuTModels = db.Menus.ToList();
                foreach (var model in menuTModels)
                {
                    MenuFunctionPage menuFunctionPage = new MenuFunctionPage(model);
                    menuFunctionPage.Dock = DockStyle.Top;
                    panel2.Controls.Add(menuFunctionPage);

                }
            }
            InitOptions();
        }

        public void InitOptions()
        {
            RoleEnum roleEnum = (RoleEnum)comboBox1.SelectedItem;
            int roleID = (int)roleEnum;
            List<PermissionTmodel> listPermission = new List<PermissionTmodel>();
            using (var db = new AppDbContext())
            {
                listPermission = db.permissios.Where(m => m.RoleID == roleID).ToList();
            }

            foreach (var item in panel2.Controls)
            {
                MenuFunctionPage page = (MenuFunctionPage)item;
                page.RestoreOptions(listPermission);
            }
        }

        private void btnAddMenu_Click(object sender, EventArgs e)
        {
            //获取角色id
            RoleEnum roleEnum = (RoleEnum)comboBox1.SelectedItem;
            int roleID = (int)roleEnum;
            List<PermissionTmodel> models = new List<PermissionTmodel>();

            foreach(var item in panel2.Controls)
            {
                MenuFunctionPage menuFunctionPage = (MenuFunctionPage)item;
                Tuple<string,List<string>> result = menuFunctionPage.GetMenuPageAndFunctions();
               string functions = string.Join(",", result.Item2);
                PermissionTmodel model = new PermissionTmodel
                {
                    RoleID = roleID,
                    Functions = functions,
                    MenuPage = result.Item1,
                };

                models.Add(model);
                
            }
            using(var db = new AppDbContext())
            {
                List<PermissionTmodel> permissionTmodels = db.permissios.Where(m => m.RoleID == roleID).ToList();

                db.permissios.RemoveRange(permissionTmodels);
                db.permissios.AddRange(models);
                db.SaveChanges();
            }
            MessageBox.Show("保存成功");
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            InitOptions();
        }
    }
}
