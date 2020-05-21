using CM.DTOs;
using CM.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CM.Services
{
    public class AppUserServices : IAppUserServices
    {
        public Task<AppUserDTO> AdminGetUserAsync(int id)
        {
            throw new NotImplementedException();
        }

        public AppUserDTO CreateUser(AppUserDTO dto)
        {
            throw new NotImplementedException();
        }

        public AppUserDTO DeleteUser(int id)
        {
            throw new NotImplementedException();
        }

        public AppUserDTO EditUser(int id, AppUserDTO editedUser)
        {
            throw new NotImplementedException();
        }

        public AppUserDTO GetUser(int id)
        {
            throw new NotImplementedException();
        }

        public AppUserDTO GetUser(string name)
        {
            throw new NotImplementedException();
        }

        public List<AppUserDTO> ListUsers()
        {
            throw new NotImplementedException();
        }
    }
}
