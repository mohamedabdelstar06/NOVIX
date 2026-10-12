using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NOVIX.Identity.Application.Interfaces;
using NOVIX.Identity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NOVIX.Identity.Infrastructure.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public IdentityService(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<(bool IsSuccess, string Token, string ErrorMessage)> LoginAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null && await _userManager.CheckPasswordAsync(user, password))
            {
                var token = GenerateJwtToken(user);
                return (true, token, string.Empty);
            }
            return (false, string.Empty, "Invalid email or password");
        }

        public async Task<(bool IsSuccess, string ErrorMessage)> RegisterAsync(string email, string password, string firstName, string lastName)
        {
            var userExists = await _userManager.FindByEmailAsync(email);
            if (userExists != null)
                return (false, "User already exists!");

            var user = new ApplicationUser
            {
                Email = email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = email,
                FirstName = firstName,
                LastName = lastName
            };
            
            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded)
                return (false, "User creation failed! Please check user details and try again.");

            return (true, string.Empty);
        }

        public async Task<(bool IsSuccess, string Token, string ErrorMessage)> GoogleLoginAsync(string idToken)
        {
            try
            {
                var settings = new GoogleJsonWebSignature.ValidationSettings()
                {
                    Audience = new List<string>() { "YOUR_GOOGLE_CLIENT_ID" } 
                };
                
                var payload = await GoogleJsonWebSignature.ValidateAsync(idToken, settings);
                
                var user = await _userManager.FindByEmailAsync(payload.Email);
                if (user == null)
                {
                    user = new ApplicationUser()
                    {
                        Email = payload.Email,
                        UserName = payload.Email,
                        FirstName = payload.GivenName,
                        LastName = payload.FamilyName,
                        EmailConfirmed = true
                    };
                    await _userManager.CreateAsync(user);
                }

                var token = GenerateJwtToken(user);
                return (true, token, string.Empty);
            }
            catch (Exception ex)
            {
                return (false, string.Empty, "Invalid Google Token: " + ex.Message);
            }
        }

        private string GenerateJwtToken(ApplicationUser user)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Secret"]!));

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["ExpiryMinutes"])),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
