namespace FishFactoryServiceImplementDataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CannedFoods",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CannedFoodName = c.String(nullable: false),
                        Cost = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Requests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        CannedFoodId = c.Int(nullable: false),
                        Total = c.Int(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Status = c.Int(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                        DateImplement = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CannedFoods", t => t.CannedFoodId, cascadeDelete: true)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId)
                .Index(t => t.CannedFoodId);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerFIO = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TypeOfCanneds",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CannedFoodId = c.Int(nullable: false),
                        TypeOfFishId = c.Int(nullable: false),
                        Total = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CannedFoods", t => t.CannedFoodId, cascadeDelete: true)
                .ForeignKey("dbo.TypeOfFish", t => t.TypeOfFishId, cascadeDelete: true)
                .Index(t => t.CannedFoodId)
                .Index(t => t.TypeOfFishId);
            
            CreateTable(
                "dbo.TypeOfFish",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TypeOfFishName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StorageFish",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StorageId = c.Int(nullable: false),
                        TypeOfFishId = c.Int(nullable: false),
                        Total = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Storages", t => t.StorageId, cascadeDelete: true)
                .ForeignKey("dbo.TypeOfFish", t => t.TypeOfFishId, cascadeDelete: true)
                .Index(t => t.StorageId)
                .Index(t => t.TypeOfFishId);
            
            CreateTable(
                "dbo.Storages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StorageName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TypeOfCanneds", "TypeOfFishId", "dbo.TypeOfFish");
            DropForeignKey("dbo.StorageFish", "TypeOfFishId", "dbo.TypeOfFish");
            DropForeignKey("dbo.StorageFish", "StorageId", "dbo.Storages");
            DropForeignKey("dbo.TypeOfCanneds", "CannedFoodId", "dbo.CannedFoods");
            DropForeignKey("dbo.Requests", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Requests", "CannedFoodId", "dbo.CannedFoods");
            DropIndex("dbo.StorageFish", new[] { "TypeOfFishId" });
            DropIndex("dbo.StorageFish", new[] { "StorageId" });
            DropIndex("dbo.TypeOfCanneds", new[] { "TypeOfFishId" });
            DropIndex("dbo.TypeOfCanneds", new[] { "CannedFoodId" });
            DropIndex("dbo.Requests", new[] { "CannedFoodId" });
            DropIndex("dbo.Requests", new[] { "CustomerId" });
            DropTable("dbo.Storages");
            DropTable("dbo.StorageFish");
            DropTable("dbo.TypeOfFish");
            DropTable("dbo.TypeOfCanneds");
            DropTable("dbo.Customers");
            DropTable("dbo.Requests");
            DropTable("dbo.CannedFoods");
        }
    }
}
