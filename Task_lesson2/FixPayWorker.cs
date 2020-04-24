using System;

namespace Task_lesson2
{
    /// <summary> работник с фиксированной оплатой </summary>
    class FixPayWorker : AbstractWorker  
    {
        /// <summary> оклад</summary>
        private double _monthPay;     

        public FixPayWorker(string name, string sur, double tax) : base(name, sur)
        {
            _monthPay = tax;
        }
        
        /// <summary> считает и выдаёт среднемесячную зароботную плату</summary>
        public override double AverageMonthSelary()
        {
            return _monthPay;
        }

    }
}
