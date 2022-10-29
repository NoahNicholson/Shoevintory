using Microsoft.Data.SqlClient;
using Shoevintory.Models;
using System.Collections.Generic;


namespace Shoevintory.Repositories
{
    public interface IUserProfileRepository
    {
        SqlConnection Connection { get; }

        void Add(UserProfile userProfile);
        List<UserProfile> GetAllUserProfiles();
        UserProfile GetByFirebaseUserId(string firebaseUserId);
    }
}