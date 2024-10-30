using CandidateManagement.BussinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateManagement.DAO
{
    public class HRAccountDAO
    {
        private GenericDAO<Hraccount> hrAccountDAO;
        private CandidateManagementContext dbContext;

        private static HRAccountDAO instance = null;

        public static HRAccountDAO Instance
        {
            get
            {//singleton

                if (instance == null)
                {
                    instance = new HRAccountDAO();
                }
                return instance;

            }
        }
        public HRAccountDAO()
        {
            hrAccountDAO = new GenericDAO<Hraccount>(new CandidateManagementContext());

        }
        public Hraccount GetHraccountByEmail(string email)
        {
            return hrAccountDAO.GetById(email);
        }
        public List<Hraccount> GetHraccounts()
        {
            return hrAccountDAO.GetAll();
        }


    }
}
