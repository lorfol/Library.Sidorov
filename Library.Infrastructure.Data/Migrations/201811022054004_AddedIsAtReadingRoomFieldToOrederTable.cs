namespace Library.Infrastructure.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedIsAtReadingRoomFieldToOrederTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "IsAtReadingRoom", c => c.Boolean(nullable: false));
            DropColumn("dbo.Books", "Status");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Books", "Status", c => c.Int(nullable: false));
            DropColumn("dbo.Orders", "IsAtReadingRoom");
        }
    }
}