using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompositeTree
{
    public abstract class Tree
    {
        public String name {get;set;}

        public void add(Tree t)
        {
            throw new NotImplementedException();
        }

        public void remove(Tree t)
        {
            throw new NotImplementedException();
        }

        //protected abstract Tree getChild(Tree t);
    }
}
