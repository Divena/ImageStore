namespace ImageStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Actions",
                c => new
                    {
                        ActionID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ActionID);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.CategoryID);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        CommentID = c.Int(nullable: false, identity: true),
                        UserID = c.String(),
                        ImageID = c.Int(nullable: false),
                        Text = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        DatePublishing = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CommentID);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        CountryID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        count = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CountryID);
            
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        ImageID = c.Int(nullable: false, identity: true),
                        Picture = c.Binary(),
                        DateTaken = c.DateTime(nullable: false),
                        DatePublishing = c.DateTime(nullable: false),
                        Latitude = c.Double(),
                        Longitude = c.Double(),
                        Comment = c.String(maxLength: 100),
                        Name = c.String(maxLength: 30),
                        CategoryID = c.Int(nullable: false),
                        Rating = c.Double(nullable: false),
                        UserID = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        CountryID = c.Int(nullable: false),
                        PictureMimeType = c.String(),
                    })
                .PrimaryKey(t => t.ImageID);
            
            CreateTable(
                "dbo.Marks",
                c => new
                    {
                        MarkID = c.Int(nullable: false, identity: true),
                        ImageID = c.Int(nullable: false),
                        UserID = c.String(),
                        point = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MarkID);
            
            CreateTable(
                "dbo.AllActions",
                c => new
                    {
                        AllActionsID = c.Int(nullable: false, identity: true),
                        ImageID = c.Int(nullable: false),
                        UserID = c.String(),
                        ActionID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AllActionsID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AllActions");
            DropTable("dbo.Marks");
            DropTable("dbo.Images");
            DropTable("dbo.Countries");
            DropTable("dbo.Comments");
            DropTable("dbo.Categories");
            DropTable("dbo.Actions");
        }
    }
}
