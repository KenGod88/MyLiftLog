using Microsoft.AspNetCore.Mvc;
using MyLiftLog.Data.Models;
using MyLiftLog.Data.Store;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyLiftLog.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkoutController : ControllerBase
    {
        private readonly IWorkoutStore _workoutStore;

        public WorkoutController(IWorkoutStore workoutStore)
        {
            _workoutStore = workoutStore;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Workout>>> GetAllWorkouts()
        {
            return await _workoutStore.GetAllWorkouts();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Workout>> GetWorkout(Guid id)
        {
            return await _workoutStore.GetWorkout(id);
        }

        [HttpGet("exists/{id}")]
        public ActionResult<bool> WorkoutExists(Guid id)
        {
            return _workoutStore.WorkoutExists(id);
        }

        [HttpPost]
        public async Task<ActionResult<Workout>> CreateWorkout(WorkoutInputModel input)
        {
            try
            {
                // Create new Workout
                var newWorkout = new Workout
                {
                    Date = input.Date,
                    Notes = input.Notes,
                    WorkoutExercises = new List<WorkoutExercise>()
                };

                // Loop through each input workout exercise
                foreach (var inputExercise in input.WorkoutExercises)
                {
                    // Create new Exercise
                    var exercise = new Exercise
                    {
                        Name = inputExercise.Exercise.Name,
                        Description = inputExercise.Exercise.Description,
                        ExerciseMuscleGroups = new List<ExerciseMuscleGroup>()
                    };

                    // Loop through each muscle group in the exercise
                    foreach (var muscleGroup in inputExercise.Exercise.ExerciseMuscleGroups)
                    {
                        var muscleGroupEntity = new MuscleGroup
                        {
                            Name = muscleGroup.MuscleGroup.Name
                        };

                        var exerciseMuscleGroup = new ExerciseMuscleGroup
                        {
                            MuscleGroup = muscleGroupEntity
                        };

                        // Add muscle group to exercise
                        exercise.ExerciseMuscleGroups.Add(exerciseMuscleGroup);
                    }

                    // Create new WorkoutExercise
                    var workoutExercise = new WorkoutExercise
                    {
                        Exercise = exercise,
                        Sets = new List<Set>()
                    };

                    // Loop through each set in the exercise
                    foreach (var set in inputExercise.Sets)
                    {
                        var newSet = new Set
                        {
                            Reps = set.Reps,
                            Weight = set.Weight
                        };

                        // Add set to workout exercise
                        workoutExercise.Sets.Add(newSet);
                    }

                    // Add workout exercise to workout
                    newWorkout.WorkoutExercises.Add(workoutExercise);
                }

                // Save to database via _workoutStore
                var result = await _workoutStore.CreateWorkout(newWorkout);

                // Return result
                return result;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Workout>> UpdateWorkout(Guid id, Workout workout)
        {
            return await _workoutStore.UpdateWorkout(id, workout);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkout(Guid id)
        {
            var result = await _workoutStore.DeleteWorkout(id);

            if (result.Result is NotFoundResult)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
    public class WorkoutInputModel
    {
        public DateTime Date { get; set; }
        public string Notes { get; set; }
        public List<WorkoutExerciseInputModel> WorkoutExercises { get; set; }
    }

    public class WorkoutExerciseInputModel
    {
        public ExerciseInputModel Exercise { get; set; }
        public List<SetInputModel> Sets { get; set; }
    }

    public class ExerciseInputModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<ExerciseMuscleGroupInputModel> ExerciseMuscleGroups { get; set; }
    }

    public class ExerciseMuscleGroupInputModel
    {
        public MuscleGroupInputModel MuscleGroup { get; set; }
    }

    public class MuscleGroupInputModel
    {
        public string Name { get; set; }
    }

    public class SetInputModel
    {
        public int Reps { get; set; }
        public double Weight { get; set; }
    }
}

