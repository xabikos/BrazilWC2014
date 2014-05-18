namespace WorldCup.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Dummy : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RaisedMoney", "DummyProperty", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RaisedMoney", "DummyProperty");
        }
    }
}
