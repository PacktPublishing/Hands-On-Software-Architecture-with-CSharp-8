using DesignPatternsSample.FactorySample.ConcreteProduct;
using DesignPatternsSample.FactorySample.CreatorInterface;
using DesignPatternsSample.FactorySample.ProductInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatternsSample.FactorySample.ConcreteCreator
{
    
    public class PaymentServiceCreator : IFactoryCreator
    {
        public enum ServicesAvailable
        {
            Italian = 0,
            Brazilian
        }


        public IFactoryProduct Factory(Enum operation)
        {
            IFactoryProduct result;
            if ((ServicesAvailable)operation == ServicesAvailable.Italian)
                result = new ItalianPaymentService();
            else
                result = new BrazilianPaymentService();
            return result;
        }
    }
}
