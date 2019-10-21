namespace MyShop.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSpecification : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Specification", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "Specification");
        }
    }
}
