using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_lesson5.Entities
{
    /// <summary> Должность </summary>
    class Position
    {
        /// <summary> Название </summary>
        public string Title { get; private set; }
        public int PositionCode { get; private set; }

        public Position(string title, int positionCode)
        {
            Title = title;
            PositionCode = positionCode;
        }

    }
}
