using JwtAuthAspWebApi.core.dto;
using JwtAuthAspWebApi.services.auth;
using Microsoft.AspNetCore.Mvc;

namespace JwtAuthAspWebApi.controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(IAuth auth): ControllerBase
{
    
    [HttpPost("seed")]
    public async Task<IActionResult> SeedRoles()
    {
    var result = await auth.SeedRoles();
    return Ok(result);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        try
        {
            return Ok(await auth.Register(registerDto));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}