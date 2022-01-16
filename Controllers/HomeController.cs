using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Proje.Data;
using Proje.Models;
using Proje.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Proje.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _contex;
        public HomeController(ApplicationDbContext contex)
        {
            _contex = contex;
        }
        public IActionResult Index()
        {
            HomeViewModel HomeVM = new HomeViewModel()

            {
                Menu = _contex.Menu.Include(m => m.Category).ToList(),
                Discount = _contex.Discount.ToList(),
                Comments = _contex.Comments.Include(c => c.ApplicationUser).ToList(),
            };
            return View(HomeVM);
        }
    }
}
