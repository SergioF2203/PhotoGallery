﻿namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v11 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Albums", "ImagePath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Albums", "ImagePath");
        }
    }
}
