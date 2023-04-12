namespace LinqStoringData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovingPercentage : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Subjects", "Percentage");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Subjects", "Percentage", c => c.Int(nullable: false));
        }
    }
}
