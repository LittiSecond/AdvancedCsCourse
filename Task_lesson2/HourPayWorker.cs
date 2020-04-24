using System;

namespace Task_lesson2
{
    /// <summary> работник с почасовой оплатой </summary>
    class HourPayWorker : AbstractWorker  
    {
        /// <summary> почасовая ставка </summary>
        private double _taxe;  
        
        public HourPayWorker(string name, string sur, double tax) : base(name, sur)
        {
            _taxe = tax;
        }
        
        ///<summary> считает и выдаёт среднемесячную зароботную плату </summary>
        public override double AverageMonthSelary()
        {
            return 20.8 * 8 * _taxe;
        }

    }
}
