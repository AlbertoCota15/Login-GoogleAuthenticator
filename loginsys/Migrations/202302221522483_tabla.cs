namespace loginsys.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tabla : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "UsarGoogleAuthenticator", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "UsarGoogleAuthenticator");
        }
    }
}
