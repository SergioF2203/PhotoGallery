namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v5 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Photos", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Photos", new[] { "ApplicationUser_Id" });
            DropTable("dbo.Photos");
        }
        
        public override void Down()
        {
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
                        ApplicationUserId = c.Guid(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Photos", "ApplicationUser_Id");
            AddForeignKey("dbo.Photos", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
