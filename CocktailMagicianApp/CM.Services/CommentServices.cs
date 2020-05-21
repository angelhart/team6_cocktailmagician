using CM.DTOs;
using CM.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CM.Services
{
    public class CommentServices : ICommentServices
    {
        /// <summary>
        /// Adds new Comment to the collection of comments of a Bar.
        /// </summary>
        /// <param name="id">Guid representing Id.</param>
        /// <param name="barCommentDTO">The params need for the Comment to be created.</param>
        /// <returns></returns>
        public async Task<BarCommentDTO> AddBarComment(int id, BarCommentDTO barCommentDTO)
        {
            throw new NotImplementedException();
        }

    }
}
