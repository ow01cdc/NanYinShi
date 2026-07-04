namespace NanYinShiTeachersCollege.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MenuT",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        MenuText = c.String(),
                        MenuImage = c.String(),
                        MenuPage = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.PersonnelT",
                c => new
                    {
                        PersonnelID = c.Int(nullable: false),
                        name = c.String(),
                        Gender = c.String(),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.PersonnelID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PersonnelT");
            DropTable("dbo.MenuT");
        }
    }
}
