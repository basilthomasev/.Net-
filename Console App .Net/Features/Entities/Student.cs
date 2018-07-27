using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Features.Entities
{
    public class Student : Features.Common.EntityBase
    {
       
        [Required]
        public string Name { get; set; }
        [Required]
        public int Age { get; set; }
        public string Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}
