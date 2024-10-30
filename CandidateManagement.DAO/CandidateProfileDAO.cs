using CandidateManagement.BussinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
            bool isSuccess = false;
            try
            {
                var existingCandidate = this.GetCandidateProfileById(candidateProfile.CandidateId);
                if (existingCandidate != null)
                {
                    throw new Exception("A candidate with this ID already exists.");
                }

                context.CandidateProfiles.Add(candidateProfile);
                context.SaveChanges();
                isSuccess = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
            return isSuccess;
        }

        //public bool DeleteCandidateProfile(string profileID)
        //{
        //    bool isSuccess = false;
        //    CandidateProfile candidateProfile = this.GetCandidateProfileById(profileID);
        //    try
        //    {
        //        if (candidateProfile != null)
        //        {
        //            context.CandidateProfiles.Remove(candidateProfile);
        //            context.SaveChanges();
        //            isSuccess = true;
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //    return isSuccess;
        //}
        public bool DeleteCandidateProfile(string profileID)
        {
            bool isSuccess = false;

            try
            {
                var candidateProfile = context.CandidateProfiles
                    .AsNoTracking()
                    .FirstOrDefault(c => c.CandidateId == profileID);

                if (candidateProfile != null)
                {
                    var trackedProfile = context.CandidateProfiles.Local
                        .FirstOrDefault(c => c.CandidateId == profileID);

                    if (trackedProfile != null)
                    {
                        context.Entry(trackedProfile).State = EntityState.Detached;
                    }
                    context.CandidateProfiles.Attach(candidateProfile);
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
