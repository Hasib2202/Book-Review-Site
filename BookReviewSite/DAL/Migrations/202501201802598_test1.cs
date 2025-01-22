namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class test1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Authors",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(),
                    Biography = c.String(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Books",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Title = c.String(),
                    Genre = c.String(),
                    Description = c.String(),
                    AuthorId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Authors", t => t.AuthorId, cascadeDelete: true)
                .Index(t => t.AuthorId);

            CreateTable(
                "dbo.Reviews",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Content = c.String(),
                    Rating = c.Int(nullable: false),
                    CreatedAt = c.DateTime(nullable: false),
                    UserId = c.Int(nullable: false),
                    BookId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Books", t => t.BookId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.BookId);

            CreateTable(
                "dbo.Users",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(),
                    Username = c.String(),
                    Email = c.String(),
                    Password = c.String(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Votes",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    IsUpvote = c.Boolean(nullable: false),
                    UserId = c.Int(nullable: false),
                    ReviewId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Reviews", t => t.ReviewId, cascadeDelete: false) // Change cascadeDelete to false
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: false) // Change cascadeDelete to false
                .Index(t => t.UserId)
                .Index(t => t.ReviewId);

            CreateTable(
                "dbo.Recommendations",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    UserId = c.Int(nullable: false),
                    BookId = c.Int(nullable: false),
                    RecommendedAt = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Books", t => t.BookId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.BookId);
        }

        public override void Down()
        {
            DropForeignKey("dbo.Recommendations", "UserId", "dbo.Users");
            DropForeignKey("dbo.Recommendations", "BookId", "dbo.Books");
            DropForeignKey("dbo.Reviews", "UserId", "dbo.Users");
            DropForeignKey("dbo.Votes", "UserId", "dbo.Users");
            DropForeignKey("dbo.Votes", "ReviewId", "dbo.Reviews");
            DropForeignKey("dbo.Reviews", "BookId", "dbo.Books");
            DropForeignKey("dbo.Books", "AuthorId", "dbo.Authors");
            DropIndex("dbo.Recommendations", new[] { "BookId" });
            DropIndex("dbo.Recommendations", new[] { "UserId" });
            DropIndex("dbo.Votes", new[] { "ReviewId" });
            DropIndex("dbo.Votes", new[] { "UserId" });
            DropIndex("dbo.Reviews", new[] { "BookId" });
            DropIndex("dbo.Reviews", new[] { "UserId" });
            DropIndex("dbo.Books", new[] { "AuthorId" });
            DropTable("dbo.Recommendations");
            DropTable("dbo.Votes");
            DropTable("dbo.Users");
            DropTable("dbo.Reviews");
            DropTable("dbo.Books");
            DropTable("dbo.Authors");
        }
    }
}
