using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectsMap.WebApi.Models;

namespace ProjectsMap.WebApi.Repositories.Abstract
{
    public interface IUserRepository
    {
        IEnumerable<User> Users { get; }

        User Get(int id);

        void Add(User user);

        void Delete(User user);

        void Update(User user);
    }
}
