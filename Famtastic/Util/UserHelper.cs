using DataAccess.Crud;
using DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Famtastic.Util
{
    public class UserHelper
    {
        private static IDbReadService _dbRead;

        public UserHelper(IDbReadService dbRead)
        {
            _dbRead = dbRead;
        }

        public static async Task<UserProfile> GetUserProfileAsync(int userProfileId)
        {
            return await _dbRead.GetSingleRecordAsync<UserProfile>(m => !m.Id.Equals(null) && m.Id.Equals(userProfileId));
        }

        public static async Task<string> GetUserReadableName(int userProfileId)
        {
            var user = await GetUserProfileAsync(userProfileId);
            return user.FirstName + " " + user.LastName;
        }
    }
}
