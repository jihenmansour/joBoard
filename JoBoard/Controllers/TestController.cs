using AutoMapper;
using JoBoard.App_Start;
using JoBoard.Data;
using JoBoard.Models;
using JoBoard.Services;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JoBoard.Controllers
{
    public class TestController : Controller
    {

        private readonly PostedJobService PjobService;
        private readonly SavedJobsService SjobService;
        private readonly IMapper mapper;
        JoBoard_DBEntities1 db = new JoBoard_DBEntities1();

        public TestController()
        {
            SjobService = new SavedJobsService();
            var db = new JobsIdentityContext();
            mapper = AutoMapperConfig.Mapper;
        }

        [HttpPost]
        public ViewResult Index(int customerId)
        {
            return View("Index", db.Jobs.Find(customerId));
        }
    }


}
