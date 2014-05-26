namespace WorldCup.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserRegistrationDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "RegistrationDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "RegistrationDate");
        }

    }
}