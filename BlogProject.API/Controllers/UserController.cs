﻿using BlogProject.Business.Services.AuthenticationService;
using BlogProject.Business.Services.UserService;
using BlogProject.Business.Services.UserService.Dtos;
using BlogProject.Entities.Base;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IJwtAuthenticationManager _jwtAuthenticationManager;

    public UserController(
        IUserService userService,
        IConfiguration configuration,
        IJwtAuthenticationManager jwtAuthenticationManager)
    {
        _userService = userService;
        _jwtAuthenticationManager = jwtAuthenticationManager;
    }

    // GET
    [HttpGet("Get")]
    public async Task<IActionResult> Get(int id)
    {
        if (id == 0) return BadRequest();
        if (!await _userService.IsExistAsync(id)) return NotFound();
    
        var response = await _userService.GetAsync(id);

        return Ok(response);
    }
    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        var responseList = await _userService.GetAllAsync();

        return Ok(responseList);
    }
    [HttpGet("IsExist")]
    public async Task<IActionResult> IsExist(int userId)
    {
        if (userId == 0) return BadRequest();
    
        var response = await _userService.IsExistAsync(userId);

        return Ok(response);
    }
    [HttpGet("Validate")]
    public async Task<IActionResult> Validate(UserValidationRequest request)
    {
        var response = await _userService
            .ValidateUserAsync(request.Username, request.Password);

        return Ok(response);
    }

    [HttpGet("GetUserIdByUsername")]
    public async Task<IActionResult> GetUserIdByUsername(string username)
    {
        var response = await _userService.GetUserIdByUsername(username);

        return Ok(response);
    }
    [HttpGet("IsEmailExist")]
    public async Task<IActionResult> IsEmailExist(string email)
    {
        var response = await _userService.IsEmailExistAsync(email);

        return Ok(response);
    }


    // POST
    [HttpPost("Add")]
    public async Task<IActionResult> Add(AddUserRequest request)
    {
        var affectedRows = await _userService.AddAsync(request);

        return Ok(affectedRows);
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login(UserValidationRequest request)
    {
        var response = await _userService
            .ValidateUserAsync(request.Username, request.Password);

        if (response == null) return BadRequest();

        var tokenResponse = await _jwtAuthenticationManager.GetJwtTokenAsync(response.UserName);

        if (tokenResponse == null) throw new Exception("Token is null");

        response.Token = tokenResponse;

        return Ok(response);
    }

    // PATCH
    [HttpPut("Update")]
    public async Task<IActionResult> Update(User request)
    {
        var affectedRows = await _userService.UpdateAsync(request);

        return Ok(affectedRows);
    }

    // DELETE
    [HttpDelete("Delete")]
    public async Task<IActionResult> Delete(int userId)
    {
        var affectedRows = await _userService.DeleteAsync(userId);

        return Ok(affectedRows);
    }
}

