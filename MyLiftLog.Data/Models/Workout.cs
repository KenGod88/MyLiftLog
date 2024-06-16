using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLiftLog.Data.Models
{
    public class Workout
    {

        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Notes { get; set; }
        public ICollection<WorkoutExercise> WorkoutExercises { get; set; }
    }
}
