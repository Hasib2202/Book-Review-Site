using AutoMapper;
using BLL.DTOs;
using DAL.EF.Tables;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;

namespace BLL.Services
{

    public class UserService
    {
        private static Dictionary<string, string> ActiveSessions = new Dictionary<string, string>(); // In-memory session store

        private static Mapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserDTO>();
                cfg.CreateMap<UserDTO, User>();
            });
            return new Mapper(config);
        }

        public static UserDTO Login(string username, string password)
        {
            var repo = DataAccess.UserData();
            var user = repo.GetByUsername(username);
            if (user != null && user.Password == password)
            {
                var userDto = GetMapper().Map<UserDTO>(user);

                // Generate token
                var token = Guid.NewGuid().ToString();
                ActiveSessions[token] = username;

                userDto.Token = token; // Attach token to the response DTO
                return userDto;
            }
            return null;
        }

        public static bool InvalidateSessionToken(string token)
        {
            if (ActiveSessions.ContainsKey(token))
            {
                ActiveSessions.Remove(token);
                return true;
            }
            return false;
        }

        public static bool IsTokenValid(string token)
        {
            return ActiveSessions.ContainsKey(token);
        }

        public static bool Register(UserDTO userDto)
        {
            var repo = DataAccess.UserData();  // Assuming this returns an IUserRepo instance

            // Check if the email already exists in the database
            var existingUser = repo.GetByEmail(userDto.Email);
            if (existingUser != null)
            {
                // Email exists, return false
                return false;
            }

            // If email doesn't exist, proceed with registration
            var user = GetMapper().Map<User>(userDto);
            return repo.Create(user);
        }
    }
}
