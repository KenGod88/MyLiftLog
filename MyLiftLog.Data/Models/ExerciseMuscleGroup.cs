using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLiftLog.Data.Models
{
    public class ExerciseMuscleGroup
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid ExerciseId { get; set; }
        public Exercise Exercise { get; set; }

        [Required]
        public Guid MuscleGroupId { get; set; }
        public MuscleGroup MuscleGroup { get; set; }
    }
}
