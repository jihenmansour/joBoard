using AutoMapper;
using JoBoard.App_Start;
using JoBoard.Data;
using JoBoard.Models;
using JoBoard.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JoBoard.Controllers
{
    public class SavedJobsController : Controller
    {
        private readonly SavedJobsService SjobService;
        private readonly IMapper mapper;

        public SavedJobsController()
        {
            SjobService= new SavedJobsService();
            mapper = AutoMapperConfig.Mapper;

        }


        // GET: SavedJobs
        public ActionResult Index()
        {
            string userId = User.Identity.GetUserId();
            var jobs = SjobService.GetSJobs(userId);

            var jobsDTO = mapper.Map<IEnumerable<SavedJob>, IEnumerable<SavedJobs>>(jobs).OrderByDescending(j => j.Registration_Date);

            return View(jobsDTO);
        }


        //DELETE : jobs

        public ActionResult Delete()
        {
            return View("PostedJobs", "Jobs");
        }

        [HttpPost]
        public ActionResult DeleteConfirmed(int? Id)
        {

            if (Id != null)
            {
               var userId = User.Identity.GetUserId();
                var deleted = SjobService.DeleteSavedJobs(Id.Value, userId);

                if (deleted)
                {
                    return Json(Id);
                }
            }
            return new EmptyResult();
        }

    }
}