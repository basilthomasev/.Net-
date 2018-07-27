using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using Features.DbContext;
using Features.Repository;
using Features.Entities;
using Features.Services;

namespace Features.Services
{
     public class SubjectService
    {

         private readonly SchoolContext _database;
      // private readonly StudentService _studentService;
        private readonly SubjectRepository _subjectRepository;
        private readonly MarkService _markService;
        private readonly MarksRepository _markRepository;

        public SubjectService(SubjectRepository subjectRepository)
        {
            _markRepository = new MarksRepository(new SchoolContext());
            _database = new SchoolContext();
          //  _studentService = new StudentService();
            _subjectRepository = subjectRepository;
            _markService = new MarkService();
        }

        //code to insert into subject repository
        public void Insert(int id)
        {
            int i = 0;
            double[] arrScore = new double[3];
            for (int j = 0; j < 3; j++)
            {
                if (j == 0)
                {
                    Console.WriteLine("Please enter marks for English");

                }
                else if (j == 1)
                {
                    Console.WriteLine("Please enter marks for malayalam");
                }
                else if (j == 2)
                {
                    Console.WriteLine("Please enter marks for Hindi");
                }
                bool boolValue = true;
                while (boolValue)
                {
                    try
                    {
                        arrScore[i] = Convert.ToDouble(Console.ReadLine());
                        boolValue = false;
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Wrong Input");
                        Console.WriteLine("Please reenter the value");
                    }
                }
                i++;

            }

            for (int j = 0; j < 3; j++)
            {
                ArrayList arrayList = new ArrayList() { "English  ", "Malayalam", "Hindi    " };
                var subject = new Subject() { Name = (string)arrayList[j], StudentId = id };
                _subjectRepository.Create(subject);
                int Id = subject.Id;
                _markService.Mark(Id, id, j, arrScore);
            }
        }

      //  public void Update(int Id) 
        //{
        //}
    }
}
