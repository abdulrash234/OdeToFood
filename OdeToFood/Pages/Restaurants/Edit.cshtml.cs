using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OdeToFood.Core;
using OdeToFood.Data;

namespace OdeToFood.Pages.Restaurants
{
    public class EditModel : PageModel
    {
        private readonly IRestaurantData _restauranData;
        private readonly IHtmlHelper _htmlHelper;
        
        [BindProperty] 
        public Restaurant Restaurant { get; set; }

        public IEnumerable<SelectListItem> Cuisines { get; set; }

        public EditModel(IRestaurantData restauranData, IHtmlHelper htmlHelper)
        {
            _restauranData = restauranData;
            _htmlHelper = htmlHelper;
        }
        public IActionResult OnGet(int? restaurantId)
        {
            Cuisines = _htmlHelper.GetEnumSelectList<CuisineType>();
            if (restaurantId.HasValue)
            {
                Restaurant = _restauranData.GetById(restaurantId.Value);
            }
            else
            {
                Restaurant = new Restaurant();
            }


            if (Restaurant == null)
            {
                return RedirectToPage("./NotFound");
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                Cuisines = _htmlHelper.GetEnumSelectList<CuisineType>();
                return Page();
            }

            if (Restaurant.Id > 0)
            {
                _restauranData.Update(Restaurant);
            }

            else
            {
                _restauranData.Add(Restaurant);
            }
            _restauranData.Commit();
            TempData["Message"] = "Restaurant Saved";
            return RedirectToPage("./Detail", new { restaurantId = Restaurant.Id });
        }
    }
}