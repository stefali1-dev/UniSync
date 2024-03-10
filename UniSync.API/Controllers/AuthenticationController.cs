using UniSync.API.Models;
using UniSync.Application.Contracts.Identity;
using UniSync.Application.Contracts.Interfaces;
using UniSync.Application.Models.Identity;
using UniSync.Identity.Models;
using Microsoft.AspNetCore.Mvc;

namespace UniSync.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthenticationController> _logger;
        private readonly ICurrentUserService _currentUserService;
        private readonly IRoleAssignmentService _roleAssignmentService;

        public AuthenticationController(IAuthService authService, ILogger<AuthenticationController> logger, ICurrentUserService currentUserService, IRoleAssignmentService roleAssignmentService)
        {
            _authService = authService;
            _logger = logger;
            _currentUserService = currentUserService;
            _roleAssignmentService = roleAssignmentService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid payload");
                }

                var (status, message) = await _authService.Login(model);

                if (status == 0)
                {
                    return BadRequest(message);
                }

                return Ok(message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegistrationModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid payload");
                }
                
                var userRole = _roleAssignmentService.GetUserRoleByRegistrationId(model.RegistrationId);
                var (status, message) = await _authService.Registeration(model, userRole);

                if (status == 0)
                {
                    return BadRequest(message);
                }

                return CreatedAtAction(nameof(Register), model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        
        [HttpPost]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await _authService.Logout();
            return Ok();
        }

        [HttpGet]
        [Route("currentuserinfo")]
        public CurrentUser CurrentUserInfo()
        {
            if (_currentUserService.GetCurrentUserId() == null)
            {
                return new CurrentUser
                {
                    IsAuthenticated = false
                };
            }
            return new CurrentUser
            {
                IsAuthenticated = true,
                UserName = _currentUserService.GetCurrentUserId(),
                Claims = _currentUserService.GetCurrentClaimsPrincipal().Claims.ToDictionary(c => c.Type, c => c.Value)
            };
        }

    }
}
