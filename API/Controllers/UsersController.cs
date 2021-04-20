using System.Collections.Generic;
using System.Linq;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly DataContext _dbContext;

        public UsersController(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet()]
        public ActionResult <IEnumerable<AppUser>> GetUsers(){
            return _dbContext.Users.ToList();
            
        }

        [HttpGet("{id}")]
        public ActionResult <AppUser> GetUser(int id){
             return _dbContext.Users.Find(id);
             
        }
        
    }
}