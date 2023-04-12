namespace LinqStoringData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingPercentage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Subjects", "Percentage", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Subjects", "Percentage");
        }
    }
}
