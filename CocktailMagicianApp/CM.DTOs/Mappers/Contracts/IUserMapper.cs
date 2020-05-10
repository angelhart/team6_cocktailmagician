using System;
using System.Collections.Generic;
using System.Text;
using CM.Models;

namespace CM.DTOs.Mappers.Contracts
{
    public interface IUserMapper
    {
        AppUserDTO CreateAppUserDTO(AppUser user);
    }
}
