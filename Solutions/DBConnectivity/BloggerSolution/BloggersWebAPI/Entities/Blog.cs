namespace BloggersWebAPI.Entities
{
    public class Blog
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public List<Post> Posts { get; set; }

    }
}
