using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Features.DbContext;
using Features.Entities;
using Features.Repository;
using Features.Services;
using Features.DAL;



namespace Features.Repository
{
    public class MarksRepository : GenericRepository<Mark>
    {
        private readonly SchoolContext _database;
        public MarksRepository(SchoolContext context)
            : base(context)
        {
            _database = context;
        }
        public void InsertMark(Mark score)
        {
            _database.Marks.Add(score);
            _database.SaveChanges();
        }
        public Mark IncludeStudentSubject(int Id , string subName) 
        {
            var studentData = _database.Marks.Include("subject").Include("student").Where(i => i.student.Id == Id && i.subject.Name == subName).FirstOrDefault();
            return studentData;
        }
        public Mark GetFirstValue(int markId) 
        {
            var value = _database.Marks.Where(i => i.Id == markId).FirstOrDefault();
            return value;
        }
    }
}
