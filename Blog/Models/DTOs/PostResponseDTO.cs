namespace Blog.Models.DTOs
{
    public class PostResponseDTO
    {
        public int CategoryId { get; private set; }
        public int AuthorId { get; private set; }
        public string Title { get; private set; }
        public string Summary { get; private set; }
        public string Body { get; private set; }
        public string Slug { get; private set; }
        public DateTime CreateDate { get; private set; }
        public DateTime LastUpdateDate { get; private set; }
    }
}
