namespace AxadoTest.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Carriers",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Created = c.DateTime(nullable: false),
                        Updated = c.DateTime(nullable: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CarrierRates",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        CarrierId = c.Long(nullable: false),
                        Rate = c.Int(nullable: false),
                        UserId = c.String(),
                        Created = c.DateTime(nullable: false),
                        Updated = c.DateTime(nullable: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Carriers", t => t.CarrierId, cascadeDelete: true)
                .Index(t => t.CarrierId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CarrierRates", "CarrierId", "dbo.Carriers");
            DropIndex("dbo.CarrierRates", new[] { "CarrierId" });
            DropTable("dbo.CarrierRates");
            DropTable("dbo.Carriers");
        }
    }
}
