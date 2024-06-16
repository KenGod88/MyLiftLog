using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLiftLog.Data.Models
{
    public class WorkoutExercise
    {
        public Guid Id { get; set; }

        public Guid WorkoutId { get; set; }
        public Workout Workout { get; set; }

        public Guid ExerciseId { get; set; }
        public Exercise Exercise { get; set; }

        public ICollection<Set> Sets { get; set; }
    }
}
