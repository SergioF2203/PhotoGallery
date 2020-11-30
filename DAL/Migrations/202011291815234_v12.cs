namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v12 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PhotoAlbums", "Album_Id", "dbo.Albums");
            DropIndex("dbo.PhotoAlbums", new[] { "Album_Id" });
            DropPrimaryKey("dbo.Albums");
            DropPrimaryKey("dbo.PhotoAlbums");
            AlterColumn("dbo.Albums", "Id", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.PhotoAlbums", "Album_Id", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Albums", "Id");
            AddPrimaryKey("dbo.PhotoAlbums", new[] { "Photo_Id", "Album_Id" });
            CreateIndex("dbo.PhotoAlbums", "Album_Id");
            AddForeignKey("dbo.PhotoAlbums", "Album_Id", "dbo.Albums", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PhotoAlbums", "Album_Id", "dbo.Albums");
            DropIndex("dbo.PhotoAlbums", new[] { "Album_Id" });
            DropPrimaryKey("dbo.PhotoAlbums");
            DropPrimaryKey("dbo.Albums");
            AlterColumn("dbo.PhotoAlbums", "Album_Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.Albums", "Id", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.PhotoAlbums", new[] { "Photo_Id", "Album_Id" });
            AddPrimaryKey("dbo.Albums", "Id");
            CreateIndex("dbo.PhotoAlbums", "Album_Id");
            AddForeignKey("dbo.PhotoAlbums", "Album_Id", "dbo.Albums", "Id", cascadeDelete: true);
        }
    }
}
