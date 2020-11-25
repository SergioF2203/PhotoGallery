namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v8 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PhotoAlbums", "Photo_Id", "dbo.Photos");
            DropIndex("dbo.PhotoAlbums", new[] { "Photo_Id" });
            DropPrimaryKey("dbo.Photos");
            DropPrimaryKey("dbo.PhotoAlbums");
            AlterColumn("dbo.Photos", "Id", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Photos", "PhotoPath", c => c.String());
            AlterColumn("dbo.PhotoAlbums", "Photo_Id", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Photos", "Id");
            AddPrimaryKey("dbo.PhotoAlbums", new[] { "Photo_Id", "Album_Id" });
            CreateIndex("dbo.PhotoAlbums", "Photo_Id");
            AddForeignKey("dbo.PhotoAlbums", "Photo_Id", "dbo.Photos", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PhotoAlbums", "Photo_Id", "dbo.Photos");
            DropIndex("dbo.PhotoAlbums", new[] { "Photo_Id" });
            DropPrimaryKey("dbo.PhotoAlbums");
            DropPrimaryKey("dbo.Photos");
            AlterColumn("dbo.PhotoAlbums", "Photo_Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.Photos", "PhotoPath", c => c.String(nullable: false));
            AlterColumn("dbo.Photos", "Id", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.PhotoAlbums", new[] { "Photo_Id", "Album_Id" });
            AddPrimaryKey("dbo.Photos", "Id");
            CreateIndex("dbo.PhotoAlbums", "Photo_Id");
            AddForeignKey("dbo.PhotoAlbums", "Photo_Id", "dbo.Photos", "Id", cascadeDelete: true);
        }
    }
}
