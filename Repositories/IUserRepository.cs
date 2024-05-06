using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace bazy1.Models.Repositories
{
    public interface IUserRepository
    {
        bool authenticate(NetworkCredential credential);
        void add(User user);
        void remove(User user);
        void update(User user);
        User findById(int id);
        User findByUsername(string username);
        IEnumerable<User> findAll();
    }
}
