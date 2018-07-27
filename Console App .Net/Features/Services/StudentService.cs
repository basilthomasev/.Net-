using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Features.OptionAndOprations;
using Features.Entities;
using Features.DbContext;
using Features.Models;
using Features.Repository;
using System.Data.Entity;

namespace Features.Services
{
    public class StudentService
    {
        private readonly SchoolContext _context;
        private readonly StudentRepository _studentRepository;
        private readonly SubjectRepository _subjectRepository;
        private readonly SubjectService _subjectService;
        readonly MarksRepository _markRepository;
        public StudentService()
        {
            _context = new SchoolContext();
            _subjectRepository = new SubjectRepository(_context);
            _studentRepository = new StudentRepository(_context);
            _markRepository = new MarksRepository(_context);
            _subjectService = new SubjectService(_subjectRepository);
        }
        public void Delete(int id)
        {
            var marks = _markRepository.GetAllBy(i => i.StudentId == id).ToList();
            //var mark = _markRepository.GetFirstValue(markObj.Id);
            foreach(var mark in marks){
            var result = _markRepository.Delete(mark);
            }
            var subjects = _subjectRepository.GetAllBy(i => i.StudentId == id).ToList();
            foreach(var subject in subjects)
            {
                //var subject = _subjectRepository.GetFirstValue(subjectObj.Id);
                _subjectRepository.Delete(subject);
            }
            var students = _studentRepository.GetAllBy(i => i.Id == id).ToList();
            //var student = _studentRepository.GetFirstValue(studentObj.Id);
            foreach (var student in students)
            {
                _studentRepository.Delete(student);
            }
        }
        public void Insert(string studentName, int ageOfStudent, string genderOfStudent, DateTime dateBirth, int id)
        {
            var studentDetail = new Student() { Name = studentName, Age = ageOfStudent, Gender = genderOfStudent, DateOfBirth = dateBirth };
            try
            {
                var student = _studentRepository.Create(studentDetail);
                Console.WriteLine(Message.saved);
                studentDetail.Id = student.Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            int studentId = studentDetail.Id;
            var studentData = _studentRepository.GetById(studentId); // _database.Students.Where(i => i.Id == studentDetail.Id).FirstOrDefault();
            Console.WriteLine("\nStudent Id       Student Name             Age               Gender\n");
            Console.WriteLine(studentData.Id + "\t\t  " + studentData.Name + "\t\t  " + studentData.Age + "\t\t  " + studentData.Gender + "\n");
            if (id == 1)
            {
                _subjectService.Insert(studentData.Id);
            }
        }

        //code to list all students in the update info
        public void ListAllStudents()
        {
            //var studentList = _database.Students;
            var studentList = _studentRepository.GetAll1();
            Console.WriteLine("\nAvailable student list");
            foreach (var d in studentList)
            {
                Console.WriteLine(d.Id + "\t" + d.Name);

            }
        }

        public void GetAll()
        {
            var students = _studentRepository.GetAll();

            //var StudentJoinSubject = (from a in _database.Students join 
            //                              b in _database.Marks on a.Id equals b.StudentId 
            //                                join c in _database.Subjects on b.SubjectId equals c.Id
            //                                   select new { Id = a.Id, Name = a.Name, Age = a.Age, Subject = c.Name, Mark = b.Score }).ToList();
            var groupByName = students.GroupBy(i => i.Id).Select(y => new
            {
                Key = y.Key,
                Value = y.ToList()
            }).ToList();
            foreach (var p in groupByName)
            {
                Console.WriteLine("\nStudent ID : {0}    Name : {1}    Age  : {2}\n", p.Key, p.Value[0].Name, p.Value[0].Age);
                p.Value.ForEach(i => Console.WriteLine("{0}  \t\t {1}\t ", i.Subject, i.Mark));
            }
        }

        //repository to edit the student details
        public void Update(int studentId)
        {
            var getStudent = _studentRepository.StudentDetails(studentId);
            var studentList = _studentRepository.GetStudentDetails(studentId);
            var groupByName = studentList.GroupBy(i => i.Id).Select(y => new
            {
                Key = y.Key,
                Value = y.ToList()
            }).ToList();
            foreach (var p in groupByName)
            {
                Console.WriteLine("\nStudent ID : {0}    Name : {1}    Age  : {2}\n", p.Key, p.Value[0].Name, p.Value[0].Age);
                p.Value.ForEach(i => Console.WriteLine("{0}  \t\t {1}\t ", i.Subject, i.Mark));
            }
            Console.WriteLine("Press 1 to change personal details and 2 to change subject details");
            var dataValue = Convert.ToInt32(Console.ReadLine());
            if (dataValue == 1)
            {
                try
                {
                    Console.WriteLine(Message.edit1);
                    Console.WriteLine(Message.changeNameAge);
                    Int32 option = Convert.ToInt32(Console.ReadLine());
                    if (option == 1)
                    {
                        Console.WriteLine(Message.name);
                        var newName = Console.ReadLine();
                        getStudent.Name = newName;
                    }
                    else
                        if (option == 2)
                        {
                            Console.WriteLine(Message.age);
                            var newAge = Convert.ToInt32(Console.ReadLine());
                            getStudent.Age = Convert.ToInt32(newAge);
                        }
                        else
                            if (option == 3)
                            {
                                Console.WriteLine(Message.name);
                                string newName = Console.ReadLine();
                                Console.WriteLine(Message.age);
                                Int32 newAge = Convert.ToInt32(Console.ReadLine());
                                getStudent.Name = newName;
                                getStudent.Age = newAge;
                            }
                            else
                            {
                                Console.WriteLine(Message.validNo);
                            }
                    _studentRepository.Update(getStudent);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            else
                if (dataValue == 2)
                {

                    
                    var StudentId = getStudent.Id;
                    Console.WriteLine("Please choose a subject to change the mark");
                    Console.WriteLine("English\nHindi\nMalayalam\n");
                    string subName = Console.ReadLine();
                    var includeStudetSubject = _markRepository.IncludeStudentSubject(StudentId, subName);
                    if (includeStudetSubject != null)
                    {
                        Console.WriteLine("Please enter new score for {0}", subName);
                        includeStudetSubject.Score = Convert.ToInt16(Console.ReadLine());
                        _markRepository.Update(includeStudetSubject);
                    }

                    // _subjectRepository.Update(StudentId);
                }
            _studentRepository.SaveChanges();
        }
    }

}
