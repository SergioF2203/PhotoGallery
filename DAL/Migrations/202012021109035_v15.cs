namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v15 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Photos", "ThumbnailPath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Photos", "ThumbnailPath");
        }
    }
}
