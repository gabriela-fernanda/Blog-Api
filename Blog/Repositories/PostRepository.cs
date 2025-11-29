using Blog.Data;
using Blog.Models;
using Blog.Models.DTOs;
using Blog.Repositories.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Blog.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly SqlConnection _connection;

        public PostRepository(ConnectionDB connection)
        {
            _connection = connection.GetConnection();
        }

        public async Task<List<PostResponseDTO>> GetAllPostsAsync()
        {
            var sql = "SELECT * FROM Post";
            return (await _connection.QueryAsync<PostResponseDTO>(sql)).ToList();
        }

        public async Task<Post> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM Post WHERE Id = @Id";

            var result = await _connection.QueryFirstOrDefaultAsync<Post>(sql, new { Id = id });

            return result;
        }

        public async Task CreatePostAsync(Post post)
        {
            var sql = @"INSERT INTO Post (CategoryId, AuthorId, Title, Summary, Body, Slug, CreateDate, LastUpdateDate)
                        VALUES (@CategoryId, @AuthorId, @Title, @Summary, @Body, @Slug, @CreateDate, @LastUpdateDate)";
            await _connection.ExecuteAsync(sql, new { post.CategoryId, post.AuthorId, post.Title, post.Summary, post.Body, post.Slug, post.CreateDate, post.LastUpdateDate});
        }

        public async Task UpdatePostAsync(int id, Post post)
        {
            var sql = @"UPDATE Post SET 
                        CategoryId = @CategoryId, 
                        AuthorId = @AuthorId, 
                        Title = @Title, 
                        Summary = @Summary, 
                        Body = @Body, 
                        Slug = @Slug, 
                        CreateDate = @CreateDate, 
                        LastUpdateDate = @LastUpdateDate 
                        WHERE Id = @Id";

            await _connection.ExecuteAsync(sql, new { Id = id, post.CategoryId, post.AuthorId, post.Title, post.Summary, post.Body, post.Slug, post.CreateDate, post.LastUpdateDate});
        }

        public async Task DeletePostAsync(int id)
        {
            var sql = "DELETE FROM Post WHERE Id = @Id";

            await _connection.ExecuteAsync(sql, new { Id = id });
        }

        public async Task<List<Post>> GetAllPostTags()
        {
            var sql = @"SELECT *
                        FROM [Post] p
                        JOIN [PostTag] pt
                        ON p.id = pt.PostId 
                        JOIN [Tag] t
                        ON t.Id = pt.TagId";

            IEnumerable<Post> postTags = await _connection.QueryAsync<Post, Tag, Post>(
                sql,
                (post, tag) =>
                {
                    post.Tags.Add(tag);
                    return post;
                },
                splitOn: "Id"
            );

            var result = postTags
                .GroupBy(p => p.Id)
                .Select(g =>
                {
                    var groupedPost = g.First();
                    groupedPost.Tags = g.SelectMany(u => u.Tags)
                                         .DistinctBy(r => r.Id)
                                         .ToList();
                    return groupedPost;
                })
                .ToList();

            return result;
        }

        public async Task<Post> GetPostTagsById(int id)
        {
            var sql = @"SELECT *
                        FROM [Post] p
                        JOIN [PostTag] pt
                        ON p.id = pt.PostId 
                        JOIN [Tag] t
                        ON t.Id = pt.TagId
                        WHERE p.Id = @Id";

            IEnumerable<Post> postTags = await _connection.QueryAsync<Post, Tag, Post>(
                sql,
                (post, tag) =>
                {
                    post.Tags.Add(tag);
                    return post;
                },
                new { Id = id },
                splitOn: "Id"
            );

            var result = postTags
                .GroupBy(p => p.Id)
                .Select(g =>
                {
                    var groupedUser = g.First();
                    groupedUser.Tags = g.SelectMany(p => p.Tags)
                                         .DistinctBy(t => t.Id)
                                         .ToList();

                    return groupedUser;
                })
                .FirstOrDefault();

            return result;
        }
    }
}
