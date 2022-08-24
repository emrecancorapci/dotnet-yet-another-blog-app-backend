﻿using AutoMapper;
using BlogProject.Business.Services.UserService.Dtos;
using BlogProject.DataAccess.Repositories.Base.Interfaces;
using BlogProject.DataAccess.Repositories.Extensions;
using BlogProject.Entities.Base;

namespace BlogProject.Business.Services.UserService;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    
    public UserService(
        IUserRepository userRepository,
        IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<int> AddAsync(AddUserRequest request)
    {
        var user = _mapper.Map<User>(request);

        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
        user.Role = "User";
        user.Created = DateTime.Now.SetKindUtc();
        user.Modified = DateTime.Now.SetKindUtc();

        var userId = await _userRepository.AddAsync(user);
        return userId;
    }

    public async Task<int> UpdateAsync(UpdateUserRequest request)
    {
        var user = _mapper.Map<User>(request);

        user.Modified = DateTime.Now.SetKindUtc();
        
        return await _userRepository.UpdateAsync(user);
    }

    public async Task<int> DeleteAsync(int id) => 
        await _userRepository.DeleteAsync(id);

    public async Task<GetUserResponse?> GetAsync(int id)
    {
        var entity = await _userRepository.GetAsync(id);
        if (entity == null) return null;

        var response = _mapper.Map<GetUserResponse>(entity);

        return response;
    }

    public async Task<IList<GetUserResponse>> GetAllAsync()
    {
        var entityList = await _userRepository.GetAllAsync();
        var responseList = entityList
            .OrderByDescending(user => user.Created)
            .Select(user => _mapper.Map<GetUserResponse>(user))
            .ToList();

        return responseList;
    }

    public async Task<bool> IsExistAsync(int id) =>
        await _userRepository.IsExist(id);

    public async Task<UserValidationResponse?> ValidateUserAsync(string username, string password)
    {
        var user = await _userRepository.ValidateUser(username);
        bool isVerified = BCrypt.Net.BCrypt.Verify(password, user.Password);

        if (user == null || !isVerified) return null;

        var response = _mapper.Map<UserValidationResponse>(user);
        return response;
    }

    public async Task<bool> IsUserNameExistAsync(string userName) =>
        (await _userRepository.ValidateUser(userName)) != null;

    public async Task<bool> IsEmailExistAsync(string email) => 
        await _userRepository.IsEmailExist(email);
    
    public async Task<int> GetUserIdByUsername(string userName) =>
        (await _userRepository.ValidateUser(userName)).Id;
}