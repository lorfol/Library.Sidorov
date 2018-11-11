namespace Library.Infrastructure.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeReturnDateToNullableInOrdersTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "ReturnDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "ReturnDate", c => c.DateTime(nullable: false));
        }
    }
}
