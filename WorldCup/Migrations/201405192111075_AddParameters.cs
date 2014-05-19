namespace WorldCup.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddParameters : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Parameters",
                c => new
                    {
                        Name = c.String(nullable: false, maxLength: 128),
                        Value = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Name);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Parameters");
        }

    }
}