using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Features.Repository;
using Features.OptionAndOprations;
using Features.Entities;
using System.Collections;
using Features.DbContext;
using Features.Models;
using Features.Services;
using Features.DAL;

namespace Features.Repository
{
    public class SubjectRepository : GenericRepository<Subject>
    {
       private readonly SchoolContext _database;

       public SubjectRepository(SchoolContext context)
            :base(context)
        {
            _database = context;
        }
       //code to edit into student repository

       public void Update(int Id)
       {
           _database.SaveChanges();
       }
       public Subject GetFirstValue(int markId)
       {
           var value = _database.Subjects.Where(i => i.Id == markId).FirstOrDefault();
           return value;
       }
    }
}
