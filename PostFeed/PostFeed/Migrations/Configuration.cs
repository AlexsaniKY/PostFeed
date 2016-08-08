namespace PostFeed.Migrations
{
    using Domain;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<PostFeed.Infrastructure.AppDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(PostFeed.Infrastructure.AppDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.Authors.AddOrUpdate(
                a => a.Id,
                new Author { Id = 1, Active = true, Name = "The Great Bob" }
                );

            context.Posts.AddOrUpdate(
                p => p.Id,
                new Post { Id = 1, Active = true, AuthorId = 1, BodyText = "I ate food today", TimePosted = DateTime.Now, Title = "Food" }
                );

        }
    }
}
