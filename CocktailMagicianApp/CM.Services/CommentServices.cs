using CM.Data;
using CM.DTOs;
using CM.DTOs.Mappers.Contracts;
using CM.Models;
using CM.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CM.Services
{
    public class CommentServices : ICommentServices
    {
        private readonly CMContext _context;
        private readonly ICocktailMapper _cocktailMapper;
        private readonly IBarMapper _barMapper;

        public CommentServices(CMContext context,
                               ICocktailMapper cocktailMapper,
                               IBarMapper barMapper)
        {
            this._context = context;
            this._cocktailMapper = cocktailMapper;
            this._barMapper = barMapper;
        }

        //TODO Test bar comments

        /// <summary>
        /// Adds new Comment to the collection of comments of a Bar.
        /// </summary>
        /// <param name="barCommentDTO">The params need for the Comment to be created.</param>
        public async Task<BarCommentDTO> AddBarCommentAsync(BarCommentDTO newCommentDto)
        {
            if (newCommentDto == null)
                throw new ArgumentNullException("New comment object cannot be null");

            var newComment = _barMapper.CreateBarComment(newCommentDto);

            await _context.AddAsync(newComment);
            await _context.SaveChangesAsync();

            var outputDto = _barMapper.CreateBarCommentDTO(newComment);

            return outputDto;
        }

        /// <summary>
        /// Returns a comment by given Id.
        /// </summary>
        /// <param name="commentId">The Id of the comment.</param>
        /// <returns>BarCommentDTO</returns>
        public async Task<BarCommentDTO> GetBarCommentAsync(Guid commentId)
        {
            var comment = await _context.BarComments.FirstOrDefaultAsync(comment => comment.Id == commentId);

            var outputDto = _barMapper.CreateBarCommentDTO(comment);

            return outputDto;
        }

        /// <summary>
        /// Edits the text of a comment by given Id.
        /// </summary>
        /// <param name="commentId">The Id of the comment that should be edited.</param>
        /// <param name="text">The new text of the comment.</param>
        /// <returns></returns>
        public async Task<BarCommentDTO> EditBarCommentAsync(Guid commentId, string text)
        {
            var comment = await _context.BarComments.FirstOrDefaultAsync(comment => comment.Id == commentId);

            if (text == null || comment == null)
                throw new ArgumentNullException();

            comment.Text = text;

            _context.Update(comment);
            await _context.SaveChangesAsync();

            var outputDto = _barMapper.CreateBarCommentDTO(comment);

            return outputDto;
        }

        /// <summary>
        /// Deletes a comment by give Id.
        /// </summary>
        /// <param name="commentId">The Id of the comment that should be deleted.</param>
        /// <returns></returns>
        public async Task<BarCommentDTO> DeleteBarCommentAsync(Guid commentId)
        {
            var comment = await _context.BarComments.FirstOrDefaultAsync(comment => comment.Id == commentId);

            _context.Remove(comment);
            await _context.SaveChangesAsync();

            var outputDto = _barMapper.CreateBarCommentDTO(comment);

            return outputDto;
        }

        private async Task<CocktailComment> GetCocktailCommentEntityAsync(Guid commentId)
        {
            var entry = await _context.CocktailComments.FindAsync(commentId);

            if (entry == null)
                throw new NullReferenceException("No comment with this ID found.");

            return entry;
        }



        /// <summary>
        /// Adds a new comment for a cocktail.
        /// </summary>
        /// <param name="dto">Object containing IDs for commenter and cocktail along with text content.</param>
        /// <returns><see cref="CocktailCommentDTO"/></returns>
        public async Task<CocktailCommentDTO> AddCocktailCommentAsync(CocktailCommentDTO dto)
        {
            if (dto == null)
                throw new ArgumentNullException("New comment object cannot be null");

            var newComment = new CocktailComment
            {
                CocktailId = dto.CocktailId,
                AppUserId = dto.UserId,
                CommentedOn = dto.CommentedOn,
                Text = dto.Text
            };

            await _context.AddAsync(newComment);
            await _context.SaveChangesAsync();

            var outputDto = _cocktailMapper.CreateCocktailCommentDTO(newComment);

            return outputDto;
        }

        /// <summary>
        /// Provides information for the specified cocktail comment.
        /// </summary>
        /// <param name="commentId">Id of the queried comment.</param>
        /// <returns><see cref="CocktailCommentDTO"/></returns>
        public async Task<CocktailCommentDTO> GetCocktailCommentAsync(Guid commentId)
        {
            var comment = await GetCocktailCommentEntityAsync(commentId);

            var outputDto = _cocktailMapper.CreateCocktailCommentDTO(comment);

            return outputDto;
        }

        /// <summary>
        /// Edits the text of an existing cocktail comment.
        /// </summary>
        /// <param name="commentId">ID of the cocktail comment to be edited.</param>
        /// <param name="text">New text content.</param>
        /// <returns><see cref="CocktailCommentDTO"/></returns>
        public async Task<CocktailCommentDTO> EditCocktailCommentAsync(Guid commentId, string text)
        {
            var comment = await GetCocktailCommentEntityAsync(commentId);

            comment.Text = text;

            _context.Update(comment);
            await _context.SaveChangesAsync();

            var outputDto = _cocktailMapper.CreateCocktailCommentDTO(comment);

            return outputDto;
        }

        /// <summary>
        /// Deletes a comment for a cocktail from the database.
        /// </summary>
        /// <param name="commentId">Id of comment to be removed</param>
        /// <returns><see cref="CocktailCommentDTO"/></returns>
        public async Task<CocktailCommentDTO> DeleteCocktailCommentAsync(Guid commentId)
        {
            var comment = await GetCocktailCommentEntityAsync(commentId);

            _context.Remove(comment);
            await _context.SaveChangesAsync();

            var outputDto = _cocktailMapper.CreateCocktailCommentDTO(comment);

            return outputDto;
        }
    }
}
