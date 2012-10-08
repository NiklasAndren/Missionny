using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mission.Domain.Entities.Abstract;


namespace Mission.Domain.Entities.FakeData 
{
    public class FakeData 
    {
    //Fake users
        public User firstTestUser;
        public User secondTestUser;
        public User thirdTestUser;

        public Post firstTestPost;
        public Post secondTestPost;
        public Post thirdTestPost;

        public Post firstTestNews;
        public Post secondTestNews;
        public Post thirdTestNews;
        public FakeData()
        {
            firstTestUser = new User{ UserName = "Test 1", Role = (int)Role.User, ID = Guid.NewGuid() };
            secondTestUser = new User { UserName = "Test 2", Role = (int)Role.User, ID = Guid.NewGuid() };
            thirdTestUser = new User { UserName = "Test 3", Role = (int)Role.User, ID = Guid.NewGuid() };

            firstTestPost = new Post { ID = Guid.NewGuid(), Body = "test body 1", Date = DateTime.Now, Title = "test title 1", Type = (int)Type.Blog, User = firstTestUser, UserID = firstTestUser.ID };
            secondTestPost = new Post { ID = Guid.NewGuid(), Body = "test body 2", Date = DateTime.Now, Title = "test title 2", Type = (int)Type.Blog, User = firstTestUser, UserID = firstTestUser.ID };
            thirdTestPost = new Post { ID = Guid.NewGuid(), Body = "test body 3", Date = DateTime.Now, Title = "test title 3", Type = (int)Type.Blog, User = firstTestUser, UserID = firstTestUser.ID };

            firstTestNews = new Post { ID = Guid.NewGuid(), Body = "test body 1 newwws", Date = DateTime.Now, Title = "test title 1", Type = (int)Type.News, User = firstTestUser, UserID = firstTestUser.ID };
            secondTestNews = new Post { ID = Guid.NewGuid(), Body = "test body 2 neweews", Date = DateTime.Now, Title = "test title 2", Type = (int)Type.News, User = firstTestUser, UserID = firstTestUser.ID };
            thirdTestNews = new Post { ID = Guid.NewGuid(), Body = "test body 3   news?", Date = DateTime.Now, Title = "test title 3", Type = (int)Type.News, User = secondTestUser, UserID = secondTestUser.ID };
        }


        public List<User> testUserList()
        {
            return new List<User> { firstTestUser, secondTestUser, thirdTestUser };
        }
        public List<Post> testPostList() { return new List<Post> { firstTestPost, secondTestPost, thirdTestPost }; } 

        public List<Post> testNewsList() { return new List<Post> { firstTestNews, secondTestNews, thirdTestNews };  }



    }
}
