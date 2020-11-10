namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Photos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PhotoPath = c.String(nullable: false),
                        PhotoTitle = c.String(),
                        DateTimeUploading = c.DateTime(nullable: false),
                        Raiting_VoicesCount = c.Int(nullable: false),
                        Raiting_CurrentRaiting = c.Single(nullable: false),
                        Album_AlbumName = c.String(),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Photos", "UserId", "dbo.Users");
            DropIndex("dbo.Photos", new[] { "UserId" });
            DropTable("dbo.Photos");
        }
    }
}
