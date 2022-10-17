using WebApplication1.Models.Domain;

namespace WebApplication1.Repositories
{
    public class StaticUserRepository : IUserRepository
    {
        private List<User> users = new List<User>()
        {
            new User()
            {
                Id=Guid.NewGuid(),
                Firstname="Read Only",
                Lastname="User",
                EmailAddress= "readonly@user.com",
                UserName="readonly@user.com",
                Password="123",
                Roles=new List<string>
                {
                    "reader"
                }
            },

            new User()
            {
                Id=Guid.NewGuid(),
                Firstname="Read Write",
                Lastname="User",
                EmailAddress= "readwrite@user.com",
                UserName="readwrite@user.com",
                Password="123",
                Roles=new List<string>
                {
                    "reader","writer"
                }
            }
        };

        public async Task<User> AuthenticateUser(string username, string password)
        {
            var user= users.Find(x => x.UserName.Equals(username, StringComparison.InvariantCultureIgnoreCase) &&
            x.Password == password);

            return user;
        }
    }
}
