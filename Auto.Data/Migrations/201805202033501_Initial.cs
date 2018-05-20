namespace Auto.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CarRents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CarNumber = c.String(nullable: false, maxLength: 50),
                        RentLocation = c.String(nullable: false, maxLength: 100),
                        RentTime = c.DateTime(nullable: false),
                        ReturnLocation = c.String(maxLength: 100),
                        ReturnTime = c.DateTime(),
                        Price = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CarRents");
        }
    }
}
