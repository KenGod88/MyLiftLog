using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLiftLog.Data.Models
{
    public class Set
    {
        public Guid Id { get; set; }

        public Guid WorkoutExersiceId { get; set; }
        public WorkoutExercise WorkoutExercise { get; set; }

        public int Reps { get; set; }
        public double Weight { get; set; }
    }
}
