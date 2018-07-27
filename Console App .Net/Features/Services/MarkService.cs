using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Features.DbContext;
using Features.Repository;
using Features.Entities;

namespace Features.Services
{
    public class MarkService
    {
         private readonly SchoolContext _database;
        private readonly MarksRepository _markRepository;

        public MarkService()
        {
            _database = new SchoolContext();
            _markRepository = new MarksRepository(new SchoolContext());
        }
        public void Mark(int Id, int id, int j, double[] arrScore)
        {
            var subjectdata = _database.Subjects.FirstOrDefault(m => m.Id == Id);
            var score = new Mark() { Score = arrScore[j], SubjectId = subjectdata.Id, StudentId = id };

            _markRepository.InsertMark(score);
        }


    }
}
