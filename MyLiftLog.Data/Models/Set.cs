using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLiftLog.Data.Models
{
    public class Set
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid WorkoutExerciseId { get; set; }  

        public WorkoutExercise WorkoutExercise { get; set; }

        public int Reps { get; set; }
        public double Weight { get; set; }
    }
}
