using HRM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Repositories.Interfaces
{
    public interface IAppointmentService
    {
        void Add(Appointment item);
        List<Appointment> GetAppointments(int employeeId);

        Appointment GetAppointmentById(int id);
        Appointment GetAppointmentById(int id, int employeeId);
        void Update(Appointment item);

        void Delete(int qid, int employeeId);
    }
    public class AppointmentService : IAppointmentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppointmentService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public void Add(Appointment item)
        {
            _unitOfWork.GenericRepository<Appointment>().Add(item);
            _unitOfWork.Save();
        }

        public void Delete(int id, int employeeId)
        {
            var temp = _unitOfWork.GenericRepository<Appointment>().Get().Where(t => t.Id == id && t.EmployeeId == employeeId).FirstOrDefault();
            if(temp != null)
                _unitOfWork.GenericRepository<Appointment>().Delete(temp);
            _unitOfWork.Save();
        }

        public Appointment GetAppointmentById(int id)
        {
            return _unitOfWork.GenericRepository<Appointment>().GetById(id);
            
        }

        public Appointment GetAppointmentById(int id, int employeeId)
        {
            return _unitOfWork.GenericRepository<Appointment>().Get().Where(t=>t.Id == id && t.EmployeeId == employeeId).FirstOrDefault();
        }

        public List<Appointment> GetAppointments(int employeeId)
        {
            return _unitOfWork.GenericRepository<Appointment>().Get().Where(t => t.EmployeeId == employeeId).ToList();


        }

        public void Update(Appointment item)
        {
            _unitOfWork.GenericRepository<Appointment>().Update(item);
            _unitOfWork.Save();
        }
    }

}
