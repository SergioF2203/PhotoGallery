namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v7 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PhotoAlbums", "Album_Id", "dbo.Albums");
            DropForeignKey("dbo.PhotoAlbums", "Photo_Id", "dbo.Photos");
            DropIndex("dbo.PhotoAlbums", new[] { "Photo_Id" });
            DropIndex("dbo.PhotoAlbums", new[] { "Album_Id" });
            DropPrimaryKey("dbo.Albums");
            DropPrimaryKey("dbo.Photos");
            DropPrimaryKey("dbo.PhotoAlbums");
            AlterColumn("dbo.Albums", "Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.Photos", "Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.PhotoAlbums", "Photo_Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.PhotoAlbums", "Album_Id", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.Albums", "Id");
            AddPrimaryKey("dbo.Photos", "Id");
            AddPrimaryKey("dbo.PhotoAlbums", new[] { "Photo_Id", "Album_Id" });
            CreateIndex("dbo.PhotoAlbums", "Photo_Id");
            CreateIndex("dbo.PhotoAlbums", "Album_Id");
            AddForeignKey("dbo.PhotoAlbums", "Album_Id", "dbo.Albums", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PhotoAlbums", "Photo_Id", "dbo.Photos", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PhotoAlbums", "Photo_Id", "dbo.Photos");
            DropForeignKey("dbo.PhotoAlbums", "Album_Id", "dbo.Albums");
            DropIndex("dbo.PhotoAlbums", new[] { "Album_Id" });
            DropIndex("dbo.PhotoAlbums", new[] { "Photo_Id" });
            DropPrimaryKey("dbo.PhotoAlbums");
            DropPrimaryKey("dbo.Photos");
            DropPrimaryKey("dbo.Albums");
            AlterColumn("dbo.PhotoAlbums", "Album_Id", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.PhotoAlbums", "Photo_Id", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Photos", "Id", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Albums", "Id", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.PhotoAlbums", new[] { "Photo_Id", "Album_Id" });
            AddPrimaryKey("dbo.Photos", "Id");
            AddPrimaryKey("dbo.Albums", "Id");
            CreateIndex("dbo.PhotoAlbums", "Album_Id");
            CreateIndex("dbo.PhotoAlbums", "Photo_Id");
            AddForeignKey("dbo.PhotoAlbums", "Photo_Id", "dbo.Photos", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PhotoAlbums", "Album_Id", "dbo.Albums", "Id", cascadeDelete: true);
        }
    }
}
