using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanYinShiTeachersCollege.EF6
{
    [Table("PersonnelT")]
    public  class PersonnelTModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PersonnelID { get; set; }

        public string name { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }
        //角色id
        public int RoleID { get; set; }

        [NotMapped]
        public string RoleName
        {
            get
            {
                //只读返回角色名称，不要修改 name 字段
                if (RoleID == 1)
                    return "学生";
                else if (RoleID == 2)
                    return "老师";
                else if (RoleID == 3)
                    return "系统管理员";
                else
                    return "未知用户";
            }
        }

    }

}
