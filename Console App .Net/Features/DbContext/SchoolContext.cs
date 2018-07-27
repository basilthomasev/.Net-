using Features.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Features.DbContext
{
   public class SchoolContext : System.Data.Entity.DbContext
    {
       public SchoolContext()
            : base("School")
        {

        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Mark> Marks { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Subject>()
         .HasRequired(c => c.student)
         .WithMany()
         .WillCascadeOnDelete(false);
        }
    }
}
