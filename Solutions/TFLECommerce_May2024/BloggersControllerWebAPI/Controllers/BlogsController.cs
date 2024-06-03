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
                new Post { Title = "Job Guarantee Course: A Delusion.", Content = "sdfjlasjflasjldfjsaldfjlsadjflsadj" },
                new Post { Title = "Are we on right Path?", Content = "sdfyutyutyu 888888888888888888888888 adj" },
             };
            return posts;
        }
    }
}
