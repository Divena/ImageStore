namespace ImageStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Images", "DateTaken");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Images", "DateTaken", c => c.DateTime(nullable: false));
        }
    }
}
