using HRM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Repositories.Interfaces
{
    public interface ILOVsService
    {
        
        List<LOVs> Get(LOV_Type type);
        IQueryable<LOVs> Get();
        List<LOVs> GetAll();
        void Insert(List<LOVs> Lovs);

        void Insert(LOVs Lovs);
        void InsertWithOrder(LOVs Lovs);
        List<LOVs> GetDegreeLevels();
        List<LOVs> GetDegrees(int levelId);
        void InsertDegrees(List<DegreeTitle> Degrees);

        void InsertDegreeLevels(List<DegreeLevel> DegreeLevels);

        void InsertDesignations(List<Designation> designations);
        //void InsertDomiciles(List<Domicile> domiciles);
        List<LOVs> GetDesignations();
        List<Designation> GetDesignationsData();
        
        string GetText(int id, LOV_Type lOV_Type);
        Designation GetDesignationById(int id);
        Designation UpdateDesignation(Designation designation);


    }
    public class LOVsService : ILOVsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public LOVsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

    

        public List<LOVs> Get(LOV_Type type)
        {
            return _unitOfWork.GenericRepository<LOVs>().Get().Where(t => t.LOV_Type == type).ToList();
        }

        public IQueryable<LOVs> Get()
        {
            return _unitOfWork.GenericRepository<LOVs>().Get();
        }

        public List<LOVs> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<LOVs> GetDegreeLevels()
        {
            return _unitOfWork.GenericRepository<DegreeLevel>().Get().OrderBy(t=>t.Id).Select(t => new LOVs() {
            Value=t.Id.ToString(),
            Text=t.Level
            }).ToList();
        }

        public List<LOVs> GetDegrees(int levelId)
        {
            return _unitOfWork.GenericRepository<DegreeTitle>().Get().Where(t=>t.DegreeLevelId==levelId).OrderBy(t=>t.Id).Select(t => new LOVs()
            {
                Value = t.Id.ToString(),
                Text = t.Title
            }).ToList();
        }

        public Designation GetDesignationById(int id)
        {
            return _unitOfWork.GenericRepository<Designation>().GetById(id);
        }

        public List<LOVs> GetDesignations()
        {
            return _unitOfWork.GenericRepository<Designation>().Get().OrderBy(t => t.Id).Select(t => new LOVs()
            {
                Value = t.Id.ToString(),
                Text = t.DesignationTitle + " "+ t.BPSNAME
            }).ToList();
        }

        public List<Designation> GetDesignationsData()
        {
            return _unitOfWork.GenericRepository<Designation>().Get().ToList();//.Where(t => t.LOV_Type == type).ToList();
        }

        public string GetText(int id, LOV_Type lOV_Type)
        {
            return _unitOfWork.GenericRepository<LOVs>().Get().Where(tt=>tt.Value==id.ToString() && tt.LOV_Type == lOV_Type).Select(t=>t.Text).FirstOrDefault();
        }

        public void Insert(List<LOVs> Lovs)
        {
            _unitOfWork.GenericRepository<LOVs>().AddRange(Lovs);
            while (_unitOfWork.IsBusy()) {
                Thread.Sleep(100);
            }
            _unitOfWork.Save();
        }
        
        public void Insert(LOVs Lovs)
        {
            _unitOfWork.GenericRepository<LOVs>().Add(Lovs);
            while (_unitOfWork.IsBusy())
            {
                Thread.Sleep(100);
            }
            _unitOfWork.Save();
        }

        public void InsertDegreeLevels(List<DegreeLevel> DegreeLevels)
        {
            _unitOfWork.GenericRepository<DegreeLevel>().AddRange(DegreeLevels);
            while (_unitOfWork.IsBusy())
            {
                Thread.Sleep(100);
            }
            _unitOfWork.Save();

        }

        public void InsertDegrees(List<DegreeTitle> Degrees)
        {
            _unitOfWork.GenericRepository<DegreeTitle>().AddRange(Degrees);
            while (_unitOfWork.IsBusy())
            {
                Thread.Sleep(100);
            }
            _unitOfWork.Save();
        }

        public void InsertDesignations(List<Designation> designations)
        {
            _unitOfWork.GenericRepository<Designation>().AddRange(designations);
            while (_unitOfWork.IsBusy())
            {
                Thread.Sleep(100);
            }
            _unitOfWork.Save();
        }
        public void InsertWithOrder(LOVs Lovs)
        {
            throw new NotImplementedException();
        }

        public Designation UpdateDesignation(Designation designation)
        {
            _unitOfWork.GenericRepository<Designation>().Update(designation);
            _unitOfWork.Save();
            return designation;
        }


        //public void InsertDomiciles(List<Domicile> domiciles)
        //{
        //    _unitOfWork.GenericRepository<Domicile>().AddRange(domiciles);
        //    _unitOfWork.Save();
        //}
    }
}
