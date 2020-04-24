using System;
using System.Collections.Generic;
using System.Collections;

namespace Task_lesson2
{
    class Departament : IEnumerable, IEnumerator
    {
        private List<AbstractWorker> _workers;
        private int _index = -1;

        public Departament()
        {
            _workers = new List<AbstractWorker>();
        }

        public void Add(AbstractWorker bw)  // добавление работника
        {

            if (bw != null)
            {
                _workers.Add(bw);
            }
        }

        public AbstractWorker this[int i]
        {
            get { return _workers[i]; }
        }

        #region методы интерфейсов
        //                     интерфейс для IEnumerable
        public IEnumerator GetEnumerator()
        {
            return this;
        }

        //                интерфейсы для IEnumerator
        public object Current
        {
            get
            {
                return _workers[_index];
            }
        }

        public bool MoveNext()
        {
            if (_index == _workers.Count - 1)
            {
                Reset();
                return false;
            }
            _index++;
            return true;
        }

        public void Reset()
        {
            _index = -1;
        }

        #endregion

        // сортировка списка
        public void Sort()
        {
            _workers.Sort();
        }


    }
}
