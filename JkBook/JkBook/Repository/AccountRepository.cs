using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JkBook.Models;
using JkBook.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace JkBook.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;

        public AccountRepository(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            IUserService userService,
            IEmailService emailService,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userService = userService;
            _emailService = emailService;
            _configuration = configuration;
            _roleManager = roleManager;
        }
        public async Task<IdentityResult> CereateUserAsnyc(SignUpUserModel userModel)
        {
            var user = new ApplicationUser()
            {
                FirstName=userModel.FirstName,
                LastName=userModel.LastName,
                Email = userModel.Email,
                UserName = userModel.Email };
             var result= await _userManager.CreateAsync(user, userModel.Password);
            if (result.Succeeded)
            {
                await GenerateEmailConfirmationTokenAsync(user);

            }
            return result;
        }

        public async Task<ApplicationUser> GetUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task GenerateEmailConfirmationTokenAsync(ApplicationUser user)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            if (!string.IsNullOrEmpty(token))
            {
                await SendEmailConfirmationEmail(user, token);
            }
        }
        public async Task GenerateForgotPasswordTokenAsync(ApplicationUser user)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            if (!string.IsNullOrEmpty(token))
            {
                await SendForgotPasswordEmail(user, token);
            }
        }
        private async Task SendEmailConfirmationEmail(ApplicationUser user,string token)
        {
            string appDomain = _configuration.GetSection("Application:AppDomain").Value;
            string confirmationEmailLink = _configuration.GetSection("Application:EmailConfirmation").Value;

            UserEmailOptions options = new UserEmailOptions
            {
                ToEmails = new List<string>() { user.Email },
                PlaceHolders = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string,string>("{UserName}",user.FirstName ),
                    new KeyValuePair<string,string>("{Link}",string.Format(appDomain+confirmationEmailLink,user.Id,token))

                }
            };
            await _emailService.SendEmailForEmailConfirmation(options);
            
        }

        private async Task SendForgotPasswordEmail(ApplicationUser user, string token)
        {
            string appDomain = _configuration.GetSection("Application:AppDomain").Value;
            string forgotPasswordLing = _configuration.GetSection("Application:ForgotPassword").Value;

            UserEmailOptions options = new UserEmailOptions
            {
                ToEmails = new List<string>() { user.Email },
                PlaceHolders = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string,string>("{UserName}",user.FirstName ),
                    new KeyValuePair<string,string>("{Link}",string.Format(appDomain+forgotPasswordLing,user.Id,token))

                }
            };
            await _emailService.SendEmailForForgotPassword(options);
        }


        public async Task<SignInResult> PasswordSignInAsync(SignInModel signInModel)
        {
            var result= await _signInManager.PasswordSignInAsync(signInModel.Email, signInModel.Password, signInModel.RememberMe, true);
            return result;

        }
        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        } 
        public async Task<IdentityResult> ChangePassowrdAsync(ChangePasswordModel changePasswordModel)
        {
            var userId= _userService.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);            
            
            var result=await _userManager.ChangePasswordAsync(user, changePasswordModel.CurrentPassword, changePasswordModel.NewPassword);
            return result;
        }

        public async Task<IdentityResult> ConfirmEmailAsync(string uid,string token )
        {

            var user = await _userManager.FindByIdAsync(uid);

            var result = await _userManager.ConfirmEmailAsync(user,token);
            return result;
        }

        public async Task<IdentityResult> GenerateEmailConfirmationTokenAsync(string uid, string token)
        {

            var user = await _userManager.FindByIdAsync(uid);

            var result = await _userManager.ConfirmEmailAsync(user, token);
            return result;
        }

        public async Task<IdentityResult> ResetPasswordAsync(ResetPasswordModel model)
        {
            return await _userManager.ResetPasswordAsync ( await _userManager.FindByIdAsync(model.UserId),model.Token,model.NewPassword);
        }


    }
}
