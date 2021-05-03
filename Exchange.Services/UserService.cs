using Exchange.Data;
using Exchange.Data.Models;
using System.Collections.Generic;

namespace Exchange.Services
{
    public class UserService
    {
        private readonly UnitOfWork<AppDbContext> _unitOfWork;

        public UserService(UnitOfWork<AppDbContext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public User FindUserById(int id) => _unitOfWork.GetRepository<User>().Get(x => x.Id == id);

        public IEnumerable<User> FindAllUsers() => _unitOfWork.GetRepository<User>().GetAll();
    }
}
