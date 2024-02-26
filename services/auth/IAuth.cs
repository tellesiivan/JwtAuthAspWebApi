using JwtAuthAspWebApi.core.dto;

namespace JwtAuthAspWebApi.services.auth;

public interface IAuth
{
    public Task<string> SeedRoles();
    public Task<string> Register(RegisterDto registerDto);
}