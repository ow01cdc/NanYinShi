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
    public partial class MenuFunctionPage : UserControl
    {
        MenuTModel CurrentMenuModel { get; set; }
        public MenuFunctionPage(MenuTModel model)
        {
            InitializeComponent();
            labMenuText.Text = model.MenuText;
            CurrentMenuModel = model;


            foreach(MenuFunctionEnum item in Enum.GetValues(typeof(MenuFunctionEnum))){
                CheckBox check =new CheckBox();
                check.Text = item.ToString();
                flowLayoutPanel1.Controls.Add(check);
            }
        }

        public Tuple<string,List<string>> GetMenuPageAndFunctions()
        {
            string menuPage = CurrentMenuModel.MenuPage;

            List<string> functions = new List<string>();


            foreach(var item in flowLayoutPanel1.Controls)
            {
                CheckBox check = (CheckBox)item;
                if (check.Checked){
                    string text = check.Text;
                    functions.Add(text);

                }
                     
                
               
            }
            Tuple<string, List<string>> result = new Tuple<string, List<string>>(menuPage,functions);
            return result;
        }

        public void RestoreOptions(List<PermissionTmodel> models)
        {

            foreach(var item in flowLayoutPanel1.Controls)
            {
                CheckBox check = new CheckBox();
                check.Checked = false;
            }
            foreach(var item in models) {
                if(item.MenuPage == CurrentMenuModel.MenuPage)
                {
                    foreach(var itemCheck in flowLayoutPanel1.Controls) {

                        CheckBox check = (CheckBox)itemCheck;
                        if (item.Functions.Contains(check.Text)){
                            check.Checked = true;
                        }
                                    
                    }
                }            
            }
        }
    }
}
