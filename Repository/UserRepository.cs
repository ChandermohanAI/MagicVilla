using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using MagicVilla.Data;
using MagicVilla.Model;
using MagicVilla.Model.DTO;
using MagicVilla.Repository.IRepository;
using Microsoft.IdentityModel.Tokens;

namespace MagicVilla.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;
        private string secretKey;

        public UserRepository(ApplicationDbContext db, IConfiguration configuration)
        {
            _db = db;
            secretKey = configuration.GetValue<string>("ApiSettings:Secret");
        }

        public bool IsUniqueUser(string username)
        {
            var user = _db.LocalUser.FirstOrDefault(x=> x.UserName==username);
            if(user==null){
                return true;
            }
            return false;

        }

        public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO)
        {
            var user = _db.LocalUser.FirstOrDefault(u=>u.UserName.ToLower()==loginRequestDTO.UserName.ToLower()
            && u.Password == loginRequestDTO.Password);

            if(user==null)
            {
                return null;
            }

            // if user exists
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name,user.Id.ToString()),
                    new Claim(ClaimTypes.Role,user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            LoginResponseDTO loginResponseDTO = new LoginResponseDTO()
            {
                Token = tokenHandler.WriteToken(token),
                User = user
            };
            return loginResponseDTO;

        }

        public async Task<LocalUser> Register(RegisterationRequestDTO registerationRequestDTO)
        {
            LocalUser user = new LocalUser()
            {
                UserName = registerationRequestDTO.UserName,
                Password = registerationRequestDTO.Password,
                Name =  registerationRequestDTO.Name,
                Role = registerationRequestDTO.Role

            };

            _db.LocalUser.Add(user);
            await _db.SaveChangesAsync();
            user.Password="";
            return user;
        }

    }
}