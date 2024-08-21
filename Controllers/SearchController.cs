using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Search_app.Controllers
{
    public class SearchController : Controller
    {
        // Sample search data
        private static List<User> searchData = new List<User>
        {
            new User { Id = 1, FirstName = "Antony", LastName = "Fitt", Email = "afitt0@a8.net", Gender = "Male" },
            new User { Id = 2, FirstName = "Katey", LastName = "Gaines", Email = "kgaines1@bbb.org", Gender = "Female" },
            // Add the rest of the data here...
        };

        [HttpGet("/")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("/submit")]
        public IActionResult Submit([FromForm] string searchText)
        {
            var result = SequentialSearch(searchData, searchText);
            return Content(string.Join("\n", result.Select(r => $"First Name: {r.FirstName}, Last Name: {r.LastName}")));
        }

        private List<User> SequentialSearch(List<User> searchData, string searchText)
        {
            var parts = searchText.Split(' ');
            var fname = parts[0];
            var lname = parts.Length > 1 ? parts[1] : string.Empty;

            var searchResult = new List<User>();

            foreach (var user in searchData)
            {
                if (fname == user.FirstName || 
                    fname.Substring(0, 3) == user.FirstName.Substring(0, 3) || 
                    (fname == user.FirstName && lname == user.LastName))
                {
                    searchResult.Add(user);
                }
            }

            if (searchResult.Count == 0)
            {
                searchResult.Add(new User { FirstName = "No matches found", LastName = "" });
            }

            return searchResult;
        }
    }

    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
    }
}
