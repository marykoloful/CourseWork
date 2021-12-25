using System.Collections.Generic;
using System.Linq;

namespace CourseWork.Model.Data.Service
{
    public class UserService
    {

        readonly static SortedSet<User> users = new();
        public static bool AuthUser(string nickname, string password)
        {
            using (ApplicationContext db = new())
            {
               

                bool isAuthenticated = db.Users.Any(u => u.Nickname == nickname & u.Password == password);
                if (isAuthenticated) return true;
            }
            return false;
        }
        public static void CreateUser(string nickname, string password)
        {
            using ApplicationContext db = new();
            bool isAuthenticated = db.Users.Any(u => u.Nickname == nickname & u.Password == password);
            if (!isAuthenticated) db.Users.Add(new User() { Nickname = nickname, Password = password });
            db.SaveChanges();
        }
    }
}
