using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PoppertechCalculator.Models.PSO
{
    public class PsoContext:GoalAttainmentContext
    {
        [Required]
        [OptimizationParamsValidation]
        public OptimizationContext OptimizationParams { get; set; }
    }
}