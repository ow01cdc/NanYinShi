namespace NanYinShiTeachersCollege.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserPawwswordToPersonnelTModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MenuT", "UserPassword", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.MenuT", "UserPassword");
        }
    }
}
