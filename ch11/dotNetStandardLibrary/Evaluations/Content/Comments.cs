using dotNetStandardLibrary;
using dotNetStandardLibrary.Evaluations;
using System.Collections.Generic;

namespace dotNetStandardLibrary.Evaluations.Content
{
    public class Comments : IContentEvaluated
    {
        public List<Evaluation> Evaluations { get; set; }
    }
}
