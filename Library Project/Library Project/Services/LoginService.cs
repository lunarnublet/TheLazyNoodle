using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library_Project.Services
{
    public class LoginService
    {
        private string userName;
        private string password;

        public LoginService(string userName, string password)
        {
            this.userName = userName;
            this.password = password;
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

        public int GetUserId()
        {
            int id = -1;
            using (TheLazyNoodleEntities1 context = new TheLazyNoodleEntities1())
            {
                var user = context.UserProfiles.Single(u => u.username == this.userName);

                id = user.Id;
            }

            return id;
        }

    }
}