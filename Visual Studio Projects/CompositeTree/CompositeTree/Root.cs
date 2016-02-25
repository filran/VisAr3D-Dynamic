using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompositeTree
{
    public class Root : Tree
    {
        private ArrayList nodes = new ArrayList();

        public void add(Tree t)
        {
            nodes.Add(t);
        }

        public void remove(Tree t)
        {
            nodes.Remove(t);
        }

        public void print()
        {
            IEnumerator root = nodes.GetEnumerator();

            while(root.MoveNext())
            {
                Root r = (Root)root.Current;
                Console.WriteLine(r.name);
            }
        }
    }
}
