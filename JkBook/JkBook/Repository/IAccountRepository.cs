using System.Threading.Tasks;
using JkBook.Models;
using Microsoft.AspNetCore.Identity;

namespace JkBook.Repository
{
    public interface IAccountRepository
    {
        Task<IdentityResult> ResetPasswordAsync(ResetPasswordModel model);
        Task GenerateForgotPasswordTokenAsync(ApplicationUser user);
        Task GenerateEmailConfirmationTokenAsync(ApplicationUser user);
        Task<IdentityResult> CereateUserAsnyc(SignUpUserModel userModel);
        Task<SignInResult> PasswordSignInAsync(SignInModel signInModel);
        Task SignOutAsync();
        Task<IdentityResult> ChangePassowrdAsync(ChangePasswordModel changePasswordModel);
        Task<IdentityResult> ConfirmEmailAsync(string uid, string token);
        Task<ApplicationUser> GetUserByEmailAsync(string email);
    }
}