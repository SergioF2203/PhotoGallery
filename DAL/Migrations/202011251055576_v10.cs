namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v10 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Photos", "ExifData_Width");
            DropColumn("dbo.Photos", "ExifData_Height");
            DropColumn("dbo.Photos", "ExifData_CreateDate");
        }
        
        public override void Down()
        {
        }
    }
}
