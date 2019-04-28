using dotNetStandardLibrary.Evaluations;
using System;
using System.Collections.Generic;
using System.Text;

namespace dotNetStandardLibrary
{
    public interface IContentEvaluated
    {
        List<Evaluation> Evaluations { get; set; }
    }
}
