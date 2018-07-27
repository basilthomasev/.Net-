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
    public class Mark : EntityBase
    {
        [Required]
        public double Score { get; set; }
        public int SubjectId { get; set; }
        [ForeignKey("SubjectId")]
        public virtual Subject subject { get; set; }
        public int StudentId { get; set; }
        [ForeignKey("StudentId")]
        public virtual Student student { get; set; }
    }
}
