using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common.ViewModel;

namespace Web.Models
{
    public class ResultsViewModel
    {
        [Display(Name="Local Authority")]
        public int SelectedAuthorityId { get; set; }
        public IEnumerable<RatingViewModel> Ratings { get; set; } 
        
    }
}