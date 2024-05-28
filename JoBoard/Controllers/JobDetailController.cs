using AutoMapper;
using JoBoard.App_Start;
using JoBoard.Data;
using JoBoard.Models;
using JoBoard.Services;
using Microsoft.AspNet.Identity;
using PagedList;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace JoBoard.Controllers
{
    public class JobDetailController : Controller
    {

        private readonly PostedJobService PjobService;
        private readonly SavedJobsService SjobService;
        private readonly AppliedJobsService AjobService;
        private readonly IMapper mapper;
        JoBoard_DBEntities1 db = new JoBoard_DBEntities1();

        public JobDetailController()
        {
            SjobService = new SavedJobsService();
            PjobService = new PostedJobService();
            AjobService = new AppliedJobsService();
            mapper = AutoMapperConfig.Mapper;
        }

        // GET: Home/Details/5
        public ActionResult Index(int? Id, SavedJob Sjob)
        {
            if (Id == null)
                return HttpNotFound();

            JobDetailModel jobModel = new JobDetailModel();

            Sjob.UserId = User.Identity.GetUserId();

            var IsSaved = SjobService.ReadSJobsByUser(Id.Value,Sjob.UserId);

            var IsApplied = AjobService.ReadAJobsByCondida(Id.Value,Sjob.UserId);

            if (IsSaved != null && User.Identity.IsAuthenticated == true)
            {
              ViewBag.Success = true;
            }
            else
            { ViewBag.Success = false; }



            if (IsApplied != null && User.Identity.IsAuthenticated == true)
            {
                ViewBag.Result = true;
            }
            else
            { ViewBag.Result = false; }

            var currentJob = PjobService.ReadJobsById(Id.Value);
            jobModel.Job = mapper.Map<JobModel>(currentJob);

            return View(jobModel);
        }


        public ActionResult ApplyJobPartial()
        {

            return View();
        }


        [HttpPost]
        public ActionResult ApplyJobPartial(JobDetailModel Ajob, HttpPostedFileBase postedFile)
        { 

            if (ModelState.IsValid)
            {


                byte[] bytes;
                using (BinaryReader br = new BinaryReader(postedFile.InputStream))
                {
                    bytes = br.ReadBytes(postedFile.ContentLength);
                }

                Ajob.Resume = bytes;
                Ajob.FileName = Path.GetFileName(postedFile.FileName);
                Ajob.ContentType = postedFile.ContentType;

                var job = PjobService.ReadPJobsById(Ajob.JobId);

                Ajob.UserId = job.UserId;
                Ajob.CondidaId = User.Identity.GetUserId();


                var jobDTO = mapper.Map<AppliedJob>(Ajob);

                int result1 = SjobService.ApplyJob(jobDTO);


                if (result1 >= 1)
                {
                    ViewBag.Message = "Sucess!";
                    return RedirectToAction("Index", "JobDetail", new { id = Ajob.JobId });
                }
                ViewBag.Message = "An error occurred!";
            }
            return View(Ajob);
        }

        //SAVE : jobs
        public ActionResult SaveJob()
        {
            JobModel job = new JobModel();

            return View(job);
        }


        [HttpPost]
        public ActionResult SaveJob(SavedJobs Sjob)
        {

            try
            {
                Sjob.UserId = User.Identity.GetUserId();
                var job = mapper.Map<SavedJob>(Sjob);

                var savingJob = SjobService.SaveJob(job);

                if (savingJob >= 1)
                {
                    return Json(new { saved = true });
                }
                else
                {
                    return Json(new { saved = false });
                }
            }
            catch (Exception ex)
            {
                return Json(new { saved = false, message = ex.Message });
            }
        }



        //DELETE : Savedjobs

        public ActionResult DeleteSavedJobs()
        {

            return View();
        }

        [HttpPost]
        public ActionResult DeleteSavedJobs(int? Id)
        {

            if (Id != null)
            {
                var userId = User.Identity.GetUserId();

                var job = SjobService.ReadSJobsByUser(Id.Value, userId);

                var jobInfo = mapper.Map<SavedJobs>(job);

                var deleted = SjobService.DeleteSavedJobs(Id.Value, userId);

                if (deleted)
                {
                    return RedirectToAction("PostedJobs", "Jobs");
                }
                return RedirectToAction("Delete", new { Id = Id });
            }
            return HttpNotFound();
        }

        public ActionResult RelatedJobs( int ? page,int id, string query = null)
        {
            IndexViewModel model = new IndexViewModel();

            int pageSize = 5;
            var pageIndex = page ?? 1;

            var allStatelist = PjobService.ReadAllStates();


            var jobs = PjobService.ReadAllRelated(id,query);

            ViewBag.Count = jobs.Count(); 

            


            model.Jobs = mapper.Map<List<JobModel>>(jobs).OrderByDescending(i => i.Id).ToPagedList(pageIndex, pageSize);


            return View(model);
        }
    }
}