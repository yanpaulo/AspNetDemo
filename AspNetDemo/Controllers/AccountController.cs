using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using AspNetDemo.Data;
using AspNetDemo.Services;
using AspNetDemo.ViewModels;

namespace AspNetDemo.ApiControllers
{
    [Produces("application/json")]
    [Route("api/Account")]
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly TokenConfigurations tokenConfigurations;

        public AccountController(ApplicationDbContext context, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, TokenConfigurations tokenConfigurations)
        {
            this.context = context;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.tokenConfigurations = tokenConfigurations;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody]LoginViewModel model)
        {
            var user = context.Users.SingleOrDefault(u => u.UserName == model.UserName);
            if (user == null)
            {
                ModelState.AddModelError("", "Não existe usuário com o email especificado.");
            }

            if (user != null)
            {
                var signInResult = await signInManager.PasswordSignInAsync(user, model.Password, false, false);
                if (signInResult.Succeeded)
                {
                    var result = new LoginResultViewModel
                    {
                        Login = user.UserName,
                        Token = await GetToken(user),
                    };
                    return Ok(result);
                }
                else
                {
                    ModelState.AddModelError("", "Usuário ou Senha inválidos");
                }
            }

            return BadRequest(ModelState);
        }
        
        private async Task<string> GetToken(IdentityUser model)
        {
            var now = DateTime.UtcNow;
            
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                new Claim(JwtRegisteredClaimNames.UniqueName, model.UserName),
            };

            
            ClaimsIdentity identity = new ClaimsIdentity( new GenericIdentity(model.UserName), claims);

            var handler = new JwtSecurityTokenHandler();
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = tokenConfigurations.Issuer,
                Audience = tokenConfigurations.Audience,
                Expires = now.AddMonths(1),
                IssuedAt = now,
                
                SigningCredentials = tokenConfigurations.SigningCredentials,
                Subject = identity
            });
            

            var token = handler.WriteToken(securityToken);

            return token;
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
        
    }
}