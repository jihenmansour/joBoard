using JoBoard.Data;
using JoBoard.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace JoBoard.Models
{
    public class JobModel 
    {
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "The Job Title field is required.")]
        public string Job_Title { get; set; }

        [Required(ErrorMessage = "The Job State is required.")]
        public string StateId { get; set; }
        public SelectList States { get; set; }
        public state state { get; set; }

        [Required(ErrorMessage = "The Job city is required.")]
        public int CityId { get; set; }
        public SelectList Cities { get; set; }
        public city city { get; set; }
        public string StateName { get; set; }
        public string CityName { get; set; }
        public string Job_Type { get; set; }
        public string Gender { get; set; }

        [Required(ErrorMessage = "The Job Description field is required.")]
        public string Job_Description { get; set; }
        public string Job_Requirements { get; set; }
        public System.DateTime Creation_Date { get; set; }

        [ValidDeadline(ErrorMessage = "Invalide Date!")]
        public Nullable<System.DateTime> Deadline { get; set; }

        [Required(ErrorMessage = "The Company Name field is required.")]
        public string Company_Name { get; set; }
        public string Company_Description { get; set; }
        public string Company_Website { get; set; }
        public string Company_LinkedIn { get; set; }
        public string Company_Logo { get; set; }
        private string company_Logo {
            set
            {
                Company_Logo = string.IsNullOrWhiteSpace(value) ? "empty.jpg" : value;
            }
            get
            {
                return Company_Logo;
            }
        }
        public HttpPostedFileBase LogoFile { get; set; }
        public List<JobModel> Jobs { get; set; }

    }

    public class State
    {
        [Key]
        public string StateId { get; set; }
        public string StateName { get; set; }
    }
    public class City
    {
        [Key]
        public int CityId { get; set; }
        public string CityName { get; set; }
        public string StateId { get; set; }
        [ForeignKey("StateId")]
        public State State { get; set; }
    }
    public enum type
    {
        [Display(Name = "Part Time")]
        PartTime,

        [Display(Name = "Full Time")]
        FullTime,

    }

    public enum gender
    {
        Any,

        Female,

        Male,

    }


    public class PostedJobs
    {
        public string  UserId { get; set; }
        public int JobId { get; set; }
        public JobModel Job { get; set; }
    }
    public class SavedJobs
    {
        public string UserId { get; set; }
        public int JobId { get; set; }
        public System.DateTime Registration_Date { get; set; }
        public JobModel Job { get; set; }
    }

}