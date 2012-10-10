using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mission.Domain.Entities.Abstract;
using Mission.Domain.Entities;


namespace Mission.Domain.Entities.FakeData 
{
    public class FakeData 
    {
    //Fake users
        public User firstTestUser;
        public User secondTestUser;
        public User thirdTestUser;
        public User JesperSomAdmin;

        public Post firstTestPost;
        public Post secondTestPost;
        public Post thirdTestPost;

        public Post firstTestNews;
        public Post secondTestNews;
        public Post thirdTestNews;

        public Event firstTestEvent;
        public Event secondTestEvent;
        public Event thirdTestEvent;

        public FakeData()
        {
            firstTestUser = new User{ UserName = "Test 1", Role = (int)Role.User, ID = Guid.NewGuid() };
            secondTestUser = new User { UserName = "Test 2", Role = (int)Role.User, ID = Guid.NewGuid() };
            thirdTestUser = new User { UserName = "Test 3", Role = (int)Role.User, ID = Guid.NewGuid() };
            //JesperSomAdmin = new User { UserName = "Jesper", Salt = "$2a$10$/XJG8qJZgw4ZdbV.k/Tne.", PasswordHash = "$2a$10$/XJG8qJZgw4ZdbV.k/Tne.GNk94mOT7f1AuhaW.v2rj5qmJ1j2fF.", Role = 1, UserEmailAddress = "JesperDude@ngnmail.com", ID = Guid.NewGuid() };

            firstTestPost = new Post { ID = Guid.NewGuid(), Body = "test body 1", Date = DateTime.Now, Title = "test title 1", Type = (int)Type.Blog, User = firstTestUser, UserID = firstTestUser.ID };
            secondTestPost = new Post { ID = Guid.NewGuid(), Body = "test body 2", Date = DateTime.Now, Title = "test title 2", Type = (int)Type.Blog, User = firstTestUser, UserID = firstTestUser.ID };
            thirdTestPost = new Post { ID = Guid.NewGuid(), Body = "test body 3", Date = DateTime.Now, Title = "test title 3", Type = (int)Type.Blog, User = firstTestUser, UserID = firstTestUser.ID };

            firstTestNews = new Post { ID = Guid.NewGuid(), Body = "test body 1 newwws", Date = DateTime.Now, Title = "test title 1", Type = (int)Type.News, User = firstTestUser, UserID = firstTestUser.ID };
            secondTestNews = new Post { ID = Guid.NewGuid(), Body = "test body 2 neweews", Date = DateTime.Now, Title = "test title 2", Type = (int)Type.News, User = firstTestUser, UserID = firstTestUser.ID };
            thirdTestNews = new Post { ID = Guid.NewGuid(), Body = "test body 3   news?", Date = DateTime.Now, Title = "test title 3", Type = (int)Type.News, User = secondTestUser, UserID = secondTestUser.ID };

            firstTestEvent = new Event {  ID = Guid.NewGuid(), City = "Göteborg", Date = DateTime.Now, HeadLine = "Test headline 1", Info = "oiqewoihewfoiewofiewohfoiwhf" };
            secondTestEvent = new Event { ID = Guid.NewGuid(), City = "Stockholm", Date = DateTime.Now, HeadLine = "Test headline 2", Info = "oi576767867867687678768f" };
            thirdTestEvent = new Event { ID = Guid.NewGuid(), City = "Bankeryd", Date = DateTime.Now, HeadLine = "Bitches & hoes", Info = "från jönköping" };
        
        }


        public List<User> testUserList()
        {
            return new List<User> { firstTestUser, secondTestUser, thirdTestUser/*, JesperSomAdmin*/ };
        }
        public List<Post> testPostList() { return new List<Post> { firstTestPost, secondTestPost, thirdTestPost }; } 

        public List<Post> testNewsList() { return new List<Post> { firstTestNews, secondTestNews, thirdTestNews };  }

        public List<Event> testEventList() { return new List<Event> { firstTestEvent, secondTestEvent, thirdTestEvent }; }


    }
}
