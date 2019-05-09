using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BankPortalAggregator.Helpers;
using BankPortalAggregator.Models;

namespace BankPortalAggregator.Services
{
    public interface IUserService
    {
        Task<UserDto> Authenticate(string code);
        IEnumerable<User> GetAll();
        User GetById(int id);
        User GetBySub(string sub);
        Task<User> Create(User user);
        void Update(User user);
        void Delete(int id);
    }

    public class UserService : IUserService
    {
        private readonly BankContext _context;
        private readonly TokenHelper _tokenHelper;
        private readonly IMapper _mapper;

        public UserService(BankContext context, TokenHelper tokenHelper, IMapper mapper)
        {
            _context = context;
            _tokenHelper = tokenHelper;
            _mapper = mapper;
        }

        public async Task<UserDto> Authenticate(string code)
        {
            var googleUser = await _tokenHelper.ExchangeAuthorizationCode(code);
            var user = GetBySub(googleUser.Sub);

            if (user == null)
            {
                user = await Create(googleUser);
            }
            else
            {
                Update(googleUser);
            }

            UserDto userDto = _mapper.Map<UserDto>(user);

            userDto.Token = _tokenHelper.GetJwtToken(user);

            return userDto;
        }

        public async Task<User> Create(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public User GetById(int id)
        {
            throw new NotImplementedException();
        }

        public User GetBySub(string sub)
        {
            return _context.Users.SingleOrDefault(u => u.Sub == sub);
        }

        public void Update(User userParam)
        {
            var user = _context.Users.SingleOrDefault(u => u.Sub == userParam.Sub);

            if (user == null)
            {
                throw new Exception("User not found");
            }

            user.Name = userParam.Name;
            user.Surname = userParam.Surname;
            if (userParam.RefreshToken != null)
            {
                user.RefreshToken = userParam.RefreshToken;
            }

            _context.Users.Update(user);
            _context.SaveChanges();
        }
    }
}
