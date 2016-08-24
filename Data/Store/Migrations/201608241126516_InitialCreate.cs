namespace Data.Store.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Temperatures",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Value = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Date);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Temperatures", new[] { "Date" });
            DropTable("dbo.Temperatures");
        }
    }
}
