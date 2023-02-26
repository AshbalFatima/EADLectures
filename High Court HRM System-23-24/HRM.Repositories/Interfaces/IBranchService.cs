using HRM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Repositories.Interfaces
{
    public interface IBranchService
    {
        void Add(Branch branch);
        ICollection<Branch> GetAllBranches();
        ICollection<Branch> GetRootBranches();
        Branch GetBranch(int id);
        bool Delete(int id);
        void UpdateOrder(int id,bool up=true);
        void Update(Branch branch);

    }
    public class BranchService : IBranchService
    {
        private readonly IUnitOfWork _unitOfWork;
        public BranchService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public void Add(Branch branch)
        {

            var OrderBy = _unitOfWork.GenericRepository<Branch>().Get().Where(t => t.ParentBranchId == branch.ParentBranchId).FirstOrDefault();

            if (OrderBy != null)
            {
                branch.OrderBy = _unitOfWork.GenericRepository<Branch>().Get().Where(t => t.ParentBranchId == branch.ParentBranchId).Select(t => t.OrderBy).Max() + 1;
            }
            else {
                branch.OrderBy = 1;
            }
            

            _unitOfWork.GenericRepository<Branch>().Add(branch);
            _unitOfWork.Save();

        }

        public bool Delete(int id)
        {
            
            var temp = _unitOfWork.GenericRepository<Branch>().GetById(id);
            if (temp != null)
            {
                _unitOfWork.GenericRepository<Branch>().Delete(temp);
                _unitOfWork.Save();
            }
            return temp != null;

        }

        public ICollection<Branch> GetAllBranches()
        {
            return _unitOfWork.GenericRepository<Branch>().GetAll().ToList<Branch>();        
        }

        public Branch GetBranch(int id)
        {
            return _unitOfWork.GenericRepository<Branch>().GetById(id);
        }

        public ICollection<Branch> GetRootBranches()
        {
            return _unitOfWork.GenericRepository<Branch>().Get().Where(t => t.ParentBranchId == null).ToList<Branch>(); 
        }


        public void Update(Branch branch)
        {
            //var OrderBy = _unitOfWork.GenericRepository<Branch>().Get().Where(t => t.ParentBranchId == branch.ParentBranchId).FirstOrDefault();

            //if (OrderBy != null)
            //{
            //    branch.OrderBy = _unitOfWork.GenericRepository<Branch>().Get().Where(t => t.ParentBranchId == branch.ParentBranchId).Select(t => t.OrderBy).Max() + 1;
            //}
            //else
            //{
            //    branch.OrderBy = 1;
            //}

            _unitOfWork.GenericRepository<Branch>().Update(branch);
            _unitOfWork.Save();
        }

        public void UpdateOrder(int id, bool up = true)
        {
            if (up)
            {
                var current = _unitOfWork.GenericRepository<Branch>().GetById(id);
                var above = _unitOfWork.GenericRepository<Branch>().Get().Where(t => t.ParentBranchId == current.ParentBranchId && t.OrderBy < current.OrderBy ).OrderByDescending(t=>t.OrderBy).FirstOrDefault();
                if (above != null && current != null)
                {
                    var aboveOrder = current.OrderBy;
                    current.OrderBy = above.OrderBy;
                    above.OrderBy = aboveOrder;
                    _unitOfWork.GenericRepository<Branch>().Update(above);
                    _unitOfWork.GenericRepository<Branch>().Update(current);
                    _unitOfWork.Save();
                }


            }
            else
            {
                var current = _unitOfWork.GenericRepository<Branch>().GetById(id);
                var below = _unitOfWork.GenericRepository<Branch>().Get().Where(t => t.ParentBranchId == current.ParentBranchId && t.OrderBy > current.OrderBy ).FirstOrDefault();
                if (below != null && current != null)
                {
                    var aboveOrder = current.OrderBy;
                    current.OrderBy = below.OrderBy;
                    below.OrderBy = aboveOrder;
                    _unitOfWork.GenericRepository<Branch>().Update(below);
                    _unitOfWork.GenericRepository<Branch>().Update(current);
                    _unitOfWork.Save();
                }

            }


        }
    }
}
