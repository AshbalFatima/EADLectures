using HRM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Repositories.Interfaces
{
    public interface ILHCData
    {
        void Insert(LHCData item);
        void Update(LHCData item);
        void AddRange(List<LHCData> items);
        List<LHCData> Get(string cnic, string personal);
        LHCData GetById(int id);

    }
    public class LHCDataService : ILHCData
    { 
        private readonly IUnitOfWork _unitOfWork;

        public LHCDataService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void AddRange(List<LHCData> items)
        {
            _unitOfWork.GenericRepository<LHCData>().AddRange(items);
            _unitOfWork.Save();
        }

        public List<LHCData> Get(string cnic,string personal)
        {
            return _unitOfWork.GenericRepository<LHCData>().Get().Where(t => 
            (cnic != null  && t.CNIC.Contains(cnic)) || 
            (personal != null  && t.PersonalNumber.Contains(personal))).ToList();
        }

    
        public LHCData GetById(int id)
        {
            return _unitOfWork.GenericRepository<LHCData>().GetById(id);
        }

        public void Insert(LHCData item)
        {
            _unitOfWork.GenericRepository<LHCData>().Add(item);
            _unitOfWork.Save();
        }

        public void Update(LHCData item)
        {
            _unitOfWork.GenericRepository<LHCData>().Update(item);
            _unitOfWork.Save();
        }
    }
}
