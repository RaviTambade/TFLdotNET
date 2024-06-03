using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BolgPost.Entities;
using BolgPost.DBContext;

namespace BolgPost.Repositories
{
    public class BlogRepository
    {
        private BlogContext _context;
        public BlogRepository(BlogContext context)
        {
            _context = context;
        }

        public void Initialize()
        {
            using (var context = new BlogContext())
            {
                var blog = new Blog { Catagory = "Outside Explore" };
                blog.Posts = new List<Post>
                {
                    new Post { PostTitle = "At Wankhede Stadium" },
                    new Post { PostTitle = "At Satish Dhawan Space Station" },
                    new Post { PostTitle = "Exploring Melghat Sanctury" }
                };
                context.Blogs.Add(blog);
                context.SaveChanges();
            }
        }

        public void ShowAll()
        {
            using (var context = new BlogContext())
            {
                var blogsWithPosts = context.Blogs.Include(b => b.Posts).ToList();

                foreach (var blog in blogsWithPosts)
                {
                    Console.WriteLine($"Blog: {blog.Catagory}");

                    foreach (var post in blog.Posts)
                    {
                        Console.WriteLine($" - Post: {post.PostTitle}");
                    }
                }
            }
        }
    }
}
