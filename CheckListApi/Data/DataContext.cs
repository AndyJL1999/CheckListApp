using CheckListApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CheckListApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Canvas> Canvases { get; set; }
        public DbSet<TaskBoard> TaskBoards { get; set; }
        public DbSet<Models.MyTask> Tasks { get; set; }


    }
}
