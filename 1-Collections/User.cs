using System;

namespace Collections
{
    public class User : IUser
    {
        public User(string fullName, string username, uint? age)
        {
            // throw new NotImplementedException("TODO ensure that username is not null");
            // throw new NotImplementedException("TODO initialise this instance of user accordingly");
            if (fullName == null)
            {
                throw new ArgumentNullException();
            }
            Age = age;
            FullName = fullName;
            Username = username; 
        }
        
        public uint? Age { get; }
        
        public string FullName { get; }
        
        public string Username { get; }

        public bool IsAgeDefined => Age.HasValue;
        
        // TODO implement missing methods (try to autonomously figure out which are the necessary methods)
        
        public override string ToString() => $"Name = {FullName}, Username = {Username}, Age = {Age}";

        public override int GetHashCode() => HashCode.Combine(FullName, Username, Age); 
    }
}
