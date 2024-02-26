using System.Collections;
using JwtAuthAspWebApi.core.dto;
using JwtAuthAspWebApi.core.otherObjs;
using Microsoft.AspNetCore.Identity;

namespace JwtAuthAspWebApi.services.auth;

public class Auth(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration): IAuth
{
    public async Task<string> SeedRoles()
    {
        var doesUserRoleExists = await roleManager.RoleExistsAsync(StaticUserRoles.User);
        var doesOwnerRoleExists = await roleManager.RoleExistsAsync(StaticUserRoles.Owner);
        var doesAdminRoleExists = await roleManager.RoleExistsAsync(StaticUserRoles.Admin);

        if (doesAdminRoleExists && doesUserRoleExists && doesOwnerRoleExists)
            return "Roles already exist";

        if (!doesUserRoleExists)
        {
            await roleManager.CreateAsync(new IdentityRole(StaticUserRoles.User));

        }

        if (!doesAdminRoleExists)
        {
            await roleManager.CreateAsync(new IdentityRole(StaticUserRoles.Admin));

        }

        if (!doesOwnerRoleExists)
        {
            await roleManager.CreateAsync(new IdentityRole(StaticUserRoles.Owner));

        }

        return "Roles have been seeded";
    }

    public async Task<string> Register(RegisterDto registerDto)
    {
        var userAlreadyExist = await userManager.FindByEmailAsync(registerDto.Email) != null;
        if (userAlreadyExist) throw new InvalidOperationException("User already exists");
        
        // create new user 
        IdentityUser newUser = new()
        {
            Email = registerDto.Email,
            UserName = registerDto.Username,
            SecurityStamp = Guid.NewGuid().ToString(),
        };
        // save user and add its password to be hashed 
        var result = await userManager.CreateAsync(newUser, registerDto.Password);
        if (!result.Succeeded)
        {
            var errors = "User creation failed:";
            foreach (var error in result.Errors)
            {
                errors += $" # {error.Description}";
            }
            throw new InvalidOperationException(errors);
        }
        // add an initial user role to the new user
        await userManager.AddToRoleAsync(newUser, StaticUserRoles.User);
        return "User created successfully";

    }
}