using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JopPortal.Data;
using JopPortal.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using jobPortal.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace JopPortal.Controllers
{
    public class LoginUserController : Controller
    {
        private readonly AppDbContext context;
        public LoginUserController(AppDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            var Email = HttpContext.Session.GetString("Email");
            if (string.IsNullOrEmpty(Email))
            {
                return RedirectToAction("LogIn");
            }
            User users = context.user.FirstOrDefault(u => u.Email == Email);
       
            return View(users);
        }


        [HttpGet]
        public IActionResult LogIn()
        {
            return View(new User());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult LogInSave(User Us)
        {
            User user = context.user.FirstOrDefault(x => x.Email == Us.Email && x.Password == Us.Password);
            User isEmailExist = context.user.FirstOrDefault(x => x.Email == Us.Email);
            User isPassExist = context.user.FirstOrDefault(x => x.Password == Us.Password);

            if (user != null)
            {
                HttpContext.Session.SetString("Email", Us.Email);
              //  ViewBag.IsAuthenticated = true;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.Clear();
                if (isEmailExist == null)
                {
                    ModelState.AddModelError("Email", "This Email is not found");
                    return View("LogIn", Us);

                }
                else
                {
                    ModelState.AddModelError("Password", "Incorrect Password");
                    //ViewBag.IsAuthenticated = false;
                    return View("LogIn", Us);
                }
            }
        }

        [HttpGet]
        public IActionResult SignUp()
        {

            return View(new User());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SignUpSave(User Us)
        {
            var isEmailExist = context.user.Any(x => x.Email == Us.Email);
            if (isEmailExist)
            {
                ModelState.AddModelError("Email", "Email is already exists");
                return View("SignUp",Us);
            }
            if (ModelState.IsValid)
            {
                if (Us.Password == Us.ConfirmPassword)
                {
                    context.user.Add(Us);
                    context.SaveChanges();
                    return RedirectToAction("LogIn");
                }
            }
            return View("SignUp", Us);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || context.user == null)
            {
                return NotFound();
            }

            var user = await context.user
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }


        public  IActionResult Edit(int? id)
        {

            User user = context.user.FirstOrDefault(u => u.Id == id);
            return View(user);
        }
        public IActionResult SaveEdit(int id, User model, string currentPassword, string newPassword, string confirmNewPassword)
        {
            User user = context.user.FirstOrDefault(u => u.Id == id);

            if (currentPassword == user.Password)
            {
                if (newPassword == confirmNewPassword)
                {
                    user.Name = model.Name;
                    user.Email = model.Email;
                    user.Password = model.Password;
                    user.ConfirmPassword = model.ConfirmPassword;
                    user.Age = model.Age;
                    user.Gender = model.Gender;
                    user.Address = model.Address;
                    user.PhoneNumber = model.PhoneNumber;
                    user.Photo = model.Photo;

                    if (!string.IsNullOrEmpty(newPassword))
                    {
                        user.Password = newPassword;
                    }

                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "New password and confirm new password do not match.");
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Current password is incorrect.");
            }

            return View("Edit", model);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await context.user
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await context.user.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            context.user.Remove(user);
            await context.SaveChangesAsync();
            HttpContext.Session.Clear();
            return RedirectToAction("Index","Home");
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
        //private string HashPassword(string password)
        //{
        //    using (SHA256 sha256 = SHA256.Create())
        //    {
        //        byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        //        return Convert.ToBase64String(hashedBytes);
        //    }
        //    //return "********";
        //}

        //private bool VerifyPassword(string inputPassword, string hashedPassword)
        //{
        //    return HashPassword(inputPassword) == hashedPassword;
        //}
    }
}
