namespace TmspRoster.Migrations.SqlDbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Members",
                c => new
                    {
                        MemberID = c.Int(nullable: false, identity: true),
                        LastName = c.String(),
                        FirstMidName = c.String(),
                        City = c.String(),
                        Territory = c.Int(nullable: false),
                        JoinDate = c.DateTime(),
                        Status = c.Int(nullable: false),
                        PlateNumber = c.String(),
                        Tmsp_TmspID = c.Int(),
                    })
                .PrimaryKey(t => t.MemberID)
                .ForeignKey("dbo.Tmsps", t => t.Tmsp_TmspID)
                .Index(t => t.Tmsp_TmspID);
            
            CreateTable(
                "dbo.Tmsps",
                c => new
                    {
                        TmspID = c.Int(nullable: false),
                        Link = c.String(),
                    })
                .PrimaryKey(t => t.TmspID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Members", "Tmsp_TmspID", "dbo.Tmsps");
            DropIndex("dbo.Members", new[] { "Tmsp_TmspID" });
            DropTable("dbo.Tmsps");
            DropTable("dbo.Members");
        }
    }
}
