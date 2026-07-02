using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanYinShiTeachersCollege.EF6
{
    internal class AppDbContext:DbContext
    {
        public AppDbContext() : base("Data Source=localhost,1433;Initial Catalog=NanYinShiDB;User ID=sa;Password=Sql@2026Test;TrustServerCertificate=True;MultipleActiveResultSets=True")
        {
        }

        public DbSet<MenuTModel> Menus { get; set; }
    }
}
