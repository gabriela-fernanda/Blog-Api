namespace Blog.Models.DTOs
{
    public class UserResponseDTO
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Bio { get; private set; }
        public string Image { get; private set; }
        public string Slug { get; private set; }
    }
}
