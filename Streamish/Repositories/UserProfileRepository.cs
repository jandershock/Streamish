using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Streamish.Models;
using System.Collections.Generic;
using System.Reflection;

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
                    foreach(PropertyInfo propertyInfo in userProfile.GetType().GetProperties())
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
            throw new System.NotImplementedException();
        }

        public UserProfile Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public List<UserProfile> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public void Update(UserProfile userProfile)
        {
            throw new System.NotImplementedException();
        }
    }
}
