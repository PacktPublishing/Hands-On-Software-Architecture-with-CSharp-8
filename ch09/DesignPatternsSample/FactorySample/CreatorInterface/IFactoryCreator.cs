using DesignPatternsSample.FactorySample.ProductInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatternsSample.FactorySample.CreatorInterface
{
    public interface IFactoryCreator
    {
        IFactoryProduct Factory(Enum operation);
    }
}
