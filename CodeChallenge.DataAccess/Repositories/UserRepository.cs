using CodeChallenge.DataAccess.Entities;
using CodeChallenge.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeChallenge.DataAccess.Repositories;

public class UserRepository : IUserRepository
{
    private List<User> _users = new List<User>
        {
            new User
            {
                Id = 1, Username = "user1", Password = "1"
            },
            new User
            {
                Id = 2, Username = "user2", Password = "2"
            }
        };

    public async Task<bool> FindAsync(string username, string password)
    {
        var user = _users.SingleOrDefault(x => x.Username == username && x.Password == password);

        return await Task.FromResult(user != null);
    }

}
