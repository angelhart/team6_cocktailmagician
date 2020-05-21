using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CM.DTOs;

namespace CM.Services.Contracts
{
    public interface ICommentServices
    {
        public Task<BarCommentDTO> AddBarComment(int id, BarCommentDTO barCommentDTO);
    }
}
