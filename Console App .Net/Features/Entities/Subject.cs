using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Features.Common;

namespace Features.Entities
{
    public class Subject : EntityBase
    {
        [Required]
        public string Name { get; set; }
        public int StudentId { get; set; }
        [ForeignKey("StudentId")]
        public virtual Student student { get; set; }
    }
}
