using HRM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Repositories.Interfaces
{
    public interface IQualificationSerivce
    {
        void Add(Qualification item);
        List<Qualification> GetQualifications(int employeeId, string type=null);
        
        Qualification GetQualificationById(int id);
        Qualification GetQualificationById(int id,int employeeId);
        void Update(Qualification item);

        void Delete(int qid, int employeeId);

    }
    public class QualficationService : IQualificationSerivce
    {
        private IUnitOfWork _unitOfWork;

        public QualficationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Add(Qualification item)
        {
            _unitOfWork.GenericRepository<Qualification>().Add(item);
            _unitOfWork.Save();
        }

        public void Delete(int qid, int employeeId)
        {
            var t = _unitOfWork.GenericRepository<Qualification>().Get().Where(t=>t.Id ==qid && t.EmployeeId == employeeId).FirstOrDefault();
            if (t != null)
                _unitOfWork.GenericRepository<Qualification>().Delete(t);
            _unitOfWork.Save();
        }

        public Qualification GetQualificationById(int id)
        {
            return _unitOfWork.GenericRepository<Qualification>().GetById(id);
        }

        public Qualification GetQualificationById(int id, int employeeId)
        {
            return _unitOfWork.GenericRepository<Qualification>().Get().Where(t => t.Id==id &&  t.EmployeeId == employeeId).FirstOrDefault();
        }

        public List<Qualification> GetQualifications(int employeeId , string type=null)
        {
            var temp = _unitOfWork.GenericRepository<Qualification>().Get().Where(t => (t.EmployeeId == employeeId));
            if (type != null)
                temp = temp.Where(t => t.QualificationTime == type);
            return temp.ToList();
        }

        public void Update(Qualification item)
        {
            _unitOfWork.GenericRepository<Qualification>().Update(item);
            _unitOfWork.Save();
        }
    }

}
