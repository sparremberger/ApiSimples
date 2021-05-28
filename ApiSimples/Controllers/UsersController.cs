using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiSimples.Data;
using ApiSimples.Models;

namespace ApiSimples.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserContext _context;

        public UsersController(UserContext context)
        {
            _context = context;
        }

        // GET: /users
        [HttpGet("/users/")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: /users/id
        [HttpGet("/users/{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

 

        // POST: /users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("/users/")]
        public async Task<ActionResult<User>> PostUser(string email)
        {
            User user = new User(); 
            user.Email = email;
            user.Card = CreditCardGenerator.GenerateMasterCardNumber();
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.ID }, user);
        }

        // GET: /users/email
        [HttpGet("/users/email/{email}")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsersByEmail(string email)
        {
            return await _context.Users.Where(u => u.Email == email).ToListAsync();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.ID == id);
        }
    }
}
