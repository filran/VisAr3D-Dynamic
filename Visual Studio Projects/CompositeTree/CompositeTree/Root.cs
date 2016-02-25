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
        ArrayList nodes = new ArrayList();

        public Root(String s)
        {
            this.name = s;
        }
        
        public void add(Tree t)
        {
            nodes.Add(t);
        }

        public void remove(Tree t)
        {
            nodes.Remove(t);
        }

    }
}
