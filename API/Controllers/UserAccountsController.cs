using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class UserAccountsController : BaseApiController
    {
        private readonly DataContext _dbcontext;

        public UserAccountsController( DataContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
     
        [HttpPost("register")]
        public async Task<ActionResult<AppUser>> Register(RegisterDto registerDto) {
            if (await CheckUserExists(registerDto.username)) return BadRequest($"The Username, \"{registerDto.username}\" is taken.");

            using var hmac = new HMACSHA512();

            var user = new AppUser {
                UserName = registerDto.username.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.password)),
                PasswordSalt = hmac.Key
            }; 
            
            _dbcontext.Users.Add(user);
            await _dbcontext.SaveChangesAsync();

            return user;
        }

        [HttpPost("login")]
        public async Task<ActionResult<AppUser>> LoginUser(LoginDto loginDto){
        
            var user = await _dbcontext.Users.
            SingleOrDefaultAsync(x => x.UserName == loginDto.Username.ToLower());
           
            if (await CheckUserExists(loginDto.Username) != true) return Unauthorized("This user does not exist");

            if (user == null) return Unauthorized("Invalid username");

            using var hmac = new HMACSHA512(user.PasswordSalt);

            var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.password));

            for ( int i=0; i <computeHash.Length; i++){
                if ( computeHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid password");
            }
            return user;
        }
        private async Task<bool> CheckUserExists( string username ) {
            return await _dbcontext.Users.AnyAsync(x => x.UserName == username.ToLower());
        }

    }
    
}