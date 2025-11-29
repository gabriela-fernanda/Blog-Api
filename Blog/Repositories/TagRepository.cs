using Blog.Data;
using Blog.Models;
using Blog.Models.DTOs;
using Blog.Repositories.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Blog.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly SqlConnection _connection;

        public TagRepository(ConnectionDB connection)
        {
            _connection = connection.GetConnection();
        }

        public async Task<List<TagResponseDTO>> GetAllTagsAsync()
        {
            var sql = "SELECT * FROM Tag";

            return (await _connection.QueryAsync<TagResponseDTO>(sql)).ToList();
        }

        public async Task CreateTagAsync(Tag tag)
        {
            var sql = "INSERT INTO Tag (Name, Slug) VALUES (@Name, @Slug)";
            await _connection.ExecuteAsync(sql, new { tag.Name, tag.Slug });
        }

        public async Task<Tag> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM Tag WHERE Id = @Id";
            var result = await _connection.QueryFirstOrDefaultAsync<Tag>(sql, new { Id = id });
            return result;
        }

        public async Task UpdateTagAsync(int id, Tag tag)
        {
            var sql = "UPDATE Tag SET Name = @Name, Slug = @Slug WHERE Id = @Id";
            await _connection.ExecuteAsync(sql, new { Id = id, tag.Name, tag.Slug });
        }

        public async Task DeleteTagAsync(int id)
        {
            var sql = "DELETE FROM Tag WHERE Id = @Id";
            await _connection.ExecuteAsync(sql, new { Id = id });
        }

        public async Task<List<Tag>> GetAllTagPosts()
        {
            var sql = @"SELECT *
                FROM [Tag] t
                JOIN [PostTag] pt ON t.Id = pt.TagId
                JOIN [Post] p ON p.Id = pt.PostId";

            IEnumerable<Tag> tagPosts = await _connection.QueryAsync<Tag, Post, Tag>(
                sql,
                (tag, post) =>
                {
                    tag.Posts.Add(post);
                    return tag;
                },
                splitOn: "Id"
            );

            var result = tagPosts
                .GroupBy(t => t.Id)
                .Select(g =>
                {
                    var groupedTag = g.First();
                    groupedTag.Posts = g.SelectMany(r => r.Posts)
                                         .DistinctBy(p => p.Id)
                                         .ToList();
                    return groupedTag;
                })
                .ToList();

            return result;
        }

        public async Task<Tag> GetTagPostsById(int id)
        {
            var sql = @"SELECT *
                FROM [Tag] t
                JOIN [PostTag] pt ON t.Id = pt.TagId
                JOIN [Post] p ON p.Id = pt.PostId
                WHERE t.Id = @Id";

            IEnumerable<Tag> tagPosts = await _connection.QueryAsync<Tag, Post, Tag>(
                sql,
                (tag, post) =>
                {
                    tag.Posts.Add(post);
                    return tag;
                },
                new { Id = id },
                splitOn: "Id"
            );

            var result = tagPosts
                .GroupBy(t => t.Id)
                .Select(g =>
                {
                    var groupedTag = g.First();
                    groupedTag.Posts = g.SelectMany(t => t.Posts)
                                        .DistinctBy(p => p.Id)
                                        .ToList();
                    return groupedTag;
                })
                .FirstOrDefault();

            return result;
        }
    }
}
