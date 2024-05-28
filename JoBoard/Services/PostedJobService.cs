using JoBoard.Data;
using JoBoard.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.WebPages;

namespace JoBoard.Services
{
    public interface IPostedJobService
    {
        IEnumerable<PostedJob> ReadAll();
        IEnumerable<string> ReadCompanies();
        int PostJob(Job job, PostedJob Pjob);
        IEnumerable<PostedJob> GetPJobs(string userId);
        List<Job> ReadAll(string query = null, string StateId = null, string Type = null);
        PostedJob ReadPJobsById(int id);
        Job ReadJobsById(int id);
        bool DeletePJob(int JobId);
        int UpdatePJob(Job job);
        List<state> ReadAllStates();
        List<city> ReadCitiesById(string StateId);
        List<Job> ReadAllRelated(int id, string query = null);

    }

    public class PostedJobService : IPostedJobService
    {
        private readonly JoBoard_DBEntities1 db;

        private readonly SavedJobsService SjobService;

        private readonly AppliedJobsService AjobService;
        public PostedJobService()
        {
            db = new JoBoard_DBEntities1();
            SjobService = new SavedJobsService();
            AjobService = new AppliedJobsService();

        }



        public bool DeletePJob(int JobId)
        {
            var jobId = ReadPJobsById(JobId);
            var id = ReadJobsById(JobId);


            if (jobId != null || id != null)
            {
                SjobService.DeleteSavedJob(JobId);
                db.PostedJobs.Remove(jobId);
                AjobService.DeleteAppliedJobs(JobId);
                db.Jobs.Remove(id);
                return db.SaveChanges() > 0 ? true : false;
            }
            return false;
        }

        public IEnumerable<PostedJob> GetPJobs(string userId)
        {
            return db.PostedJobs.Where(j => j.UserId == userId);
        }

        public int PostJob(Job job, PostedJob Pjob)
        {
            job.Creation_Date = DateTime.Now;


            db.PostedJobs.Add(Pjob);
            db.Jobs.Add(job);

            return db.SaveChanges();
        }

        public List<Job> ReadAll(string query = null, string StateId = null, string Type = null)
        {
            return db.Jobs.Include(j => j.state).Include(j => j.city).Where(j =>
                (StateId == null || j.StateId == StateId)
                && (Type == null || j.Job_Type.Contains(Type))
                && (query == null || j.Job_Title.Contains(query))).ToList();
        }

        public IEnumerable<PostedJob> ReadAll()
        {
            return db.PostedJobs.ToList();
        }

        public List<Job> ReadAllRelated(int id, string query = null)
        {
            return db.Jobs.Where(j =>
             query == null || j.Job_Title.Contains(query) && j.Id != id).ToList();
        }

        public List<state> ReadAllStates()
        {
            return db.states.ToList();
        }

        public List<city> ReadCitiesById(string StateId = null)
        {
            return db.cities.Where(j => j.StateId.ToString() == StateId).ToList();
        }

        public IEnumerable<string> ReadCompanies()
        {
            return db.Jobs.Select(j => j.Company_Name).Distinct().ToList();
        }

        public Job ReadJobsById(int Id)
        {
            return db.Jobs.Find(Id);
        }

        public PostedJob ReadPJobsById(int jobId)
        {
            return db.PostedJobs.FirstOrDefault(J => J.JobId == jobId);
        }

        public int UpdatePJob(Job job)
        {
            db.Jobs.Attach(job);
            db.Entry(job).State = System.Data.Entity.EntityState.Modified;
            return db.SaveChanges();
        }

    }
}