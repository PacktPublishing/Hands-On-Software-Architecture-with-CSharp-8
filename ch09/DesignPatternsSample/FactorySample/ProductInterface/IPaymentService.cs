using DesignPatternsSample.FactorySample.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatternsSample.FactorySample.ProductInterface
{
    public interface IPaymentService : IFactoryProduct
    {
        /// <summary>
        /// E-mail of the person who will be charged
        /// </summary>
        string EmailToCharge { get; set; }
        /// <summary>
        /// Money that will be charged
        /// </summary>
        float MoneyToCharge { get; set; }
        
        /// <summary>
        /// Credit Card or Debit Card
        /// </summary>
        EnumChargingOptions OptionToCharge { get; set; }

        /// <summary>
        /// Method responsible for process the charging asked
        /// </summary>
        /// <returns></returns>
        bool ProcessCharging();

    }
}
