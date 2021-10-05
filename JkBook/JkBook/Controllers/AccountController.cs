using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using JkBook.Models;
using JkBook.Repository;
using Microsoft.AspNetCore.Authorization;

namespace JkBook.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountRepository;

        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [Route("signup")]
        public IActionResult Signup()
        {
            return View();
        }

        [Route("signup")]
        [HttpPost]
        public async Task<IActionResult> Signup(SignUpUserModel user)
        {
            if(ModelState.IsValid)
            {
                var result = await _accountRepository.CereateUserAsnyc(user);
                if(!result.Succeeded)
                {
                    foreach (var objErrorMessage in result.Errors)
                    {
                        ModelState.AddModelError("", objErrorMessage.Description);

                    }
                    return View(user);

                }
                ModelState.Clear();
                return RedirectToAction("ConfirmEmail", new { email = user.Email });
            }
            
            return View();
        }

        [Route("login")]
        public IActionResult Login()
        {
            return View();
        }

        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login(SignInModel signInModel,string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var resutl= await _accountRepository.PasswordSignInAsync(signInModel);
                if (resutl.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                       return LocalRedirect(returnUrl);
                    }
                    return RedirectToAction("Index", "Home");
                }
                if(resutl.IsNotAllowed)
                {
                    ModelState.AddModelError("", "Not allowed to login");
                }
                else if (resutl.IsLockedOut)
                {
                    ModelState.AddModelError("", "Account blocked. Try after sometime.");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid credentials");
                }

                
            }
            return View(signInModel);
        }

        [Route("logout")]     
        public async Task<IActionResult> LogOut()
        {
            await _accountRepository.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [Route("changepassword")]
        public IActionResult ChangePassword()
        {
            
            return View();
 
        }

        [HttpPost("changepassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var result =await _accountRepository.ChangePassowrdAsync(model);
                if (result.Succeeded)
                {
                    ViewBag.IsSuccess = true;
                    ModelState.Clear();
                    return View();
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }               
                
            }
            return View(model);
        }


        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(string uid,string token,string email)
        {
            EmailConfirmModel model = new EmailConfirmModel
            {
                Email = email
            };
            if (!string.IsNullOrEmpty(uid) && !string.IsNullOrEmpty(token))
            {
                token = token.Replace(" ", "+");
                var result= await _accountRepository.ConfirmEmailAsync(uid, token);
                if (result.Succeeded)
                {
                    model.EmailVarified= true;
                }
            }
            return View(model);

        }

        [HttpPost("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(EmailConfirmModel model)
        {
            var user = await _accountRepository.GetUserByEmailAsync(model.Email);
            if (user !=null)
            {
                if (user.EmailConfirmed)
                {
                    model.EmailVarified = true;
                    
                    return View(model);

                }
                await _accountRepository.GenerateEmailConfirmationTokenAsync(user);
                model.EmailSend = true;
                ModelState.Clear();
                
            }
            else
            {
                ModelState.AddModelError("", "Something went wrong");
            }

             return View(model);

        }

        [AllowAnonymous,HttpGet("forgot-password")]
        public IActionResult ForgotPassword()
        {
            return View();

        }
        [AllowAnonymous, HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _accountRepository.GetUserByEmailAsync(model.Email);
                if (user != null)
                {
                    await _accountRepository.GenerateForgotPasswordTokenAsync(user);

                }
                ModelState.Clear();
                model.EmailSent = true;

            }
            return View(model);

        }

        [AllowAnonymous, HttpGet("reset-password")]
        public IActionResult ResetPassword(string uid,string token)
        {
            var model = new ResetPasswordModel
            {
                UserId = uid,
                Token = token
            };
            return View(model);

        }

     

        [AllowAnonymous, HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                model.Token = model.Token.Replace(" ", "+");
                var result = await _accountRepository.ResetPasswordAsync(model);
                if (result.Succeeded)
                {
                    ModelState.Clear();
                    model.IsSuccess = true;
                    return View(model);

                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }

            }
            return View(model);

        }

    }
}