using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Users;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : WorklogControllerBase
    {
        public UserController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn(UserLoginDTO loginDto)
        {
            var result = await Mediator.Send(new Details.Query() {  
                Email = loginDto.Email, 
                Password = loginDto.Password 
            }); 
            
            if(result == null) return Unauthorized();

            return Ok(result);
        }

        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser(NewUserDTO newUserDto)
        {
            var result = await Mediator.Send(new Create.Command {  
                NewUser = newUserDto
            }); 
            
            if(newUserDto == null) return BadRequest();

            return Ok(result);
        }
    }
}