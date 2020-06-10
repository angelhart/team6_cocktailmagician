//using CM.Data;
//using CM.DTOs;
//using CM.DTOs.Mappers.Contracts;
//using CM.Models;
//using CM.Services.Contracts;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace CM.Services
//{
//    public class AppUserServices : IAppUserServices
//    {
//		private readonly CMContext _context;
//		private readonly IUserMapper _userMapper;

//		public AppUserServices(CMContext context, IUserMapper userMapper)
//		{
//			_context = context ?? throw new ArgumentNullException(nameof(context));
//			_userMapper = userMapper ?? throw new ArgumentNullException(nameof(userMapper));
//		}

//		/// <summary>
//		/// Retrieves speciafied user by given Id.
//		/// </summary>
//		/// <param name="id">The Id of the user to be retrieved.</param>
//		/// <returns>AppUserDTO</returns>
//		public async Task<AppUserDTO> GetUserAsync(Guid id)
//		{
//			var user = await _context.Users
//				.Include(user => user.BarComments)
//				.Include(user => user.BarRatings)
//				.Include(user => user.CocktailComments)
//				.Include(user => user.CocktailRatings)
//				.FirstOrDefaultAsync(user => user.Id == id);

//			var userDTO = _userMapper.CreateAppUserDTO(user);

//			return userDTO;
//		}

//		/// <summary>
//		/// Retrieves user Id from the database by given UserName.
//		/// </summary>
//		/// <param name="name">The UserName of the User.</param>
//		/// <returns>AppUserDTO with the Id.</returns>
//		public async Task<AppUserDTO> GetUserIdAsync(string name)
//		{
//			if (name == null)
//				throw new ArgumentNullException();
//			var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == name || u.NormalizedUserName == name)
//						?? throw new NullReferenceException();

//			var userTO = new AppUserDTO { Id = user.Id };

//			return userTO;
//		}

//		/// <summary>
//		/// Retrieves all registered users from the database.
//		/// </summary>
//		/// <returns>List of AppUserDTO</returns>
//		public async Task<List<AppUserDTO>> GetAllUsersAsync()
//		{
//			var users = await _context.Users
//				.Include(user => user.BarComments)
//				.Include(user => user.BarRatings)
//				.Include(user => user.CocktailComments)
//				.Include(user => user.CocktailRatings)
//				.Select(user => _userMapper.CreateAppUserDTO(user))
//				.ToListAsync();

//			return users;
//		}

//		/// <summary>
//		/// Creates a new user and adds it to the database.
//		/// </summary>
//		/// <param name="appUserDto">The params needed for the user to be created.</param>
//		/// <returns>AppUserDTO</returns>
//		public async Task<AppUserDTO> CreateUserAsync(AppUserDTO appUserDto)
//		{
//			if (await _context.Users.FirstOrDefaultAsync(user => user.UserName == appUserDto.Username) != null)
//				throw new ArgumentException("User with the same name already exists!");

//			var newUser = _userMapper.CreateAppUser();

//			await _context.Users.AddAsync(newUser);
//			await _context.SaveChangesAsync();

//			var newUserDTO = _userMapper.CreateAppUserDTO(newUser);

//			return newUserDTO;
//		}

//		/// <summary>
//		/// Marks specified user in the database as deleted.
//		/// </summary>
//		/// <param name="id">The Id of the user that should be marked as deleted.</param>
//		/// <returns>AppUserDTO</returns>
//		public async Task<AppUserDTO> DeleteUserAsync(Guid id)
//		{
//			var user = _context.Users
//				.FirstOrDefault(u => u.Id == id && u.IsDeleted == false) ?? throw new NullReferenceException();

//			var userDTO = _userMapper.CreateAppUserDTO(user);

//			user.IsDeleted = true;

//			_context.Users.Update(user);
//			await _context.SaveChangesAsync();

//			return userDTO;
//		}
//	}
//}
