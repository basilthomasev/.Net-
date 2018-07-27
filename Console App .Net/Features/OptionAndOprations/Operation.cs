using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Features.Repository;
using Features.Models;
using System.Globalization;
using Features.Services;
using Features.DbContext;

namespace Features.OptionAndOprations
{
    public class Operations
    {
        private readonly StudentService _studentService;
        private readonly SchoolContext _database;

        public Operations() 
        {
            _database = new SchoolContext();
            _studentService = new StudentService();
        }
        public void Operation(int optionVaue)
        {
            switch (optionVaue)
            {
                case 1: Insert();
                    break;
                case 2: Delete();
                    break;
                case 3: Update();
                    break;
                case 4: View();
                    break;
                default: Console.WriteLine("Invalid Operation");
                    break;
            }
        }
        public void Insert()
        {
            var studentName = "";
            bool boolVal = true;
            while(boolVal){
            Console.WriteLine(Message.StudentName);
            studentName = Console.ReadLine();
            if (!string.IsNullOrEmpty(studentName))
            {
                boolVal = false;
            }
            }
            var ageOfStudent = 0;
            int genderOfStudent = 0;
            string genderStudent = null;
            boolVal = true;
            while (boolVal)
            {
                try
                {
                    bool boolv = true;
                    while(boolv){
                    Console.WriteLine(Message.StudentAge);
                    ageOfStudent = Convert.ToInt32(Console.ReadLine());
                    if (ageOfStudent > 0 && ageOfStudent < 120)
                    {
                        boolv = false;
                    }
                    boolVal = false;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Please enter a valid digit " + e.Message);
                }
            }
            boolVal = true;
            while (boolVal)
            {
                try
                {
                    bool boolv = true;
                    while (boolv)
                    {
                        Console.WriteLine("Please enter 1 for male and 0 for female");
                        genderOfStudent = Convert.ToInt16(Console.ReadLine());
                        if (genderOfStudent == 0 || genderOfStudent == 1)
                        {
                            boolv = false;
                        }
                    }
                    boolVal = false;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            if (genderOfStudent == 1)
            {
                genderStudent = "Male";
            }
            else if (genderOfStudent == 0)
            {
                genderStudent = "Female";
            }
            Console.WriteLine("Please enter the dateOfBirth of student");
            DateTime dateBirth = DateTime.Now;
            bool boolval = true;
            while (boolval)
            {
                try
                {
                    bool boolv = true;
                    while (boolv)
                    {
                        dateBirth = Convert.ToDateTime(Console.ReadLine());
                        DateTime dob;
                        DateTimeFormatInfo info = new DateTimeFormatInfo();
                        info.ShortDatePattern = "dd/MM/yyyy";
                        string s = Convert.ToString(dateBirth);
                        if (DateTime.TryParse(s, info, DateTimeStyles.None, out dob))
                        {
                            boolv = false;
                        }
                        else 
                        {
                            Console.WriteLine("Inalid Input \nSample format is 01/01/1994");
                            Console.WriteLine("\nPlease reenter the Date Of Birth");
                        }
                    }
                    boolval = false;
                }
                catch (Exception )
                {
                    Console.WriteLine("Inalid Input \nSample format is 01/01/1994");
                    Console.WriteLine("\nPlease reenter the Date Of Birth");
                }
            }

            // insert students subject
            Console.WriteLine("Do you want to inssert subject info? \nPress 1 to continue 0 to exit\n");
            int infoValue = Convert.ToInt32(Console.ReadLine());
            int value = 0;
            if (infoValue == 1)
            {
                value = 1;
            }
            else
            {
            }

            //var subjectScore = Convert.ToInt32(Console.ReadLine());
             _studentService.Insert(studentName, ageOfStudent, genderStudent, dateBirth, value);
        }
        public void Delete()
        {
            int value = 0;
            string studentName = null;
            bool boolValue = true;
            _studentService.ListAllStudents();
            while (boolValue)
            {
                Console.WriteLine(Message.DeleteName);
                studentName = Convert.ToString(Console.ReadLine());
                if (!string.IsNullOrEmpty(studentName))
                {
                    try
                    {
                        value = Get(studentName);
                        if (value>0)
                        {
                            _studentService.Delete(value);
                            boolValue = false;
                        }
                        else
                        {
                            Console.WriteLine("Invlid");
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(""+e);
                    }
                }
            }
            Console.WriteLine("Sucessfully deleted");
         }
        public int  Get(string studentName) 
        {
            var id = 0;
            bool boolvalue =true;
            var dataList = _database.Students.Where(i => i.Name == studentName).Select(i => new { i.Name, i.Id }).ToList();
            if (dataList != null && dataList.Count > 0)
            {
                Console.WriteLine("\nStudent Id      Student Name\n");
                for (int i = 0; i < dataList.Count; i++)
                {
                    Console.WriteLine(dataList[i].Id.ToString() + "                " + dataList[i].Name);
                }
                while(boolvalue){
                Console.WriteLine("Please enter Student Id to delete the student");
                id = Convert.ToInt16(Console.ReadLine());
                var idlist = dataList.Where(i => i.Id == id).SingleOrDefault();
                if (idlist == null)
                {
                    Console.WriteLine("Invalid Id");
                }else
                {
                    boolvalue = false;
                }
              }
            }
            else
            {
                Console.WriteLine("Invalid Name entered");
            }
            return id;
        }
        public void Update()
        {
            _studentService.ListAllStudents();
            Console.WriteLine(Message.EditName);
            int id = Convert.ToInt32(Console.ReadLine());
            _studentService.Update(id);
        }
        public void View()
        {
            _studentService.GetAll();
        }
    }
}
