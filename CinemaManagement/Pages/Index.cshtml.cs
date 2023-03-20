using CinemaManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CinemaManagement.Pages
{
    [BindProperties]
    public class IndexModel : PageModel
    {
        private readonly CenimaDBContext _context;

        public IndexModel(CenimaDBContext context)
        {
            _context=context;
        }
        [BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; } = 1;
        public int Count() { 
            return _context.Movies.Count();
        }
        public int PageSize { get; set; } = 4;

        public int TotalPages => Count()/PageSize;

        public String Search { get; set; }
        public List<Movie> movies { get; set; } 
        public List<Genre> genres { get; set; }
        public double? AvgRate(List<Rate> rt)
        {
            double? avgRate = 0;
            foreach (Rate r in rt)
            {
                avgRate+=Math.Round((double)(r.NumericRating / rt.Count),2);
            }
            return avgRate;
        }
        public List<Rate> GetRateByMovieId(int id)
        {
            return _context.Rates.Where(r => r.MovieId == id).ToList();
        }
        public void OnGet(int id)
        {
            if(id == 0)
            {
                movies = _context.Movies.Include("Genre").
                    Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();
                genres = _context.Genres.ToList();
                
            }
            else
            {
                movies = _context.Movies.Include("Genre").Where(x => x.GenreId == id).
                    Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();
                genres = _context.Genres.ToList();
                
            }
        }

        public void OnPost(int id)
        {
            Search = Request.Form["Search"];
            if (id == 0)
            {
                movies = _context.Movies.Include("Genre").Where(x => x.Title.Contains(Search)).
                    Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();
                genres = _context.Genres.ToList();
                
            }
            else
            {
                movies = _context.Movies.Include("Genre").Where(x => x.GenreId == id&&x.Title.
                    Contains(Search)).Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();
                genres = _context.Genres.ToList();
               
            }            
        }
    }
}