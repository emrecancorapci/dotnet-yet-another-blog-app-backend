﻿namespace Business.Services.AuthenticationService;

public interface IJwtAuthenticationManager
{
    Task<string?> GetJwtTokenAsync(string username);
}