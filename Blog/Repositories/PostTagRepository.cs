using Blog.Data;
using Blog.Models;
using Blog.Repositories.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Blog.Repositories
{
    public class PostTagRepository : IPostTagRepository
    {
        private readonly SqlConnection _connection;

        public PostTagRepository(ConnectionDB connection)
        {
            _connection = connection.GetConnection();
        }

        public async Task AddTagToPost(int postId, int tagId)
        {
            var sql = @"INSERT INTO PostTag (PostId, TagId)
                        VALUES (@PostId, @TagId)";

            await _connection.ExecuteAsync(sql, new { PostId = postId, TagId = tagId });
        }

        public async Task<List<Post>> GetPostsByTagId(int tagId)
        {
            var sql = @"SELECT p.*
                        FROM PostTag pt
                        JOIN [Post] p ON p.Id = pt.PostId
                        WHERE pt.TagId = @TagId";

            return (await _connection.QueryAsync<Post>(sql, new { TagId = tagId })).ToList();
        }

        public async Task<List<Tag>> GetTagsByPostId(int postId)
        {
            var sql = @"SELECT t.*
                        FROM PostTag pt
                        JOIN Tag t ON t.Id = pt.TagId
                        WHERE pt.PostId = @PostId";

            return (await _connection.QueryAsync<Tag>(sql, new { PostId = postId })).ToList();
        }

        public async Task RemoveTagFromPost(int postId, int tagId)
        {
            var sql = @"DELETE FROM PostTag
                        WHERE PostId = @PostId AND TagId = @TagId";

            await _connection.ExecuteAsync(sql, new { PostId = postId, TagId = tagId });
        }
    }
}
