namespace PzProj.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.hosts",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        ip_addr = c.String(),
                        status = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.users",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        login = c.String(),
                        password = c.String(),
                        mail = c.String(),
                        superuser = c.Int(nullable: false),
                        status = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.users");
            DropTable("dbo.hosts");
        }
    }
}
