namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Photos", "ExifData_Width", c => c.Int());
            AddColumn("dbo.Photos", "ExifData_Height", c => c.Int());
            AddColumn("dbo.Photos", "ExifData_CreateDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Photos", "ExifData_CreateDate");
            DropColumn("dbo.Photos", "ExifData_Height");
            DropColumn("dbo.Photos", "ExifData_Width");
        }
    }
}
