namespace FireCaffeDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DB_INITIAL : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Client",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(maxLength: 150),
                        LastName = c.String(maxLength: 150),
                        Username = c.String(maxLength: 150),
                        Password = c.String(maxLength: 150),
                        Admin = c.Int(nullable: false),
                        GoldenCups = c.Int(nullable: false),
                        SilverCups = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Int(nullable: false),
                        Description = c.String(),
                        Size = c.String(),
                        Sale_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sales", t => t.Sale_Id)
                .Index(t => t.Sale_Id);
            
            CreateTable(
                "dbo.Sales",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SilverCups = c.Int(nullable: false),
                        SaleTime = c.DateTime(nullable: false),
                        ClientId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Client", t => t.ClientId, cascadeDelete: true)
                .Index(t => t.ClientId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Product", "Sale_Id", "dbo.Sales");
            DropForeignKey("dbo.Sales", "ClientId", "dbo.Client");
            DropIndex("dbo.Sales", new[] { "ClientId" });
            DropIndex("dbo.Product", new[] { "Sale_Id" });
            DropTable("dbo.Sales");
            DropTable("dbo.Product");
            DropTable("dbo.Client");
        }
    }
}
