using JoBoard.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Xml.Linq;

namespace JoBoard.Models
{
    public class IndexViewModel
    {
        public IPagedList<JobModel> Jobs { get; set; }
        public string StateId { get; set; }
        public SelectList States { get; set; }
        public string type { get; set; }
        public string Query { get; set; }

    }

    public enum TheType
    {
        [Display(Name = "Part Time")]
        PartTime,

        [Display(Name = "Full Time")]
        FullTime,

    }
}