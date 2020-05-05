using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_lesson4
{
    class Program
    {


        static void Main(string[] args)
        {
            // ----------------------------  задание 2а
            //2. Дана коллекция List<T>, требуется подсчитать, сколько раз каждый элемент встречается в данной коллекции:
            //а) для целых чисел;

            List<int> listInt = CreateCollectionInt();
            //listInt.Sort();

            if (listInt != null && listInt.Count > 0)
            {
                Dictionary<int, int> dict2a = new Dictionary<int, int>();

                foreach (int i in listInt)
                {
                    if (dict2a.ContainsKey(i))
                    {
                        dict2a[i] += 1;
                    }
                    else
                    {
                        dict2a.Add(i, 1);
                    }
                }

                Console.WriteLine("Задание 2а");

                //foreach (KeyValuePair<int, int> kvp in dict2a)
                //{
                //    Console.WriteLine(" Элемент " + kvp.Key.ToString() + " встречается раз(а): " + kvp.Value.ToString());
                //}
                WriteResults(dict2a);
            }
            //------------------------------------- Задание 2б
            //2. Дана коллекция List<T>, требуется подсчитать, сколько раз каждый элемент встречается в данной коллекции:
            //б) *для обобщенной коллекции;

            // по заданию не понятно, чем должно быть Т. Предполагаю, Т - любой пользовательский класс.

            List<MyClass> listMy = CreateCollectionMyClass();

            Dictionary<MyClass, int> counterMyClass = CalculateElements(listMy);

            Console.WriteLine("Задание 2б");
            WriteResults(counterMyClass);



            Console.ReadLine();
        }

        private static Dictionary<T, int> CalculateElements<T>(List<T> ar)
        {
            Dictionary<T, int> result = new Dictionary<T, int>();

            foreach (T tt in ar)
            {
                if (result.ContainsKey(tt))
                {
                    result[tt] += 1;
                }
                else
                {
                    result.Add(tt, 1);
                }
            }

            return result;
        }

        private static void WriteResults<W>(Dictionary<W, int> dict)
        {
            foreach (KeyValuePair<W, int> kvp in dict)
            {
                Console.WriteLine(" Элемент " + kvp.Key.ToString() + " встречается раз(а): " + kvp.Value.ToString());
            }
        }


        private static List<MyClass> CreateCollectionMyClass()
        {
            List<MyClass> list = new List<MyClass>();
            list.Add(new MyClass());
            list.Add(new MyClass(1, "first"));
            list.Add(new MyClass(2, "second"));
            list.Add(new MyClass(3, "third"));
            list.Add(new MyClass(4, "fourth"));
            list.Add(new MyClass(5, "fifth"));
            list.Add(new MyClass(5, "fourth"));
            list.Add(new MyClass(4, "fifth"));
            list.Add(new MyClass(2, "second"));
            list.Add(new MyClass(3, "third"));
            list.Add(new MyClass(3, "third"));
            list.Add(new MyClass(99, "last"));
            list.Add(new MyClass());

            return list;
        }

        private static List<int> CreateCollectionInt()
        {
            List<int> list = new List<int> { 1, 3, 5, 6, 9, 4, 2, 4, 1, 2, 3, 3 };
            return list;
        }


    }
}
