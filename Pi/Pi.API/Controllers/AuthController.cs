using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pi.API.Attributes;
using Pi.Application.Common.Models;
using Pi.Application.UseCases.Login.Command;
using Pi.Application.UseCases.Login.Dto;
using Pi.Domain.Entities.Identity;
using Pi.Domain.Interfaces;
using Pi.Infrastracture.Repositories.Identity;

namespace Pi.API.Controllers
{
    public class AuthController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IMediator mediator, IUnitOfWork unitOfWork, ILogger<AuthController> logger)
        {
            _mediator = mediator;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("GetSecureData")]
        [Authorize]
        [HasPermission(permission: "user.getadata")]
        [ProducesOkApiResponseType<Users>]
        public async Task<IActionResult> GetSecureData()
        {
            var users = await _unitOfWork.Repository<Users>().GetAllAsync();
            return Ok(ApiResult<IEnumerable<Users>>.Success(users, "Danh sách Users"));
        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var result = await _mediator.Send(new LoginComand { Request = request });
            _logger.LogInformation("user login", result);
            return result == null ? Unauthorized() : Ok(result);
        }

        //public async Task<IActionResult> Refresh(RefreshTokenRequest request)
        //{

        //}
    }
}
