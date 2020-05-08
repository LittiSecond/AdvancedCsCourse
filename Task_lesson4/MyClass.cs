using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_lesson4
{
    /// <summary>
    /// Класс для использования в задании 2.
    /// </summary>
    class MyClass : IEquatable<MyClass>
    {
        public int Id { get; private set; }
        public string Name { get; private set; }

        public MyClass()
        {
            Id = -1;
            Name = String.Empty;
        }

        public MyClass(int id, string name)
        {
            Id = id;
            Name = name;
        }

        // чтобы можно было сделать ключём в Dictionary
        public bool Equals(MyClass other)
        {
            if (other == null) return false;
            return Id == other.Id && Name == other.Name;
        }

        // чтобы можно было сделать ключём в Dictionary
        public override int GetHashCode()
        {
            return (Name + Id.ToString()).GetHashCode();
        }

        public override string ToString()
        {
            return Name + " Id = " + Id.ToString();
        }
    }
}
