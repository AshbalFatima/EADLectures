using HRM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Repositories.Interfaces
{
    public interface IUserService
    {
        bool UserExist(string CNIC, string PersonalNumber);
        bool IsLHCEmployee(string CNIC, string PersonalNumber);
    }
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationDbContext _dbContext;

        public UserService(IUnitOfWork unitOfWork,ApplicationDbContext context)
        {
            _unitOfWork = unitOfWork;
            _dbContext = context;
        }

        public bool IsLHCEmployee(string CNIC, string PersonalNumber)
        {
            var found = _unitOfWork.GenericRepository<LHCData>().Get().Where(t=>t.CNIC == CNIC.Replace("-","") && t.PersonalNumber == PersonalNumber).FirstOrDefault();
            return found != null;
        }

        public bool UserExist(string CNIC, string PersonnelNumber)
        {
            var found = _dbContext.ApplicationUsers.Where(t => t.CNIC == CNIC.Replace("-", "") && t.PersonnelNumber == PersonnelNumber).FirstOrDefault();

            //var Foud = _unitOfWork.IdentityUser>().Get().Where(t => t.CNIC == CNIC.Replace("-", "") && t.PersonalNumber == PersonalNumber).FirstOrDefault();
            return found!= null;
        }
    }
}
