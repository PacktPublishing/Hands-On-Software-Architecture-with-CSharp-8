using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace dotNetStandardLibrary
{
    /// <summary>
    /// This declaration indicates that any class that implements 
    /// the interface IContentEvaluaded can be used for this
    /// service. Besides, the service will be responsible to 
    /// create the contented evaluated
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EvaluationService<T> where T: IContentEvaluated
    {
        /// <summary>
        /// The content is generic and totatly managed by the class
        /// </summary>
        public T content { get; set; }

        /// <summary>
        /// The creation of the content to be evaluated will use reflection and the
        /// generic definition from the class.
        /// Notice that this code will work because all the classes are in the 
        /// same assembly
        /// </summary>
        /// <param name="value"></param>
        public EvaluationService()
        {
            var name = GetTypeOfEvaluation();
            content = (T)Assembly.GetExecutingAssembly().CreateInstance(name);
        }

        /// <summary>
        /// This method is just used to get the type name of the generic one defined
        /// </summary>
        /// <returns></returns>
        public string GetTypeOfEvaluation()
        {
            var nome = typeof(T).FullName;
            return nome;
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
    }
}
