using JoBoard.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace JoBoard.Services
{

    public interface ISavedJobService
    {
        IEnumerable<SavedJob> ReadAll();
        int SaveJob(SavedJob Sjob);
        IEnumerable<SavedJob> GetSJobs(string userId);
        SavedJob ReadSJobsByUser(int jobId, string UserId);
        SavedJob ReadSJobById (int jobId);
        bool DeleteSavedJob(int JobId);
        bool DeleteSavedJobs(int JobId, string UserId);
        int ApplyJob(AppliedJob Ajob);


    }
    public class SavedJobsService : ISavedJobService
    {
        private readonly JoBoard_DBEntities1 db;
        public SavedJobsService()
        {
            db = new JoBoard_DBEntities1();

        }

        public int SaveJob(SavedJob Sjob)
        {

            Sjob.Registration_Date = DateTime.Now;

            db.SavedJobs.Add(Sjob);

            return db.SaveChanges();
        }

        public SavedJob ReadSJobsByUser(int jobId, string UserId)
        {
            return db.SavedJobs.FirstOrDefault(J => J.JobId == jobId &&  J.UserId == UserId);
        }

        public bool DeleteSavedJobs(int JobId, string UserId)
        {
            var jobId = ReadSJobsByUser(JobId,UserId);

            if (jobId != null)
            {
                db.SavedJobs.Remove(jobId);
                return db.SaveChanges() > 0 ? true : false;
            }
            return false;
        }

        public IEnumerable<SavedJob> GetSJobs(string userId)
        {
            return db.SavedJobs.Where(j => j.UserId == userId);
        }

        public int ApplyJob(AppliedJob Ajob)
        {
            db.AppliedJobs.Add(Ajob);

            return db.SaveChanges();
        }

        public SavedJob ReadSJobById(int jobId)
        {
            return db.SavedJobs.FirstOrDefault(J => J.JobId == jobId);
        }

        public bool DeleteSavedJob(int JobId)
        {
            var jobId = ReadSJobById(JobId);

            if (jobId != null)
            {
                db.SavedJobs.Remove(jobId);
                return db.SaveChanges() > 0 ? true : false;
            }
            return false;
        }

        public IEnumerable<SavedJob> ReadAll()
        {
            return db.SavedJobs.ToList();
        }
    }
}