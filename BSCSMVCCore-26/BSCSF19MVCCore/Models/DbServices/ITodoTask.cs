using Microsoft.EntityFrameworkCore;

namespace BSCSMVCCore.Models.DbServices
{
    public interface ITodoTask
    {
        Task<List<ToDoTask>> GetTasks();
        ToDoTask GetTask(int id);
        Task Insert(ToDoTask task);
        Task Update(ToDoTask task); 
        Task Delete(int id);
    }
    public class TodoTaskService : ITodoTask
    {
        private readonly AppDbContext _db;
        public TodoTaskService(AppDbContext db)
        {
            _db = db;
        }
        public async Task Delete(int id)
        {
            var temp = _db.Tasks.Find(id);
            if (temp != null)
            {
                _db.Tasks.Remove(temp);
                await _db.SaveChangesAsync();
            }
            
        }

        public ToDoTask GetTask(int id)
        {
            var temp = _db.Tasks.Where(t => t.Id == id).FirstOrDefault();
            return temp;
        }

        public async Task<List<ToDoTask>> GetTasks()
        {
            return await _db.Tasks.ToListAsync();
        }

        public async Task Insert(ToDoTask task)
        {
             await _db.Tasks.AddAsync(task);
             await _db.SaveChangesAsync();
        }

        async Task ITodoTask.Update(ToDoTask task)
        {
            var temp = await _db.Tasks.FindAsync(task.Id);
            if (temp != null)
            {
                temp.Title = task.Title;
                temp.Description = task.Description;
                temp.CreatedOn = task.CreatedOn;

            }
            _db.Update(task);
           await _db.SaveChangesAsync();
        }
    }
}
