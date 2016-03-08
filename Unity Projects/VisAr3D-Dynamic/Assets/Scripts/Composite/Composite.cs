using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Composite
{
    class Composite<T> : IComponent<T>
    {
        List<IComponent<T>> list;
        public T Name { get; set; }
        public Composite(T name)
        {
            Name = name;
            list = new List<IComponent<T>>();
        }

        public void Add(IComponent<T> c)
        {
            list.Add(c);
        }

        public string Display()
        {
            string s = "";
            s += Name + "\n";

            foreach (IComponent<T> c in list)
            {
                s += c.Display();
            }

            return s;
        }
    }
}
