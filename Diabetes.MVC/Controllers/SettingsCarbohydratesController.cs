﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Diabetes.MVC.Models;
using Microsoft.AspNetCore.Identity;
using Diabetes.Persistence;
using MediatR;
using System.Threading.Tasks;
using Diabetes.Domain.Normalized.Enums.Units;

namespace Diabetes.MVC.Controllers
{
    public class SettingsCarbohydratesController : Controller
    {
        private readonly UserManager<Account> _userManager;
        private readonly IMediator _mediator;

        public SettingsCarbohydratesController(IMediator mediator, UserManager<Account> userManager)
        {
            _userManager = userManager;
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> IndexAsync(SettingsCarbohydratesViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);

                user.BreakfastTime = model.BreakfastTime;
                user.LunchTime = model.LunchTime;
                user.DinnerTime = model.DinnerTime;
                user.CarbohydrateUnits = model.CarbohydrateUnitsUsed;
                if (model.CarbohydrateInBreadUnit != null)
                    user.CarbohydrateInBreadUnit = (int)model.CarbohydrateInBreadUnit;

                var result = await _userManager.UpdateAsync(user);

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return View(model);
                }

                return RedirectToAction("Index", "Settings");
            }

            return View(model);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var viewModel = new SettingsCarbohydratesViewModel
            {
                BreakfastTime = user.BreakfastTime,
                LunchTime = user.LunchTime,
                DinnerTime = user.DinnerTime,
                CarbohydrateUnitsUsed = user.CarbohydrateUnits,
                CarbohydrateInBreadUnit = user.CarbohydrateInBreadUnit
            };

            return View(viewModel);
        }
    }
}
