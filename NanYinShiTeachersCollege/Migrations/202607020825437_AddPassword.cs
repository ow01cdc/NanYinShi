namespace NanYinShiTeachersCollege.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPassword : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PersonnelT", "Password", c => c.String());
            DropColumn("dbo.MenuT", "UserPassword");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MenuT", "UserPassword", c => c.String());
            DropColumn("dbo.PersonnelT", "Password");
        }
    }
}
