using dotNetStandardLibrary;
using dotNetStandardLibrary.Evaluations;
using System.Collections.Generic;

namespace CodeReuse
{
    class DestinationExpert : IContentEvaluated
    {
        public List<Evaluation> Evaluations { get; set; }
    }
}
