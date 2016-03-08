using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Composite
{
    class Component<T> : IComponent<T>
    {
        public T Name { get; set; }

        public Component (T name)
        {
            Name = name;
        }

        public void Add(IComponent<T> c)
        {
            Debug.Log("Cannot add to an item");
        }

        public string Display()
        {
            return Name+"\n";
        }

        
    }
}
