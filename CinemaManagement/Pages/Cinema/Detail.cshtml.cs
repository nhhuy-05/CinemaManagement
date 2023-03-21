using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CinemaManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace CinemaManagement.Pages.Cinema
{
    public class DetailModel : PageModel
    {
        private readonly CenimaDBContext _db;
        public DetailModel(CenimaDBContext db)
        {
            _db = db;
        }
        [BindProperty(SupportsGet = true)]
        public Movie movie { get; set; }
        public List<Rate> rates { get; set; }
        public void OnGet(int id)
        {
            movie = _db.Movies.Include("Genre").Where(m => m.MovieId == id).SingleOrDefault();
            rates = _db.Rates.Include("Person").Where(r => r.MovieId == id).ToList();
        }


        public double? AvgRate(List<Rate> rt)
        {
            double? avgRate = 0;
            foreach (Rate r in rt)
            {
                avgRate += Math.Round((double)(r.NumericRating / rt.Count), 2);
            }
            return avgRate;
        }
        public List<Rate> GetRateByMovieId(int id)
        {
            return _db.Rates.Where(r => r.MovieId == id).ToList();
        }
    }
}
