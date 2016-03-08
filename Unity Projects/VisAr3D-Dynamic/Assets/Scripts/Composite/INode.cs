using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Composite
{
    interface INode<T>
    {
        string Name { get; set; }
        Dictionary<string, string> Attributes { get; set; }
        void Add(INode<T> node);
        string Print(int depth);
        string Find(string s);
    }
}
