using dotNetStandardLibrary;
using dotNetStandardLibrary.Evaluations;
using System.Collections.Generic;

namespace dotNetStandardLibrary.Evaluations.Content
{
    public class DestinationExpert : IContentEvaluated
    {
        public List<Evaluation> Evaluations { get; set; }
    }
}
