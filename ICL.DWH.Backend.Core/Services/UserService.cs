using ICL.DWH.Backend.Core.Entities;
using ICL.DWH.Backend.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICL.DWH.Backend.Core.Services
{
    public class UserService : IUserService
    {
		private readonly IUserRepository _userRepository;

		public UserService(IUserRepository userRepository)
		{ 
			_userRepository = userRepository;
		}

        public IEnumerable<User> GetUsers()
        {
			try
			{
				return _userRepository.GetAll();
			}
			catch (Exception)
			{

				throw;
			}
        }

        public User NewUserRegistration(User user)
        {
			try
			{
				return _userRepository.Create(user);
			}
			catch (Exception)
			{

				throw;
			}
        }
    }
}
