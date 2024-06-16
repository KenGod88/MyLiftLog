using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLiftLog.Data.Models
{
    public class Exercise
    {

        public Guid Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<ExerciseMuscleGroup> ExerciseMuscleGroups { get; set; }
        public ICollection<WorkoutExercise> WorkoutExercises { get; set; }

    }
}
