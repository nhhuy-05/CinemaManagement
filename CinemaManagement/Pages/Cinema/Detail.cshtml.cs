using CinemaManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CinemaManagement.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

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

        public Rate userRate { get; set; }

        public Person user { get; set; }
        public void OnGet(int id)
        {
            movie = _db.Movies.Include("Genre").Where(m => m.MovieId == id).SingleOrDefault();
            rates = _db.Rates.Include("Person").Where(r => r.MovieId == id).ToList();
            string objAccount = HttpContext.Session.GetString("user");
            if(objAccount != null)
            {
                user = JsonSerializer.Deserialize<Person>(objAccount);
                userRate = _db.Rates.Where(r => (r.PersonId == user.PersonId && r.MovieId == id)).SingleOrDefault();
            }
            
        }

        public void OnPost(int id)
        {
            string objAccount = HttpContext.Session.GetString("user");
            Rate uRate = new Rate();
            if (objAccount != null)
            {
                user = JsonSerializer.Deserialize<Person>(objAccount);
                uRate = _db.Rates.Where(r => (r.PersonId == user.PersonId && r.MovieId == id)).SingleOrDefault();
            }

            string raw_number = Request.Form["userRate.NumericRating"];
            string comment = Request.Form["userRate.Comment"];

            if (uRate != null)
            {
                uRate.NumericRating = Convert.ToInt32(raw_number);
                uRate.Comment = comment;
                _db.Rates.Update(uRate);
                _db.SaveChanges();
                ViewData["msg"] = "Chỉnh sửa đánh giá thành công";
            }
            else
            {
                Rate newRate = new Rate()
                {
                    PersonId = user.PersonId,
                    MovieId = id,
                    NumericRating = double.Parse(raw_number),
                    Comment = comment,
                    Time = DateTime.Now,

                };
                _db.Rates.Add(newRate);
                _db.SaveChanges();
                ViewData["msg"] = "Thêm đánh giá thành công";
            }
            
            movie = _db.Movies.Include("Genre").Where(m => m.MovieId == id).SingleOrDefault();
            rates = _db.Rates.Include("Person").Where(r => r.MovieId == id).ToList();
            userRate = rates.Where(r => r.PersonId == user.PersonId).SingleOrDefault();
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
