using CoreWebApiTraining.Core.Caching;
using CoreWebApiTraining.Core.Utility;
using CoreWebApiTraining.Data;
using CoreWebApiTraining.Data.Entities;
using CoreWebApiTraining.Data.Repositories;
using CoreWebApiTraining.Models;
using CoreWebApiTraining.services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CoreWebApiTraining.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly ICachedObjectProvider<int> cachedObjectProvider;

        public UsersController(IUserService userService, ICachedObjectProvider<int> cachedObjectProvider)
        {
            this.userService = userService;
            this.cachedObjectProvider = cachedObjectProvider;
        }

        [HttpGet]
        [Route("isAlive")]
        public string Ping()
        {
            return "It's OK!";
        }

        [HttpGet]
        public async Task<List<UserModel>> GetUsers([FromHeader] int pageNumber = 1, [FromHeader] int pageSize = 0,
                                                    [FromQuery] string vFilter = default(string))
        {
            if (cachedObjectProvider.Get(Constants.TotalUserCount, out int userCount))
            {
                Response.Headers.Add("X-Total-Count", userCount.ToString());
            }
            
            if (vFilter == default(string))
            {
                return (await userService.getUsers(pageNumber, pageSize)).ToList();
            }
            else
            {
                return (await userService.getUsers(pageNumber, pageSize, vFilter)).ToList();
            }
        }

        [HttpGet("{id}")]
        public async Task<UserModel> GetUserById(int id)
        {
            return (await userService.getById(id));
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(UserModel user)
        {
            try
            {
                if (await userService.addUser(user))
                {
                    return Created("", user);
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.InternalServerError);
                }
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
