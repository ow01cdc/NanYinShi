using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanYinShiTeachersCollege.EF6
{
    internal class AppDbContext : DbContext
    {
        public AppDbContext() : base("Data Source=localhost,1433;Initial Catalog=NanYinShiDB;User ID=sa;Password=Sql@2026Test;TrustServerCertificate=True;MultipleActiveResultSets=True")
        {
            // 不自动删库；表结构由 SQL 脚本手动维护
            Database.SetInitializer<AppDbContext>(null);
        }

        public DbSet<MenuTModel> Menus { get; set; }
        public DbSet<PersonnelTModel> Personnels { get; set; }

        public DbSet<PermissionTmodel> permissios { get; set; }
    }
   

}
