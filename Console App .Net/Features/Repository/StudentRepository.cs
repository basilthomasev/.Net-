using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using Features.OptionAndOprations;
using Features.Entities;
using Features.DbContext;
using Features.Models;
using Features.Services;
using Features.DAL;

namespace Features.Repository
{

    public class StudentRepository : GenericRepository<Student>
    {
         private readonly SchoolContext _database;
       private readonly MarksRepository _markRepository;

       public StudentRepository(SchoolContext context)
            :base(context)
        {
            _markRepository = new MarksRepository(new SchoolContext());
            _database = context;
        }

      

        //repository to view all the students
        public IList<StudentDetailsModel> GetAll()
        {
            var student = (from a in _database.Students 
                           join b in _database.Marks on a.Id equals b.StudentId 
                           join c in _database.Subjects on b.SubjectId equals c.Id 
                           select new StudentDetailsModel { Id = a.Id, Name = a.Name, Age = a.Age, Subject = c.Name, Mark = b.Score }).ToList();
            return student;
        }

        //repository to view all the students
        public IList<StudentDetailsModel1> GetAll1()
        {
            var student = (from a in _database.Students 
                           select new StudentDetailsModel1 { Id = a.Id, Name = a.Name, Age = a.Age }).ToList();
            return student;
        }

        //repository to edit the student details
        public IList<StudentUpdate> GetStudentDetails(int studentId)
        {
            var subject = (from a in _database.Students join b in _database.Marks on a.Id equals b.StudentId join c in _database.Subjects on b.StudentId equals c.Id where a.Id == studentId select new StudentUpdate { Id = a.Id, Name = a.Name, Age = a.Age, Subject = c.Name, Mark = b.Score }).ToList();
            return subject;
        }
        public Student StudentDetails(int studentId) 
        {
            var studentData = _database.Students.FirstOrDefault(i => i.Id == studentId);
            return studentData;
        }

      
        public void SaveChanges()
        {
            _database.SaveChanges();
        }
        public IList<ListAllStudents> GetList() 
        {
            var student = _database.Students.Select(i=> new ListAllStudents { Name = i.Name , Id =i.Id}).ToList();
            return student;
        }
        public Student GetById(int studentId) 
        {
            var student = _database.Students.Where(i => i.Id == studentId).FirstOrDefault();
            return student;
        }
        public Student GetFirstValue(int markId)
        {
            var value = _database.Students.Where(i => i.Id == markId).FirstOrDefault();
            return value;
        }
    }
}
