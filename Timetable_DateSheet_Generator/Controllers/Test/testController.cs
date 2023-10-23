using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Timetable_DateSheet_Generator.Controllers.Test
{
    public class testController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}