using System;

namespace Task_lesson2
{
    abstract class AbstractWorker : IComparable
    {
        private string _name;
        private string _surname;

        public AbstractWorker(string name, string surname )
        {
            _name = name;
            _surname = surname;
        }

        public string Name
        {
            get { return _name; }
        }

        public string Surname
        {
            get { return _surname; }
        }        

        // считает и выдаёт среднемесячную зароботную плату
        abstract public double AverageMonthSelary();

        //                  интерфейс для IComparable
        public int CompareTo(object obj)
        {
            // создаётся две строки "Имя фамилия" этого и другого работника
            string this_nameSurname = _name + " " + _surname;
            AbstractWorker bw2 = obj as AbstractWorker;
            string obj_nameSurname = bw2._name + " " + bw2._surname;
            
            // потом эти строки сравниваются
            return string.Compare(_name, (obj as AbstractWorker)._name);
        }


    }
}
