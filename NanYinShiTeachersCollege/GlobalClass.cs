using NanYinShiTeachersCollege.EF6;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanYinShiTeachersCollege
{
    internal class GlobalClass
    {
        private static GlobalClass _Instance = null;
        public static GlobalClass Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new GlobalClass();
                   
                }
                return _Instance;
            }
        }
       public PersonnelTModel CurrentLoginUser {  get; set; }


       //当前登录的首页
       public Form1 CurrentMainForm { get; set; }

        //当前登录用户所属角色的权限信息
        public List<PermissionTmodel> UserPermissions { get; set; }


    }
}
