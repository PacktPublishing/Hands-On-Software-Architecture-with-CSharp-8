using System;
using System.Collections.Generic;
using System.Text;

namespace dotNetStandardLibrary.Evaluations.Types
{
    public class BasicUsersEvaluation: Evaluation
    { 
        public override double CalculateGrade()
        {
            return Grade;
        }
    }
}
