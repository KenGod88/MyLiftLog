using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyLiftLog.Data.Context;
using MyLiftLog.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyLiftLog.Data.Store
{
    public class WorkoutStore : IWorkoutStore
    {
        private readonly ApplicationDbContext _context;

        public WorkoutStore(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<ActionResult<Workout>> CreateWorkout(Workout workout)
        {
            _context.Workouts.Add(workout);
            await _context.SaveChangesAsync();

            return new ActionResult<Workout>(workout);
            
            
        }

        public async Task<ActionResult<Workout>> DeleteWorkout(Guid id)
        {
            var workout = await _context.Workouts.FindAsync(id);
            if (workout == null)
            {
                return new NotFoundResult();
            }

            _context.Workouts.Remove(workout);
            await _context.SaveChangesAsync();

            return new ActionResult<Workout>(workout);

            
        }

        public async Task<ActionResult<IEnumerable<Workout>>> GetAllWorkouts()
        {
            var workouts = await _context.Workouts
                .Include(w => w.WorkoutExercises)
                .ThenInclude(we => we.Sets)
                .ToListAsync();
            return new ActionResult<IEnumerable<Workout>>(workouts);
        }

        public async Task<ActionResult<Workout>> GetWorkout(Guid id)
        {
            var workout = await _context.Workouts.FindAsync(id);
            var workoutExercises = await _context.WorkoutExercises.Where(we => we.WorkoutId == id).ToListAsync();
            foreach (var workoutExercise in workoutExercises)
            {
                var exercise = await _context.Exercises.FindAsync(workoutExercise.ExerciseId);
                workoutExercise.Exercise = exercise;
                var sets = await _context.Sets.Where(s => s.WorkoutExerciseId == workoutExercise.Id).ToListAsync();
                workoutExercise.Sets = sets;
            }
            workout.WorkoutExercises = workoutExercises;

            if (workout == null)
            {
                return new NotFoundResult();
            }

            return workout;
        }

        public async Task<ActionResult<Workout>> UpdateWorkout(Guid id, Workout workout)
        {
            if (id != workout.Id)
            {
                return new BadRequestResult();
            }

            _context.Entry(workout).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkoutExists(id))
                {
                    return new NotFoundResult();
                }
                else
                {
                    throw;
                }
            }

            return new ActionResult<Workout>(workout);
        }


        public bool WorkoutExists(Guid id)
        {
            return _context.Workouts.Any(e => e.Id == id);
        }
    }
}
