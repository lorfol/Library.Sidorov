namespace Library.Infrastructure.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeColumnDateTimePuplicationDateToStringPublicationYearInBooksTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "PublicationYear", c => c.String());
            DropColumn("dbo.Books", "PublicationDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Books", "PublicationDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Books", "PublicationYear");
        }
    }
}
