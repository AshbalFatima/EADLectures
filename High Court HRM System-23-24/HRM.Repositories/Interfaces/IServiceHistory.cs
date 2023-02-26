using HRM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Repositories.Interfaces
{
    public interface IServiceHistory 
    {
        void Add(ServiceHistory item);
        void Update(ServiceHistory item);
        List<ServiceHistory> GetList(int employeeId);
        ServiceHistory GetById(int employeeId, int id);
        void Delete(int employeeId, int id);
        
    }
    public class ServiceHistoryService : IServiceHistory
    {
        private readonly IUnitOfWork _unitOfWork;

        public ServiceHistoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Add(ServiceHistory item)
        {
            _unitOfWork.GenericRepository<ServiceHistory>().Add(item);
            _unitOfWork.Save();
        }

        public void Delete(int employeeId, int id)
        {
            var item = _unitOfWork.GenericRepository<ServiceHistory>().Get().Where(t=>t.Id==id && t.EmployeeId==employeeId).FirstOrDefault();
            if(item!=null)
                _unitOfWork.GenericRepository<ServiceHistory>().Delete(item);
            _unitOfWork.Save();
        }

        public ServiceHistory GetById(int employeeId, int id)
        {
            return _unitOfWork.GenericRepository<ServiceHistory>().Get().Where(t => t.EmployeeId == employeeId && t.Id ==id).FirstOrDefault();
        }

        public List<ServiceHistory> GetList(int employeeId)
        {
            return _unitOfWork.GenericRepository<ServiceHistory>().Get().Where(t => t.EmployeeId == employeeId).ToList();
                
         
        }

        public void Update(ServiceHistory item)
        {
            _unitOfWork.GenericRepository<ServiceHistory>().Update(item);
            _unitOfWork.Save();
        }
    }
}
