using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLiftLog.Data.Models
{
    public class WorkoutExercise
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid WorkoutId { get; set; }
        public Workout Workout { get; set; }

        [Required]
        public Guid ExerciseId { get; set; }
        public Exercise Exercise { get; set; }

        public ICollection<Set> Sets { get; set; }
    }
}
