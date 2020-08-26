namespace BusinessLogic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Actors : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Actors",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100),
                        Birthplace = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MovieActors",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Character = c.String(nullable: false, maxLength: 100),
                        MovieId = c.Guid(nullable: false),
                        ActorId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Actors", t => t.ActorId, cascadeDelete: true)
                .ForeignKey("dbo.Movies", t => t.MovieId, cascadeDelete: true)
                .Index(t => t.MovieId)
                .Index(t => t.ActorId);
            
            DropColumn("dbo.Genres", "CreationDateUtc");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Genres", "CreationDateUtc", c => c.DateTime(nullable: false));
            DropForeignKey("dbo.MovieActors", "MovieId", "dbo.Movies");
            DropForeignKey("dbo.MovieActors", "ActorId", "dbo.Actors");
            DropIndex("dbo.MovieActors", new[] { "ActorId" });
            DropIndex("dbo.MovieActors", new[] { "MovieId" });
            DropTable("dbo.MovieActors");
            DropTable("dbo.Actors");
        }
    }
}
