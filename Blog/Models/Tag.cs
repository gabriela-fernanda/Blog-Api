namespace Blog.Models
{
    public class Tag
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Slug { get; private set; }
        public List<Post> Posts { get; set; }

        public Tag() 
        {
            Posts = new List<Post>();
        }

        public Tag(string name, string slug)
        {
            Name = name;
            Slug = slug;
            Posts = new List<Post>();
        }
    }
}
