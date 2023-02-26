using HRM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Repositories.Interfaces
{
    public interface IEmployeeService
    {
        HRM.Models.Employee GetbyID(int ID);
        HRM.Models.Employee GetbyUserId(string User_Id);
        HRM.Models.Employee VerifyData(int EmployeeId,string VerifyedBy);
        void Add(Employee employee);
        void Update(Employee employee);
        
        
        void Save();

    }
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Add(Employee employee)
        { 
            _unitOfWork.GenericRepository<Employee>().Add(employee);
        }
        public void Update(Employee employee)
        {
            _unitOfWork.GenericRepository<Employee>().Update(employee);
        }
        public void Save()
        {
            while (_unitOfWork.IsBusy())
            { 
            
            }
            _unitOfWork.Save();
        }
        public Employee GetbyID(int ID)
        {
            return _unitOfWork.GenericRepository<Employee>().GetById(ID);
        }

        public Employee GetbyUserId(string User_Id)
        {
            return _unitOfWork.GenericRepository<Employee>().GetAll(x => x.UserId == User_Id).FirstOrDefault();
        }

        
        public Employee VerifyData(int EmployeeId, string VerifyedBy)
        {
            throw new NotImplementedException();
        }
    }
}
