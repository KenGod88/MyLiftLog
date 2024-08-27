using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLiftLog.UI.Models
{
    class ExerciseMuscleGroup
    {
        public Guid Id { get; set; }

        
        public Guid ExerciseId { get; set; }
        public Exercise Exercise { get; set; }

        
        public Guid MuscleGroupId { get; set; }
        public MuscleGroup MuscleGroup { get; set; }
    }
}
