using System;
using System.Collections.Generic;
using System.Text;

namespace dotNetStandardLibrary
{
    public class EvaluationService<T> where T: IContentEvaluated
    {
        public T content { get; set; }

        public EvaluationService(T value)
        {
            content = value;
        }

        /// <summary>
        /// No matter the Evaluation, the calculation will always get values from the method CalculateGrade
        /// </summary>
        /// <returns>The average of the grade from Evaluations</returns>
        public double CalculateEvaluationAverage()
        {
            var count = 0;
            double evaluationGrade = 0;
            foreach (var evaluation in content.Evaluations)
            {
                evaluationGrade += evaluation.CalculateGrade();
                count++;
            }
            return evaluationGrade/count;
        }

        public string GetTypeOfEvaluation()
        {
            var nome = typeof(T).FullName;
            return nome;
        }
    }
}
