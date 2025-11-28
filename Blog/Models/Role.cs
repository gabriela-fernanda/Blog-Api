namespace Blog.Models
{
    public class Role
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Slug { get; private set; }

        public List<User> Users { get; set; }

        public Role()
        {
            Users = new List<User>();
        }

        public Role(string name, string slug)
        {
            Name = name;
            Slug = slug;
            Users = new List<User>();
        }
    }
}
