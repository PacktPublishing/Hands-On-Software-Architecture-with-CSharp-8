using dotNetStandardLibrary;
using dotNetStandardLibrary.Evaluations;
using System.Collections.Generic;

namespace CodeReuse
{
    class Package : IContentEvaluated
    {
        public List<Evaluation> Evaluations { get ; set ; }
    }
}
