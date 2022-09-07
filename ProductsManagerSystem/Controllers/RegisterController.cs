using AutoMapper;
using BusinessLayer.Services;
using BusinessLayer.Validations;
using CoreLayer.Dto;
using CoreLayer.Models;
using CoreLayer.Services;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace ProductsManagerSystem.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public RegisterController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(UserDto userDto)
        {
             var user = _mapper.Map<User>(userDto);

                userDto.Id = user.Id;
                userDto.UserName = user.UserName;
                userDto.Password = user.Password;
                userDto.Mail= user.Mail;

           await _userService.AddAsync(user);
            return RedirectToAction("Index", "Category");
        }
    }
}
