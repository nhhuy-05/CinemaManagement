using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CinemaManagement.Models
{
    public partial class Rate
    {
        public int MovieId { get; set; }
        public int PersonId { get; set; }
        [Required(ErrorMessage = "Hãy điền comment")]
        public string Comment { get; set; }
        [Required(ErrorMessage = "Hãy điền điểm đánh giá")]
        public double? NumericRating { get; set; }
        public DateTime? Time { get; set; }

        public virtual Movie Movie { get; set; }
        public virtual Person Person { get; set; }
    }
}
