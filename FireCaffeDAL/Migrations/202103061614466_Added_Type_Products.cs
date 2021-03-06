namespace FireCaffeDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_Type_Products : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Product", "Type", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Product", "Type");
        }
    }
}
