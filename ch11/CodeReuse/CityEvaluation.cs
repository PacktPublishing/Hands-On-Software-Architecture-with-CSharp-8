using dotNetStandardLibrary;
using dotNetStandardLibrary.Evaluations;
using System.Collections.Generic;

namespace CodeReuse
{
    class CityEvaluation : IContentEvaluated
    {
        public List<Evaluation> Evaluations { get; set; } = new List<Evaluation>();
    }
}
