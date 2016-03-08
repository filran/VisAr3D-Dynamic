using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Composite
{
    interface IComponent<T>
    {
        void Add(IComponent<T> c);
        //IComponent<T> Remove(T c);
        //IComponent<T> Find(T s);
        string Display();
        //T Item { get; set;}
    }
}
