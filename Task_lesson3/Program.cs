using System;

namespace Task_lesson3
{
    public delegate void MyDeligate(object o);
    public delegate void OurDeligate<T>(T arg);

    // не хочу в этом примере разносить классы в разные файлы

    class Source
    {
        public event MyDeligate Run;
        public event OurDeligate<string> RunS;
        public event OurDeligate<int> RunI;

        private int _index;
        private string _name;

        public Source(int index, string name)
        {
            _index = index;
            _name = name;
        }

        public void Start()
        {
            Console.WriteLine("RUN");
            Run?.Invoke(this);
            RunS?.Invoke(_name);
            RunI?.Invoke(_index);
        }

        public override string ToString() => _name + " с индексом " + _index.ToString();
    }

    class Observer1
    {
        public void Do(object o)
        {
            Console.WriteLine($"Первый. Принял, что объект {o} побежал", o);
        }
    }

    class Observer2
    {
        public void Do(object o)
        {
            Console.WriteLine($"Второй. Принял, что объект {o} побежал", o);
        }
    }

    class Observer3<T2>
    {
        private readonly string _name;

        public Observer3(string name)
        {
            _name = name;
        }

        public void Do(T2 arg)
        {
            Console.WriteLine("{1}. Принял, что объект {0} побежал", arg, _name);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Source s = new Source(1000, "Тысячный");
            Observer1 o1 = new Observer1();
            Observer2 o2 = new Observer2();
            Observer3<int> o3 = new Observer3<int>("Третий");
            Observer3<string> o4 = new Observer3<string>("Четвёртый");
            MyDeligate d1 = new MyDeligate(o1.Do);
            s.Run += d1;
            s.Run += o2.Do;
            s.RunI += o3.Do;
            s.RunS += o4.Do;
            s.Start();
            s.Run -= d1;
            s.RunI -= o3.Do;
            s.Start();

            Console.ReadLine();
        }
    }
}
