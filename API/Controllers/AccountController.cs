using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class AccountController : BaseApiController
{
    private readonly DataContext _context;

    private readonly RegistrationDataContext _registrationContext;
    private readonly ITokenService _tokenService;

    public AccountController(DataContext context, RegistrationDataContext registrationContext, ITokenService tokenService)
    {
        _context = context;
        _registrationContext = registrationContext;
        _tokenService = tokenService;
    }
    
    [HttpPost("register")] // POST: api/account/register
    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
    {
        if (await UserExists(registerDto.RegistrationId))
        {
            return BadRequest("User already exists.");
        }
        
        using var hmac = new HMACSHA512();
        
        var user = new User
        {
            Email = registerDto.Email.ToLower(),
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
            PasswordSalt = hmac.Key,
            FirstName = registerDto.FirstName,
            LastName = registerDto.LastName,
            Roles = GetRolesByRegistrationId(registerDto.RegistrationId).Result
            
            // TODO: Set the role based on the RegistrationId
        };
        
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return new UserDto
        {
            Email = user.Email,
            Token = _tokenService.CreateToken(user)
        };
    }
    
    [HttpPost("login")] // POST: api/account/login
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
        var user = await _context.Users.SingleOrDefaultAsync(x => x.Email == loginDto.Email);

        if (user == null)
        {
            return Unauthorized("Invalid Email");
        }

        using var hmac = new HMACSHA512(user.PasswordSalt);

        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

        for (var i = 0; i < computedHash.Length; i++)
        {
            if (computedHash[i] != user.PasswordHash[i])
            {
                return Unauthorized("Invalid password");
            }
        }

        return new UserDto
        {
            Email = user.Email,
            UserId = user.UserId,
            Token = _tokenService.CreateToken(user)
        };
    }
    
    private async Task<bool> UserExists(int registrationId)
    {
        // TODO: Check if the RegistrationId exists in the unregistered users database
        //return await _context.Users.AnyAsync(x => x.RegistrationId == RegistrationId);
        return false;
    }

    public async Task<List<Role>> GetRolesByRegistrationId(int registrationId)
    {
        // Query the database to find matching records and select only the RoleId
        var roleIds = await _registrationContext.UserRegistrationRoles
            .Where(x => x.RegistrationId == registrationId)
            .Select(x => x.RoleId)
            .ToListAsync();

        List<Role> roles = new List<Role>();
        foreach (var roleId in roleIds)
        {
            roles.Add(new Role
            {
                RoleId = roleId,
                Name = "RoleName"
            });
        }
        return roles;
    }
}