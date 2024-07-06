using BloggersControllerWebAPI.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BloggersControllerWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {

        private readonly ILogger<BlogsController> _logger;

        public BlogsController(ILogger<BlogsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public List<Post> Get()
        {
            List<Post> posts = new List<Post>
            {
                new Post { Title = "Job Guarantee Course: A Delusion.", Content = "There is no such course which gives Job gurantee, wake up" },
                new Post { Title = "Are we on right Path?", Content = "Build your skill, Develop your  Project, Learn by doing not by watchting and reading" },
             };
            return posts;
        }
    }
}
