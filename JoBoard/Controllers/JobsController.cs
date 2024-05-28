using AutoMapper;
using JoBoard.App_Start;
using JoBoard.Data;
using JoBoard.Models;
using JoBoard.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JoBoard.Controllers
{
    public class JobsController : Controller
    {
        private readonly PostedJobService PjobService;
        private readonly AppliedJobsService AjobService;
        private readonly IMapper mapper;
        public JobsController()
        {
            PjobService = new PostedJobService();
            AjobService = new AppliedJobsService();

            mapper = AutoMapperConfig.Mapper;
        }


        public ActionResult PostedJobs()
        {
            string userId = User.Identity.GetUserId();
            var jobs = PjobService.GetPJobs(userId);

            var jobsDTO = mapper.Map<IEnumerable<PostedJob>, IEnumerable<PostedJobs>>(jobs).OrderByDescending(i => i.JobId);

            return View(jobsDTO);
        }


        // POST: Jobs

        [Authorize]
        public ActionResult PostJob()
        {
            JobModel job = new JobModel();

            var allStatelist = PjobService.ReadAllStates();
            //set defaultStateId  
            var defaultStateId = allStatelist.Select(m => m.StateId).FirstOrDefault();
            //Get all cities according to defaultStateId  
            var allCitylist = PjobService.ReadCitiesById(defaultStateId);
            //set defaultCityId   
            var defaultCityId = allCitylist.Select(m => m.CityId).FirstOrDefault();
            job.States = new SelectList(allStatelist, "StateId", "StateName", defaultStateId);
            job.Cities = new SelectList(allCitylist, "CityId", "CityName", defaultCityId);

            return View(job);
        }



        [HttpPost]
        public JsonResult setDropDrownList(string type, string value)
        {
            JobModel job = new JobModel();
            switch (type)
            {
                case "CountryId":
                    var statesList = PjobService.ReadAllStates();
                    job.States = new SelectList(statesList, "StateId", "StateName");
                    var defaultStateId = statesList.Select(m => m.StateId).FirstOrDefault();
                    job.Cities = new SelectList(PjobService.ReadCitiesById(defaultStateId).ToList(), "CityId", "CityName");
                    break;
                case "StateId":
                    job.Cities = new SelectList(PjobService.ReadCitiesById(value).ToList(), "CityId", "CityName");
                    break;
            }
            return Json(job);
        }



        [HttpPost]
        public ActionResult PostJob(JobModel job, PostedJob Pjob)
        {
            job.States = new SelectList(PjobService.ReadAllStates(), "StateId", "StateName", job.StateId);
            job.Cities = new SelectList(PjobService.ReadCitiesById(job.StateId), "CityId", "CityName", job.CityId);

            /*var TModel = new Tuple<JobModel, CompanyModel>(new JobModel(), new CompanyModel());*/

            if (ModelState.IsValid)
            {

                job.Company_Logo = SaveImageFile(job.LogoFile, job.Company_Logo);


                Pjob.UserId = User.Identity.GetUserId();
                Pjob.JobId = job.Id;



                var jobDTO = mapper.Map<Job>(job);

                int result1 = PjobService.PostJob(jobDTO, Pjob);

                if (result1 >= 1)
                {
                    return RedirectToAction("PostedJobs", "Jobs");
                }
                ViewBag.Message = "An error occurred!";
            }


            return View(job);
        }


        //EDIT : jobs


        public ActionResult Edit(int? Id)
        {


            if (Id == null)
                return HttpNotFound();


            var currentJob = PjobService.ReadJobsById(Id.Value);
            var jobModel = mapper.Map<JobModel>(currentJob);

            var allStatelist = PjobService.ReadAllStates();
            //Get all cities according to defaultStateId  
            var allCitylist = PjobService.ReadCitiesById(jobModel.StateId);  
            jobModel.States = new SelectList(allStatelist, "StateId", "StateName");
            jobModel.Cities = new SelectList(allCitylist, "CityId", "CityName");

            return View(jobModel);
        }

        [HttpPost]
        public ActionResult Edit(JobModel job)
        {
            job.States = new SelectList(PjobService.ReadAllStates(), "StateId", "StateName", job.StateId);
            job.Cities = new SelectList(PjobService.ReadCitiesById(job.StateId), "CityId", "CityName", job.CityId);


            try
            {
                if (ModelState.IsValid)
                {


                    job.Company_Logo = SaveImageFile(job.LogoFile, job.Company_Logo);



                    var jobDTO = mapper.Map<Job>(job);


                    int result = PjobService.UpdatePJob(jobDTO);

                    if (result >= 1)
                    {
                        return RedirectToAction("PostedJobs", "Jobs");
                    }
                    ViewBag.Message = "An error occurred!";
                }
                return View(job);
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(job);
            }
        }

        //DELETE : jobs

        public ActionResult Delete(int? Id)
        {
            if (Id != null)
            {
                var job = PjobService.ReadPJobsById(Id.Value);

                var jobInfo = mapper.Map<PostedJobs>(job);

                /* var categoryInfo = new CategoryModel
                 {
                     Id = category.ID,
                     Name = category.Name,
                     ParentName = category.Category2?.Name
                 };*/

                return View(jobInfo);
            }
            return RedirectToAction("PostedJobs", "Jobs");
        }

        [HttpPost]
        public ActionResult DeleteConfirmed(int? Id)
        {

            if (Id != null)
            {
                var deleted = PjobService.DeletePJob(Id.Value);

                if (deleted)
                {
                    return RedirectToAction("PostedJobs", "Jobs");
                }
                return RedirectToAction("Delete", new { Id = Id });
            }
            return HttpNotFound();
        }


        //Save Image
        private string SaveImageFile(HttpPostedFileBase imageFile, string currentImageId = "")
        {
            if (imageFile != null)
            {

                var fileExtenstion = Path.GetExtension(imageFile.FileName);


                var imageGuid = Guid.NewGuid().ToString();

                string imageId = imageGuid + fileExtenstion;


                //Save file
                string filePath = Server.MapPath($"~/Uploads/Jobs/{imageId}");
                imageFile.SaveAs(filePath);

                //Delete old file - update action
                if (!string.IsNullOrEmpty(currentImageId))
                {
                    string oldFilePath = Server.MapPath($"~/Uploads/Jobs/{currentImageId}");
                    System.IO.File.Delete(oldFilePath);
                }

                return imageId;
            }
            return currentImageId;
        }


        public ActionResult GetAppliedJobs(int id)
        {
            string userId = User.Identity.GetUserId();
            var jobs = AjobService.GetAJobsById(userId,id);

            var jobsDTO = mapper.Map<IEnumerable<AppliedJob>, IEnumerable<JobDetailModel>>(jobs).OrderByDescending(j => j.Id);


            return View(jobsDTO);
        }


        //DELETE : jobs

        public ActionResult Delete()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DeleteAConfirmed(int? Id)
        {

            if (Id != null)
            {
                var userId = User.Identity.GetUserId();
                var deleted = AjobService.DeleteAppliedJob(Id.Value, userId);

                if (deleted)
                {
                    return Json(Id);
                }
            }
            return new EmptyResult();
        }

        [HttpPost]
        public FileResult DownloadFile(int? fileId)
        {
            var file = AjobService.ReadFileById(fileId.Value);
            return File(file.Resume, file.ContentType, file.FileName);
        }
    }
}