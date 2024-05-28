using AutoMapper;
using JoBoard.Data;
using JoBoard.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Job = JoBoard.Data.Job;

namespace JoBoard.App_Start
{
    public class AutoMapperConfig
    {
        public static IMapper Mapper { get; private set; }

        public static void Init()
        {
            var config = new MapperConfiguration(cfg =>
            {

                cfg.CreateMap<Job, JobModel>().ReverseMap();

                cfg.CreateMap<PostedJob,PostedJobs>().ReverseMap();

                cfg.CreateMap<SavedJob, SavedJobs>().ReverseMap();

                cfg.CreateMap<Data.state, State>().ReverseMap();

                cfg.CreateMap<Data.city, City>().ReverseMap();

                cfg.CreateMap<AppliedJob, JobDetailModel>().ReverseMap();

                cfg.CreateMap<tblFile, TestModel>().ReverseMap();

            });

            Mapper = config.CreateMapper();
        }
    }
}