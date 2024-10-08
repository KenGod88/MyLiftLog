﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLiftLog.Data.Models
{
    public class Exercise
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public string Description { get; set; }

       
        public MuscleGroup MuscleGroup { get; set; }


        public List<WorkoutExercise> WorkoutExercises { get; set; }

    }
}
