using AutoMapper;
using JoBoard.App_Start;
using JoBoard.Data;
using JoBoard.Models;
using JoBoard.Services;
using PagedList;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.WebPages;
using System.Xml.Linq;

namespace JoBoard.Controllers
{
    public class HomeController : Controller
    {
        private readonly PostedJobService PjobService;
        private readonly SavedJobsService SjobService;
        private readonly AppliedJobsService AjobService;
        private readonly IMapper mapper;
        private readonly JoBoard_DBEntities1 db;

        public HomeController()
        {
            PjobService = new PostedJobService();
            SjobService = new SavedJobsService();
            AjobService = new AppliedJobsService();
            mapper = AutoMapperConfig.Mapper;
            db = new JoBoard_DBEntities1();
        }



        public ActionResult Index(int? page, string query = null, string StateId = null, string type = null)
        {

            IndexViewModel model = new IndexViewModel();

            int pageSize = 9;
            var pageIndex = page ?? 1;
            ViewBag.CountCondida = AjobService.ReadAll().Count();
            ViewBag.CountJobs = PjobService.ReadAll().Count();
            ViewBag.CountCompanies = PjobService.ReadCompanies().Count();

            var allStatelist = PjobService.ReadAllStates();

            model.States = new SelectList(allStatelist, "StateId", "StateName");
            if(string.IsNullOrEmpty(StateId))
            {
                StateId = null;
            }

            var jobs = PjobService.ReadAll(query, StateId, type);


            model.Jobs = mapper.Map<List<JobModel>>(jobs).OrderByDescending(i => i.Id).ToPagedList(pageIndex, pageSize);


            return View(model);
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



        // GET: Home/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Home/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Home/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Home/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Home/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Home/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
