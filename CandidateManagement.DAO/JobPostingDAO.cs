using CandidateManagement.BussinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateManagement.DAO
{
    public class JobPostingDAO
    {
        public CandidateManagementContext dbContext;
        private static JobPostingDAO instance = null;
        public static JobPostingDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new JobPostingDAO();
                }
                return instance;
            }
        }
        public JobPostingDAO()
        {
            dbContext = new CandidateManagementContext();
        }
        public JobPosting GetJobPostingID(string jobID)
        {
            return dbContext.JobPostings.SingleOrDefault(m => m.PostingId.Equals(jobID));
        }
        public List<JobPosting> GetJobPostings()
        {
            return dbContext.JobPostings.ToList();
        }
    }
}
