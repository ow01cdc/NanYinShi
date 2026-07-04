using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanYinShiTeachersCollege.EF6
{
    [Table("Permission")]
    public class PermissionTmodel
    {
        public int Id { get; set; }
        public int RoleID { get; set; }
        public string MenuPage {  get; set; }
        public string Functions { get; set; }
    }
}
