using CandidateManagement.BussinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateManagement.DAO
{
    public class CandidateProfileDAO
    {
        private CandidateManagementContext context;
        private static CandidateProfileDAO instance;
        public CandidateProfileDAO()
        {
            context = new CandidateManagementContext();
        }
        public static CandidateProfileDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CandidateProfileDAO();

                }
                return instance;
            }
        }
        public CandidateProfile GetCandidateProfileById(string id)
        {
            return context.CandidateProfiles.SingleOrDefault(t=> t.CandidateId.Equals(id));
        }
        public List<CandidateProfile> GetCandidateProfiles()
        {
            return context.CandidateProfiles.ToList();
        }
        public bool AddCandidateProfile(CandidateProfile candidateProfile)
        {
            //lam them if ID da ton tai
            CandidateProfile candidate = this.GetCandidateProfileById(candidateProfile.CandidateId);

            bool isSuccess = false;
            try
            {
                if (candidateProfile != null)
                {
                    context.CandidateProfiles.Add(candidateProfile);
                    context.SaveChanges();
                    isSuccess = true;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return isSuccess;
        }
        public bool DeleteCandidateProfile(string profileID)
        {
            bool isSuccess = false;
            CandidateProfile candidateProfile = this.GetCandidateProfileById(profileID);
            try
            {
                if (candidateProfile != null)
                {
                    context.CandidateProfiles.Remove(candidateProfile);
                    context.SaveChanges();
                    isSuccess = true;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return isSuccess;
        }
        public bool UpdateCandidateProfile(CandidateProfile candidate)
        {
            bool isSuccess = false;
            CandidateProfile candidateProfile = this.GetCandidateProfileById(candidate.CandidateId);
            try
            {
                if (candidateProfile != null)
                {
                    context.Entry<CandidateProfile>(candidate).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                    context.Entry<CandidateProfile>(candidate).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
                    isSuccess = true;
                }

            }
            catch (Exception ex)
            {
                //Write log
                throw new Exception(ex.Message);
            }
            return isSuccess;
        }
    }
}
