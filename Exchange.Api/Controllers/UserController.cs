using AutoMapper;
using Exchange.Core.Exceptions;
using Exchange.Models;
using Exchange.Models.DTOs;
using Exchange.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;

namespace Exchange.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly UserService _userService;
        private readonly IMapper _mapper;

        public UserController(UserService userService, IMapper mapper) 
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet()]
        [Produces(typeof(UserDTO))]
        public ActionResult<GenericResponse<IEnumerable<UserDTO>>> AllUsers()
        {
            var response = _userService.FindAllUsers();

            if (response == null)
                throw new HttpStatusException($"Something went wrong. Please contact the System Administrator.",
                  HttpStatusCode.InternalServerError);

            return Ok(new GenericResponse<IEnumerable<UserDTO>>()
            {
                Data = _mapper.Map<IEnumerable<UserDTO>>(response),
                ErrorCode = "200"
            });
        }
    }
}
