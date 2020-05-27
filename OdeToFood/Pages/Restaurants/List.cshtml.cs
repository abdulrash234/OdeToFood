using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using OdeToFood.Core;
using OdeToFood.Data;

namespace OdeToFood.Pages.Restaurants
{
    public class ListModel : PageModel
    {
        private readonly IConfiguration _config;
        private readonly IRestaurantData _restaurantData;

        public string Message { get; set; }
        public IEnumerable<Restaurant> Restaurants;
        
        [BindProperty]
        public string SearchTerm { get; set; }
        public ListModel(IConfiguration config, IRestaurantData restaurantData, IEnumerable<Restaurant> restaurants)
        {
            _config = config;
           _restaurantData = restaurantData;
        }

        public void OnGet(string searchTerm)
        {
            Message = _config["Message"];
            Restaurants = _restaurantData.GetRestaurantsByName(searchTerm);
        }
    }
}