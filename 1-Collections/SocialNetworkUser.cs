using System;
using System.Collections.Generic;

namespace Collections
{
    public class SocialNetworkUser<TUser> : User, ISocialNetworkUser<TUser>
        where TUser : IUser
    {
        IDictionary<string, List<TUser>> _followedUsers;
        public SocialNetworkUser(string fullName, string username, uint? age) : base(fullName, username, age)
        {
            // throw new NotImplementedException("TODO is there anything to do here?");
            _followedUsers = new Dictionary<string, List<TUser>>();
        }

        public bool AddFollowedUser(string group, TUser user)
        {
            // throw new NotImplementedException("TODO add user to the provided group. Return false if the user was already in the group");
            if (_followedUsers.ContainsKey(group))
            {
                _followedUsers[group].Add(user);
                return false;
            }
            else
            {
                _followedUsers.Add(group, new List<TUser> {user});
                return true;
            }
        }

        public IList<TUser> FollowedUsers
        {
            get
            {
                // throw new NotImplementedException("TODO construct and return the list of all users followed by the current users, in all groups");
                var allUsers = new List<TUser>();
                foreach (var key in _followedUsers.Keys)
                {
                    allUsers.AddRange(_followedUsers[key].AsReadOnly());
                }
                return allUsers;
            }
        }

        public ICollection<TUser> GetFollowedUsersInGroup(string group)
        {
            if (_followedUsers.ContainsKey(group))
            {
                return _followedUsers[group].AsReadOnly();
            }
            else
            {
                return new List<TUser>();
            }
        }
    }
}
