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
        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }
    }

    public class MissionInitializer : DropCreateDatabaseAlways<EFDbContext>
    {
        

        protected override void Seed(EFDbContext context)
        {
           
           
            var fakeData = new FakeData();
            fakeData.testUserList().ForEach(s => context.Users.Add(s));
            context.SaveChanges();
            fakeData.testPostList().ForEach(s => context.Posts.Add(s));
            fakeData.testNewsList().ForEach(s => context.Posts.Add(s));
            User JesperSomAdmin = new User { UserName = "Jesper", Salt = "$2a$10$/XJG8qJZgw4ZdbV.k/Tne.", PasswordHash = "$2a$10$/XJG8qJZgw4ZdbV.k/Tne.GNk94mOT7f1AuhaW.v2rj5qmJ1j2fF.", Role = 1, UserEmailAddress = "JesperDude@ngnmail.com", ID = Guid.NewGuid() };

            context.Users.Add(JesperSomAdmin);

            context.SaveChanges();
        }
    }
}
