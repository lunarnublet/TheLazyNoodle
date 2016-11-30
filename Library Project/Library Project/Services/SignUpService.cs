using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library_Project.Services
{
    public class SignUpService
    {
        private string username;
        private string password;

        public SignUpService(string username, string password)
        {
            this.username = username;
            this.password = password;
        }

        private bool IsValidPassword()
        {
            // dallin: this could be changed to force certain password criteria
            // e.g. length >= n
            return true;
        }

        private bool IsUniqueUserName()
        {
            bool isUnique = false;
            using (TheLazyNoodleEntities1 context = new TheLazyNoodleEntities1())
            {
                try
                {
                    var user = context.UserProfiles.SingleOrDefault(u => u.username == this.username);

                    if (user == null)
                    {
                        isUnique = true;
                    }
                }
                catch (InvalidOperationException)
                {
                    // dallin: i believe this exception throws if somehow, there are multiple users with the same name
                }
            }

            return isUnique;
        }

        public void Persist()
        {
            // TODO(dallin): throw exceptions or not?
            if (!IsValidPassword())
            {
                throw new InvalidOperationException("Password was not valid");
            }
            if (!IsUniqueUserName())
            {
                throw new InvalidOperationException("Username was not unique");
            }

            // TODO(dallin): class member context?
            using (TheLazyNoodleEntities1 context = new TheLazyNoodleEntities1())
            {
                var role = context.Roles.SingleOrDefault(x => x.roleName == "General");

                var user = new UserProfile()
                {
                    username = this.username,
                    password = this.password,
                    Roles = new List<Role>() { role }
                    
                };

                context.UserProfiles.Add(user);
                context.SaveChanges();
            }
        }
    }
}