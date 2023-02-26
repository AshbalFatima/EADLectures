using HRM.Models;
using HRM.Models.ViewModels;
using HRM.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Utilities
{
    public class DDHelper
    {
        private readonly ILOVsService _LOVsService;

        public DDHelper()
        { 
        
        }
        public DDHelper(ILOVsService lOVsService)
        {
            _LOVsService = lOVsService;
        }

        public List<SelectListItem> Genders(string selected)
        {
            var temp = _LOVsService.Get(Models.LOV_Type.Genders);
            var selecteItemList = new List<SelectListItem>();
            for (int i = 0; i < temp.Count; i++)
            {
                selecteItemList.Add(new SelectListItem
                {
                    Selected = selected == temp[i].Value,
                    Value = temp[i].Value,
                    Text = temp[i].Text

                });
            }
            return selecteItemList;
        }

        public List<SelectListItem> Domiciles(string selected)
        {
            var temp = _LOVsService.Get(Models.LOV_Type.Domiciles);
            var selecteItemList = new List<SelectListItem>();
            for (int i = 0; i < temp.Count; i++)
            {
                selecteItemList.Add(new SelectListItem
                {
                    Selected = selected == temp[i].Value,
                    Value = temp[i].Value,
                    Text = temp[i].Text

                });
            }
            return selecteItemList;
        }

        public List<SelectListItem> GetBranches(ICollection<Branch> branches,int selected=-1)
        {

            var list = new List<SelectListItem>();
            foreach (var item in branches.Where(t => t.ParentBranchId.HasValue == false).OrderBy(t=>t.OrderBy))
            {
                var alreadyExsit = list.Where(t => t.Value == item.Id.ToString()).FirstOrDefault();
                if (alreadyExsit == null)
                {
                    if(item.Id==selected)
                        list.Add(new SelectListItem(item.BranchName, item.Id.ToString() , true));
                    else
                        list.Add(new SelectListItem(item.BranchName, item.Id.ToString()));

                }
                
                
                AddMyChild(ref list, branches , item.Id,1,selected);
            }




            return list;
        }
        public List<SelectListItem> AddMyChild(ref List<SelectListItem> items, ICollection<Branch> branches, int parentId, int level = 1,int selected = -1)
        {
            //var items = branches.Where(t=>t.ParentBranchId== parentId).Select(t=>)
            
            var _list = new List<SelectListItem>();
            foreach (var item in branches.Where(t => t.ParentBranchId == parentId).OrderBy(t => t.OrderBy))
            {
                var alreadyExsit = items.Where(t => t.Value == item.Id.ToString()).FirstOrDefault();
                if (alreadyExsit == null)
                {
                     if (item.Id == selected)
                        items.Add(new SelectListItem(_Chars(level) + " " + item.BranchName, item.Id.ToString(),true));
                    else
                    items.Add(new SelectListItem(_Chars(level) + " " + item.BranchName, item.Id.ToString()));
                }
                
                
                AddMyChild( ref items, branches , item.Id,level+1 , selected);
            }




            return items;
        }
        public List<BranchVM> GetBranchesOrder(ICollection<Branch> branches)
        {

            var list = new List<BranchVM>();
            foreach (var item in branches.Where(t => t.ParentBranchId.HasValue == false).OrderBy(t => t.OrderBy))
            {
                var alreadyExsit = list.Where(t => t.Id == item.Id).FirstOrDefault();
                if (alreadyExsit == null)
                {
                    var t = new BranchVM(item);
                    t.ParentClasses = "root";
                    list.Add(t);
                }

                AddMyChildOrder(ref list, branches, item.Id);
            }




            return list;
        }
        public void AddMyChildOrder(ref List<BranchVM> items, ICollection<Branch> branches, int parentId, int level = 1)
        {
            //var items = branches.Where(t=>t.ParentBranchId== parentId).Select(t=>)

            var _list = new List<BranchVM>();
            foreach (var item in branches.Where(t => t.ParentBranchId == parentId).OrderBy(t => t.OrderBy))
            {
                var alreadyExsit = items.Where(t => t.Id.Value == item.Id).FirstOrDefault();
                if (alreadyExsit == null)
                {
                    item.BranchName = _Chars(level) + item.BranchName;
                    var t = new BranchVM(item);
                    t.ParentClasses += "has-child parent-" + (level-1);
                    t.ChildClasses = "child-" +level ;
                    items.Add(t);
                }

                AddMyChildOrder(ref items, branches, item.Id, level+1);
            }




            
        }
        public string _Chars(int n) {
            string s = "";
            for (int i = 0; i < n; i++)
            {
                s += "*";
            }
            return s;
        }

        public List<SelectListItem> Religions(string selected)
        {
            var temp = _LOVsService.Get(Models.LOV_Type.Religions);
            var selecteItemList = new List<SelectListItem>();
            for (int i = 0; i < temp.Count; i++)
            {
                selecteItemList.Add(new SelectListItem
                {
                    Selected = selected == temp[i].Value,
                    Value = temp[i].Value,
                    Text = temp[i].Text

                });
            }
            return selecteItemList;
        }
        public List<SelectListItem> PayScales(string selected)
        {
            var temp = _LOVsService.Get(Models.LOV_Type.BPS);
            var selecteItemList = new List<SelectListItem>();
            for (int i = 0; i < temp.Count; i++)
            {
                selecteItemList.Add(new SelectListItem
                {
                    Selected = selected == temp[i].Value,
                    Value = temp[i].Value,
                    Text = temp[i].Text

                });
            }
            return selecteItemList;
        }
        public List<SelectListItem> Designations(string selected)
        {
            var temp = _LOVsService.GetDesignations();
            var selecteItemList = new List<SelectListItem>();
            for (int i = 0; i < temp.Count; i++)
            {
                selecteItemList.Add(new SelectListItem
                {
                    Selected = selected == temp[i].Value,
                    Value = temp[i].Value,
                    Text = temp[i].Text

                });
            }
            return selecteItemList;
        }
        public List<SelectListItem> Benches(string selected)
        {
            var temp = _LOVsService.Get(Models.LOV_Type.Benches);
            var selecteItemList = new List<SelectListItem>();
            for (int i = 0; i < temp.Count; i++)
            {
                selecteItemList.Add(new SelectListItem
                {
                    Selected = selected == temp[i].Value,
                    Value = temp[i].Value,
                    Text = temp[i].Text

                });
            }
            return selecteItemList;
        }
        public List<SelectListItem> Branches(string selected)
        {
            var temp = _LOVsService.Get(Models.LOV_Type.Branches);
            var selecteItemList = new List<SelectListItem>();
            for (int i = 0; i < temp.Count; i++)
            {
                selecteItemList.Add(new SelectListItem
                {
                    Selected = selected == temp[i].Value,
                    Value = temp[i].Value,
                    Text = temp[i].Text

                });
            }
            return selecteItemList;
        }
        public List<SelectListItem> Nationalities(string selected)
        {
            var temp = _LOVsService.Get(Models.LOV_Type.Nationalities);
            var selecteItemList = new List<SelectListItem>();
            for (int i = 0; i < temp.Count; i++)
            {
                selecteItemList.Add(new SelectListItem
                {
                    Selected = selected == temp[i].Value,
                    Value = temp[i].Value,
                    Text = temp[i].Text

                });
            }
            return selecteItemList;
        }
        public List<SelectListItem> DegreeLevels(string selected)
        {
            var temp = _LOVsService.GetDegreeLevels().ToList();
            var selecteItemList = new List<SelectListItem>();
            for (int i = 0; i < temp.Count; i++)
            {
                selecteItemList.Add(new SelectListItem
                {
                    Selected = selected == temp[i].Value,
                    Value = temp[i].Value,
                    Text = temp[i].Text

                });
            }
            return selecteItemList;
        }
        
        public List<SelectListItem> Degrees(string selected,int levelId)
        {
            var temp = _LOVsService.GetDegrees(levelId).ToList();
            var selecteItemList = new List<SelectListItem>();
            for (int i = 0; i < temp.Count; i++)
            {
                selecteItemList.Add(new SelectListItem
                {
                    Selected = selected == temp[i].Value,
                    Value = temp[i].Value,
                    Text = temp[i].Text

                });
            }
            return selecteItemList;
        }
        public List<SelectListItem> BloodGroups(string selected)
        {
            var temp = _LOVsService.Get(Models.LOV_Type.BloodGroups);
            var selecteItemList = new List<SelectListItem>();
            for (int i = 0; i < temp.Count; i++)
            {
                selecteItemList.Add(new SelectListItem
                {
                    Selected = selected == temp[i].Value,
                    Value = temp[i].Value,
                    Text = temp[i].Text

                });
            }
            return selecteItemList;
        }
        public List<SelectListItem> MaritalStatuses(string selected)
        {
            var temp = _LOVsService.Get(Models.LOV_Type.MartialStatuses);
            var selecteItemList = new List<SelectListItem>();
            for (int i = 0; i < temp.Count; i++)
            {
                selecteItemList.Add(new SelectListItem
                {
                    Selected = selected == temp[i].Value,
                    Value = temp[i].Value,
                    Text = temp[i].Text

                });
            }
            return selecteItemList;
        }

        public List<SelectListItem> Get(string selected, Models.LOV_Type type)
        {
            var temp = _LOVsService.Get(type);
            var selecteItemList = new List<SelectListItem>();
            for (int i = 0; i < temp.Count; i++)
            {
                selecteItemList.Add(new SelectListItem
                {
                    Selected = selected == temp[i].Value,
                    Value = temp[i].Value,
                    Text = temp[i].Text

                });
            }
            return selecteItemList;
        }
        public List<SelectListItem> GetYears(int selected)
        {
            
            var selecteItemList = new List<SelectListItem>();
            for (int i = DateTime.Now.Year; i >1947; i--)
            {
                selecteItemList.Add(new SelectListItem
                {
                    Selected = selected == i,
                    Value = i.ToString(),
                    Text = i.ToString()

                });
            }
            return selecteItemList;
        }
        

    }

}
