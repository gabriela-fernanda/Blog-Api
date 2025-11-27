namespace Blog.Models
{
    public class Tag
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Slug { get; private set; }

        public Tag() { }

        public Tag(string name, string slug)
        {
            Name = name;
            Slug = slug;
        }
    }
}
