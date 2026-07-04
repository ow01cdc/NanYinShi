namespace NanYinShiTeachersCollege.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPermissionT : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Permission",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoleID = c.Int(nullable: false),
                        MenuPage = c.String(),
                        Functions = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Permission");
        }
    }
}
