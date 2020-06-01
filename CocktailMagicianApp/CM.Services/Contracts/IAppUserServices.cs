using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CM.DTOs;

namespace CM.Services.Contracts
{
    public interface IAppUserServices
    {
        public Task<AppUserDTO> GetUserAsync(Guid id);
        public Task<List<AppUserDTO>> GetAllUsersAsync();
        public Task<AppUserDTO> CreateUserAsync(AppUserDTO appUserDto);
        public Task<AppUserDTO> DeleteUserAsync(Guid id);
		Task<AppUserDTO> GetUserIdAsync(string name);
	}
}
