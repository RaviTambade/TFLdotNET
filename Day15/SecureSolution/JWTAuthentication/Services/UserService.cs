using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using WebApi.Entities;
using WebApi.Helpers;

namespace WebApi.Services
{ 

    public class UserService : IUserService
    {
         // users hardcoded for simplicity, store in a db with hashed passwords in production applications
        private List<User> _users = new List<User>
        { 
            new User { Id = 1, FirstName = "Swarali", LastName = "L", Username = "swarali", Password = "swarali", Role = Role.Admin },
            new User { Id = 2, FirstName = "Ganesh", LastName = "S", Username = "ganesh", Password = "ganesh", Role = Role.User } ,
            new User { Id = 3, FirstName = "Rutuja", LastName = "T", Username = "rutuja", Password = "rutuja", Role = Role.User },
            new User { Id = 4, FirstName = "Vishwambhar", LastName = "K", Username = "vishwambhar", Password = "vishwambhar", Role = Role.User },
            new User { Id = 5, FirstName = "Rohit", LastName = "W", Username = "rohit", Password = "rohit", Role = Role.Admin },
            };

        private readonly AppSettings _appSettings;

        public UserService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            // initialize repository for database users

        }

        public User Authenticate(string username, string password)
        {

            // access validate method of repository
            
            var user = _users.SingleOrDefault(x => x.Username == username && x.Password == password);

            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] 
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            return user.WithoutPassword();
        }

        public IEnumerable<User> GetAll()
        {
            return _users.WithoutPasswords();
        }

        public User GetById(int id) 
        {
            var user = _users.FirstOrDefault(x => x.Id == id);
            return user.WithoutPassword();
        }
    }
         
    }
