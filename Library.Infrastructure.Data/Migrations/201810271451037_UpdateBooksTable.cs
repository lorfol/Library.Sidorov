namespace Library.Infrastructure.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateBooksTable : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Books", "OderId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Books", "OderId", c => c.Int(nullable: false));
        }
    }
}
