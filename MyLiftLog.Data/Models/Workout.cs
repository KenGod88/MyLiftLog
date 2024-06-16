using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLiftLog.Data.Models
{
    public class Workout
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public DateTime Date { get; set; }
        public string Notes { get; set; }
        public ICollection<WorkoutExercise> WorkoutExercises { get; set; }
    }
}
