namespace Data.Store.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGuidForDevice : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Devices", "Guid", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Devices", "Guid");
        }
    }
}
