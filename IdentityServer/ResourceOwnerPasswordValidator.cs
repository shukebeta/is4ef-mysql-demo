using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4.Validation;
using is4ef.Models;
using Microsoft.AspNetCore.Identity;

namespace IdentityServer
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public ResourceOwnerPasswordValidator(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var userName = context.Request.Raw["username"];
            var password = context.Request.Raw["password"];
            bool rememberLogin = !context.Request.Raw["rememberLogin"]?.Equals("") ?? false;

            var result = await _signInManager.PasswordSignInAsync(userName, password, rememberLogin, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                context.Result.IsError = false;
                context.Result.Subject = GetClaimsPrincipal();
            }

            return;
        }

        private static ClaimsPrincipal GetClaimsPrincipal()
        {
            var issued = DateTimeOffset.Now.ToUnixTimeSeconds();

            var claims = new List<Claim>
            {
                new Claim(JwtClaimTypes.Subject, Guid.NewGuid().ToString()),
                new Claim(JwtClaimTypes.AuthenticationTime, issued.ToString()),
                new Claim(JwtClaimTypes.IdentityProvider, "localhost")
            };

            return new ClaimsPrincipal(new ClaimsIdentity(claims));
        }
    }
}
