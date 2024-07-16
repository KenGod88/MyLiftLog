using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLiftLog.UI.Models
{
    public class Workout
    {
       
        public Guid Id { get; set; }

        [Required]
        public DateTime Date { get; set; }
        public string Notes { get; set; }
        
    }
}
