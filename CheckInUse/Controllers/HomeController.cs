using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SwedishNationalId;
using FluentValidation;
using FluentValidation.Attributes;

namespace CheckInUse.Controllers
{
    [Validator(typeof(MyViewModelValidator))]
    public class MyViewModel
    {
        public Cin MyId { get; set; }
        public int Count { get; set; }
    }

    public class MyViewModelValidator : AbstractValidator<MyViewModel>
    {
        public MyViewModelValidator()
        {
            RuleFor(x => x.MyId).NotEmpty();
        }
    }
    public class HomeController : Controller
    {
        [HttpPost]
        public IActionResult Save([FromBody]MyViewModel model)
        {
            var g = Guid.NewGuid();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
                
            }
            return Ok(model);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
