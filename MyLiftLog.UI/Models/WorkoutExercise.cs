using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLiftLog.UI.Models
{
    public class WorkoutExercise
    {
        public Guid Id { get; set; }

        
        public Guid WorkoutId { get; set; }

        public Workout Workout { get; set; }

        
        public Guid ExerciseId { get; set; }

        public Exercise Exercise { get; set; }

        public List<Set> Sets { get; set; }
    }
}
