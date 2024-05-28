using JoBoard.Data;
using JoBoard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JoBoard.Services
{

    public interface IAppliedJobsService
    {
        IEnumerable<AppliedJob> ReadAll();
        IEnumerable<AppliedJob> GetAJobs(string userId);
        IEnumerable<AppliedJob> GetAJobsById(string userId, int JobId);
        AppliedJob ReadAJobsById(int id);
        AppliedJob ReadFileById (int id);
        bool DeleteAppliedJobs(int JobId);
        AppliedJob ReadAJobsByUser(int id, string UserId);
        AppliedJob ReadAJobsByCondida(int JobId, string CondidaId);
        bool DeleteAppliedJob(int JobId, string UserId);

    }
    public class AppliedJobsService : IAppliedJobsService
    {
        private readonly JoBoard_DBEntities1 db;
       
        public AppliedJobsService()
        {
            db = new JoBoard_DBEntities1();
        }

        public bool DeleteAppliedJob(int JobId, string UserId)
        {
            var jobId = ReadAJobsByUser(JobId, UserId);

            if (jobId != null)
            {
                db.AppliedJobs.Remove(jobId);
                return db.SaveChanges() > 0 ? true : false;
            }
            return false;
        }

        public bool DeleteAppliedJobs(int JobId)
        {
            var jobId = ReadAJobsById(JobId);

            if (jobId != null)
            {
                db.AppliedJobs.Remove(jobId);
                return db.SaveChanges() > 0 ? true : false;
            }
            return false;
        }

        public IEnumerable<AppliedJob> GetAJobs(string userId)
        {
            return db.AppliedJobs.Where(j => j.UserId == userId);
        }

        public IEnumerable<AppliedJob> GetAJobsById(string userId, int JobId)
        {
            return db.AppliedJobs.Where(j => j.UserId == userId && j.JobId == JobId);
        }

        public AppliedJob ReadAJobsByCondida(int JobId, string CondidaId)
        {
            return db.AppliedJobs.FirstOrDefault(J => J.JobId == JobId && J.CondidaId == CondidaId);
        }

        public AppliedJob ReadAJobsById(int id)
        {
            return db.AppliedJobs.FirstOrDefault(J => J.JobId == id);
        }

        public AppliedJob ReadAJobsByUser(int id, string UserId)
        {
            return db.AppliedJobs.FirstOrDefault(j => j.UserId == UserId && j.JobId == id);
        }

        public IEnumerable<AppliedJob> ReadAll()
        {
            return db.AppliedJobs.ToList();
        }

        public AppliedJob ReadFileById(int id)
        {
           return db.AppliedJobs.ToList().Find(p => p.Id == id);
        }
    }
}