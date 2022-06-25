using Diabetes.MVC.Models.Components;
using Microsoft.AspNetCore.Mvc;

namespace Diabetes.MVC.Components
{
    public class FoodItemsList:ViewComponent
    {
        public IViewComponentResult Invoke(FoodItemsListViewModel model)
        {
            return View(model);
        }
        
    }
}