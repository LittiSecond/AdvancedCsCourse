using System;

namespace Task_lesson2
{
    class Program
    {
        static void Main(string[] args)
        {
            Departament _workers;

            Console.WriteLine("Создание списка сотрудников ... \n");
            _workers = Load();  //  создание списка работников

            Print(_workers);

            Console.WriteLine("\nСортировка списка сотрудников по имени ... \n");
            _workers.Sort();

            Print(_workers);

            Console.WriteLine("\nДля завершения работы программы нажмите любую клавишу.");
            Console.ReadKey();

        }

        static Departament Load()  // создаёт список работников
        {
            Departament workers = new Departament();

            workers.Add(new FixPayWorker("Иван", "Иванов", 35000));
            workers.Add(new FixPayWorker("Анна", "Молотова", 29000));
            workers.Add(new HourPayWorker("Гюльчетай", "Шахиджаян", 220));
            workers.Add(new HourPayWorker("Алексей", "Петров", 170));

            return workers;
        }

        //  перебор списка сотрудников через foreach 
        static void Print(Departament workers)
        {
            if (workers == null) return;
            foreach (AbstractWorker aw in workers)
            {
                Console.WriteLine(aw.Name + " " + aw.Surname + ", Среднемесячная заработная плата = " +
                    (aw.AverageMonthSelary()).ToString());
            }
        }
    }
}
