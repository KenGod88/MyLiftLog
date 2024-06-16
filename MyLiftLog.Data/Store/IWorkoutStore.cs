using Microsoft.AspNetCore.Mvc;
using MyLiftLog.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLiftLog.Data.Store
{
    public interface IWorkoutStore
    {
        Task<ActionResult<IEnumerable<Workout>>> GetAllWorkouts();
        Task<ActionResult<Workout>> GetWorkout(Guid id);
        Task<ActionResult<Workout>> CreateWorkout(Workout workout);
        Task<ActionResult<Workout>> UpdateWorkout(Guid id, Workout workout);
        Task<ActionResult<Workout>> DeleteWorkout(Guid id);
        bool WorkoutExists(Guid id);
    }
}
