using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CM.DTOs;

namespace CM.Services.Contracts
{
    public interface IAppUserServices
    {
        List<AppUserDTO> ListUsers();
        AppUserDTO GetUser(int id);
        AppUserDTO GetUser(string name);
        AppUserDTO CreateUser(AppUserDTO dto);
        AppUserDTO DeleteUser(int id);
        AppUserDTO EditUser(int id, AppUserDTO editedUser);

        Task<AppUserDTO> AdminGetUserAsync(int id);
    }
}
