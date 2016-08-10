namespace PostFeed.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedRequiredTags : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Authors", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Posts", "BodyText", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Posts", "BodyText", c => c.String());
            AlterColumn("dbo.Authors", "Name", c => c.String());
        }
    }
}
