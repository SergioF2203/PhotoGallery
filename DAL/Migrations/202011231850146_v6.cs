namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v6 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Albums",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        AlbumName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Photos",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        PhotoPath = c.String(nullable: false),
                        PhotoTitle = c.String(),
                        DateTimeUploading = c.DateTime(nullable: false),
                        Raiting_VoicesCount = c.Int(nullable: false),
                        Raiting_CurrentRaiting = c.Single(nullable: false),
                        ExifData_Width = c.Int(),
                        ExifData_Height = c.Int(),
                        ExifData_CreateDate = c.DateTime(nullable: false),
                        IsPublish = c.Boolean(nullable: false),
                        ApplicationUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .Index(t => t.ApplicationUserId);
            
            CreateTable(
                "dbo.PhotoAlbums",
                c => new
                    {
                        Photo_Id = c.String(nullable: false, maxLength: 128),
                        Album_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Photo_Id, t.Album_Id })
                .ForeignKey("dbo.Photos", t => t.Photo_Id, cascadeDelete: true)
                .ForeignKey("dbo.Albums", t => t.Album_Id, cascadeDelete: true)
                .Index(t => t.Photo_Id)
                .Index(t => t.Album_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Photos", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.PhotoAlbums", "Album_Id", "dbo.Albums");
            DropForeignKey("dbo.PhotoAlbums", "Photo_Id", "dbo.Photos");
            DropIndex("dbo.PhotoAlbums", new[] { "Album_Id" });
            DropIndex("dbo.PhotoAlbums", new[] { "Photo_Id" });
            DropIndex("dbo.Photos", new[] { "ApplicationUserId" });
            DropTable("dbo.PhotoAlbums");
            DropTable("dbo.Photos");
            DropTable("dbo.Albums");
        }
    }
}
