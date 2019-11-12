using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleLoginAndReg.Models;

namespace SimpleLoginAndReg.Controllers
{
  public class HomeController : Controller
  {
    private SimpleLoginAndRegContext dbContext;

    public HomeController(SimpleLoginAndRegContext context)
    {
      dbContext = context;
    }

    [HttpGet("")]
    public IActionResult Index()
    {
      return View();
    }

    [HttpPost("register")]
    public IActionResult Register(IndexViewModel model)
    {
      if (ModelState.IsValid)
      {
        RegUser newUser = model.NewUser;
        if (dbContext.Users.Any(u => u.EmailAddress == newUser.EmailAddress))
        {
          ModelState.AddModelError("Email", "Email already in use.");
          return View("Index");
        }
        else
        {
          PasswordHasher<RegUser> Hasher = new PasswordHasher<RegUser>();
          newUser.Password = Hasher.HashPassword(newUser, newUser.Password);
          dbContext.Add(newUser);
          dbContext.SaveChanges();
          RegUser userInDb = dbContext.Users.FirstOrDefault(u => u.EmailAddress == newUser.EmailAddress);
          HttpContext.Session.SetInt32("id", userInDb.UserId);
          return RedirectToAction("Success");
        }
      }
      else
      {
        return View("Index");
      }
    }

    [HttpPost("login")]
    public IActionResult Login(IndexViewModel model)
    {
      if (ModelState.IsValid)
      {
        System.Console.WriteLine($"############### Model State is Valid #######################");
        RegUser userInDb = dbContext.Users.FirstOrDefault(u => u.EmailAddress == model.User.EmailAddress);
        if (userInDb == null)
        {
          System.Console.WriteLine("################ This User Does Not Exist ######################");
          ModelState.AddModelError("Email", "Invalid Email/Password");
          return View("Index");
        }
        PasswordHasher<IndexViewModel> hasher = new PasswordHasher<IndexViewModel>();

        PasswordVerificationResult result = hasher.VerifyHashedPassword(model, userInDb.Password, model.User.Password);

        if (result == 0)
        {
          System.Console.WriteLine("################## Incorrect Password #################################");
          ModelState.AddModelError("Password", "This password is incorrect");
        }
        else
        {
          HttpContext.Session.SetInt32("id", userInDb.UserId);
          return RedirectToAction("Success");
        }
      }
      return View("Index");
    }

    [HttpGet("success")]
    public IActionResult Success()
    {
      if (HttpContext.Session.GetInt32("id") != null)
      {
        return View();
      }
      return RedirectToAction("Index");
    }

    [HttpGet("logout")]
    public IActionResult Logout()
    {
      HttpContext.Session.Clear();
      return RedirectToAction("Index");

    }


    public IActionResult Privacy()
    {
      return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
  }
}