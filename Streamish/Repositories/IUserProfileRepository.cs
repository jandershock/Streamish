using Streamish.Models;
using System.Collections.Generic;

namespace Streamish.Repositories
{
    public interface IUserProfileRepository
    {
        List<UserProfile> GetAll();
        UserProfile Get(int id);
        UserProfile GetUserProfileWithAuthoredVideos(int userId);
        void Add(UserProfile userProfile);
        void Update(UserProfile userProfile);
        void Delete(int id);
        UserProfile GetByFirebaseUserId(string firebaseUserId);
    }
}
