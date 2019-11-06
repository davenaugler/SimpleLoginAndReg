using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleLoginAndReg.Models;

namespace SimpleLoginAndReg.Controllers
{
  public class HomeController : Controller
  {
    [HttpGet("")]
    public IActionResult Index()
    {
      return View();
    }

    [HttpPost("register")]
    public IActionResult Register(IndexViewModel modelData)
    {
      RegUser submittedUser = modelData.NewUser;
      if (ModelState.IsValid)
      {
        return RedirectToAction("Success");
      }
      return View("Index");
    }

    [HttpPost("login")]
    public IActionResult Login(IndexViewModel modelData)
    {
      LogUser submittedUser = modelData.User;
      if (ModelState.IsValid)
      {
        return RedirectToAction("Success");
      }
      return View("Index");
    }

    [HttpGet("Success")]
    public ViewResult Success()
    {
      return View("Success");
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