namespace Library.Infrastructure.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeIdTostringInOrdersTable : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Orders");
            DropColumn("dbo.Orders", "Id");
            AddColumn("dbo.Orders", "Id", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Orders", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Orders");
            DropColumn("dbo.Orders", "Id");
            AddColumn("dbo.Orders", "Id", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.Orders", "Id");
        }
    }
}
