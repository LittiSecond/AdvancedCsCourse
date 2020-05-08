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

            //-------------------------------------- Задание 2с
            //2. Дана коллекция List<T>, требуется подсчитать, сколько раз каждый элемент встречается в данной коллекции:
            //в) *используя Linq.
            Console.WriteLine("Задание 2c");

            //var awfulObscure = listMy.GroupBy(c => c.Id); 
            //var awfulObscure = listMy.GroupBy(c => c);
            //foreach (var t in awfulObscure)
            //{
            //    Console.WriteLine("Id = " + t.Key.ToString() + " count = " + t.Count().ToString());
            //    foreach (var t2 in t)
            //    {
            //        Console.WriteLine("    " + t2);
            //    }
            //}

            var awfulObscure = listMy.GroupBy(c => c).Select(g => new { Obj = g.Key, Count = g.Count() } ) ; // @_@

            foreach (var t in awfulObscure)
            {
                Console.WriteLine("объект = " + t.Obj.ToString() + " количество = " + t.Count.ToString());                
            }


            //----------------------------------------  Задание 3

            Console.WriteLine("Задание 3");

            // 3. *Дан фрагмент программы:
            Dictionary<string, int> dict = new Dictionary<string, int>()
            {
                {"four",4 },
                {"two",2 },
                { "one",1 },
                {"three",3 },
            };
            var d = dict.OrderBy(delegate (KeyValuePair<string, int> pair) { return pair.Value; });
            foreach (var pair in d)
            {
                Console.WriteLine("{0} - {1}", pair.Key, pair.Value);
            }
            //а) Свернуть обращение к OrderBy с использованием лямбда - выражения $.
            //а. Свернуть обращение к OrderBy с использованием лямбда-выражения =>.
            Console.WriteLine("Задание 3a");

            var da = dict.OrderBy(kvp => kvp.Value);       
            foreach (var pair in da)
            {
                Console.WriteLine("{0} - {1}", pair.Key, pair.Value);
            }

            //б) *Развернуть обращение к OrderBy с использованием делегата Predicate<T>.
            //b. * Развернуть обращение к OrderBy с использованием делегата .

            //DeligatT3 delT3 = new DeligatT3(MetodToDeligateT3);
            //DeligatT3<KeyValuePair<string, int>> delT3 = new DeligatT3<KeyValuePair<string, int>>(MetodToDeligateT3);
            Func<KeyValuePair<string, int>, int> delT3 = new Func<KeyValuePair<string, int>, int>(MetodToDeligateT3);

            var db = dict.OrderBy(delT3);
            Console.WriteLine("Задание 3b");
            foreach (var pair in db)
            {
                Console.WriteLine("{0} - {1}", pair.Key, pair.Value);
            }

            Console.ReadLine();
            /*
             * Выражение
             * Func<KeyValuePair<string, int>, int> delT3 = new Func<KeyValuePair<string, int>, int>(MetodToDeligateT3);                     
             *          написал наугад, заработало. Но я всё равно не понимаю, какой делигат надо в OrderBy надо засовывать, 
             *          чтобы оно работало.
             * 
             * */

        }

        /// <summary>
        /// Делигат для задания 3  -  неудачные попытки
        /// </summary>
        /// <param name="kvp"></param>
        /// <returns></returns>
        //delegate int DeligatT3(KeyValuePair<string, int> kvp);
        //delegate int DeligatT3<T1, T2>(<KeyValuePair<T1, T2>> t2);
        //delegate int DeligatT3<T2>(T2 t2);

        /// <summary>
        /// Метод для передачи делигату в задании 3
        /// </summary>
        /// <param name="kvp"></param>
        /// <returns></returns>
        private static int MetodToDeligateT3(KeyValuePair<string, int> kvp)
        {
            return kvp.Value;
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
