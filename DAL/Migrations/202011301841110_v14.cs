namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v14 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LikedEntities",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LikedEntityApplicationUsers",
                c => new
                    {
                        LikedEntity_Id = c.String(nullable: false, maxLength: 128),
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LikedEntity_Id, t.ApplicationUser_Id })
                .ForeignKey("dbo.LikedEntities", t => t.LikedEntity_Id, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .Index(t => t.LikedEntity_Id)
                .Index(t => t.ApplicationUser_Id);
            
            DropColumn("dbo.Photos", "Raiting_CurrentRaiting");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Photos", "Raiting_CurrentRaiting", c => c.Single(nullable: false));
            DropForeignKey("dbo.LikedEntityApplicationUsers", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.LikedEntityApplicationUsers", "LikedEntity_Id", "dbo.LikedEntities");
            DropIndex("dbo.LikedEntityApplicationUsers", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.LikedEntityApplicationUsers", new[] { "LikedEntity_Id" });
            DropTable("dbo.LikedEntityApplicationUsers");
            DropTable("dbo.LikedEntities");
        }
    }
}
