using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_lesson3
{
    public delegate void MyDeligate(object o);

    // не хочу в этом примере разносить классы в разные файлы

    class Source
    {
        public event MyDeligate Run;

        public void Start()
        {
            Console.WriteLine("RUN");
            if (Run != null) Run(this);
        }
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

    class Program
    {
        static void Main(string[] args)
        {
            Source s = new Source();
            Observer1 o1 = new Observer1();
            Observer2 o2 = new Observer2();
            MyDeligate d1 = new MyDeligate(o1.Do);
            s.Run += d1;
            s.Run += o2.Do;
            s.Start();
            s.Run -= d1;
            s.Start();

            Console.ReadLine();
        }
    }
}
