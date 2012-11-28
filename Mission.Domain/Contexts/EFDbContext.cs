using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using Mission.Domain.Entities;
using Mission.Domain.Entities.FakeData;


namespace Mission.Domain.Contexts
{
    public class EFDbContext : DbContext
    {
        public DbSet<Customers> Customers { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventQuestion> EventQuestions { get; set; }
        public DbSet<Answer> Answeres { get; set; }
        public DbSet<Subscriber> Subscribers { get; set; }
        public DbSet<Words> Words { get; set; }
        public DbSet<AboutJesper> AboutJesper { get; set; }
        public DbSet<Newsletter> NewsLetter { get; set; }
    }

    public class MissionInitializer : DropCreateDatabaseIfModelChanges<EFDbContext>
    {
        

        protected override void Seed(EFDbContext context)
        {
            var fakeData = new FakeData();

            var aboutjesper = new AboutJesper { ID = Guid.NewGuid(), Content = "" };
            context.AboutJesper.Add(aboutjesper);
            
            context.SaveChanges();
            fakeData.testPostList().ForEach(s => context.Posts.Add(s));
            fakeData.testNewsList().ForEach(s => context.Posts.Add(s));
            fakeData.testEventList().ForEach(s => context.Events.Add(s));
            fakeData.testCommentList().ForEach(s => context.Comments.Add(s));
            fakeData.testEventQuestionList().ForEach(s => context.EventQuestions.Add(s));
            User JesperSomAdmin = new User { UserName = "Jesper", Salt = "$2a$10$/XJG8qJZgw4ZdbV.k/Tne.", PasswordHash = "$2a$10$/XJG8qJZgw4ZdbV.k/Tne.GNk94mOT7f1AuhaW.v2rj5qmJ1j2fF.", Role = 1, UserEmailAddress = "JesperDude@ngnmail.com", ID = Guid.NewGuid() };
            context.Users.Add(JesperSomAdmin);
            context.SaveChanges();
        }
    }
}
