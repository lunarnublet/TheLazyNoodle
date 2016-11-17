using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library_Project.Services
{
    public class AuthenticationService
    {
        private string userName;
        private string password;

        public AuthenticationService(string userName, string password)
        {
            this.userName = userName;
            this.password = password;
        }

        public bool IsUniqueUser()
        {
            bool isUnique = false;
            using (TheLazyNoodleEntities1 context = new TheLazyNoodleEntities1())
            {
                try
                {
                    var user = context.UserProfiles.SingleOrDefault(u => u.username == this.userName);

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

        public bool IsValidPassword()
        {
            bool isValid = false;

            using (TheLazyNoodleEntities1 context = new TheLazyNoodleEntities1())
            {
                var user = context.UserProfiles.Single(u => u.username == this.userName);

                if (user.password == this.password)
                {
                    isValid = true;
                }
            }

            return isValid;
        }

    }
}