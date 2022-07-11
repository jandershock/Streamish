using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Streamish.Models;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using Streamish.Utils;

namespace Streamish.Repositories
{
    public class UserProfileRepository : BaseRepository, IUserProfileRepository
    {
        public UserProfileRepository(IConfiguration configuration) : base(configuration) { }
        public void Add(UserProfile userProfile)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            INSERT INTO UserProfile ([Name], Email, ImageUrl, DateCreated)
                            VALUES (@Name, @Email, @ImageUrl, @DateCreated)";
                    foreach (PropertyInfo propertyInfo in userProfile.GetType().GetProperties())
                    {
                        if (propertyInfo.Name == "Id")
                        {
                            continue;
                        }
                        cmd.Parameters.AddWithValue($"@{propertyInfo.Name}", propertyInfo.GetValue(userProfile));
                    }
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                DELETE FROM UserProfile
                                WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public UserProfile Get(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT Id, Name, Email, ImageUrl, DateCreated
                                        FROM UserProfile
                                        WHERE UserProfile.Id = @id";
                    cmd.Parameters.AddWithValue("@id", id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        UserProfile userProfile = null;
                        if (reader.Read())
                        {
                            userProfile = new UserProfile()
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                Name = DbUtils.GetString(reader, "Name"),
                                Email = DbUtils.GetString(reader, "Email"),
                                ImageUrl = DbUtils.GetString(reader, "ImageUrl"),
                                DateCreated = DbUtils.GetDateTime(reader, "DateCreated")
                            };
                        }
                        return userProfile;
                    }
                }

            }
        }

        public List<UserProfile> GetAll()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT Id, Name, Email, ImageUrl, DateCreated
                                        FROM UserProfile";
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<UserProfile> userProfiles = new List<UserProfile>();
                        while (reader.Read())
                        {
                            userProfiles.Add(new UserProfile()
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                Name = DbUtils.GetString(reader, "Name"),
                                Email = DbUtils.GetString(reader, "Email"),
                                ImageUrl = DbUtils.GetString(reader, "ImageUrl"),
                                DateCreated = DbUtils.GetDateTime(reader, "DateCreated")
                            });
                        }
                        return userProfiles;
                    }
                }
            }
        }

        public void Update(UserProfile userProfile)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE UserProfile
                                        SET Name=@Name, Email=@Email, ImageUrl=@ImageUrl, DateCreated=@DateCreated
                                        WHERE UserProfile.Id = @Id";
                    foreach (PropertyInfo p in userProfile.GetType().GetProperties())
                    {
                        DbUtils.AddParameter(cmd, $"@{p.Name}", p.GetValue(userProfile));
                    }
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public UserProfile GetUserProfileWithAuthoredVideos(int userId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT UserProfile.Id, [Name], Email, ImageUrl, UserProfile.DateCreated,

                                               Video.Id AS VideoId, Video.Title, Video.[Description], Video.[Url], Video.DateCreated AS VideoDateCreated,

                                               Comment.Id AS CommentId, Comment.[Message], Comment.VideoId, Comment.UserProfileId
                                        FROM UserProfile
                                        JOIN Video ON Video.UserProfileId = UserProfile.Id
                                        LEFT JOIN Comment ON Comment.VideoId = Video.Id
                                        WHERE UserProfile.Id = @userId";
                    cmd.Parameters.AddWithValue("@userId", userId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        UserProfile userProfile = null;
                        while (reader.Read())
                        {
                            if (userProfile == null)
                            {
                                userProfile = DbUtils.ExtractUserProfile(reader, "Id", "Name", "Email", "ImageUrl", "DateCreated");
                                userProfile.AuthoredVideos = new List<Video>();
                            }

                            int videoId = DbUtils.GetInt(reader, "VideoId");
                            Video existingVideo = userProfile.AuthoredVideos.FirstOrDefault(p => p.Id == videoId);
                            if (existingVideo == null)
                            {
                                existingVideo = new Video()
                                {
                                    Id = DbUtils.GetInt(reader, "VideoId"),
                                    Title = DbUtils.GetString(reader, "Title"),
                                    Description = DbUtils.GetString(reader, "Description"),
                                    Url = DbUtils.GetString(reader, "Url"),
                                    DateCreated = DbUtils.GetDateTime(reader, "VideoDateCreated"),
                                    Comments = new List<Comment>(),
                                    UserProfile = DbUtils.ExtractUserProfile(reader, "Id", "Name", "Email", "ImageUrl", "DateCreated"),
                                    UserProfileId = DbUtils.GetInt(reader, "Id")
                                };
                                userProfile.AuthoredVideos.Add(existingVideo);
                            }

                            if (DbUtils.IsNotDbNull(reader, "CommentId"))
                            {
                                existingVideo.Comments.Add(DbUtils.ExtractComment(reader, "CommentId", "Message", "VideoId", "UserProfileId"));
                            }
                        }
                        return userProfile;
                    }
                }
            }
        }
    }
}
