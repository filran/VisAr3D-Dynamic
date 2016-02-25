using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompositeTree
{
    public abstract class Tree
    {
        protected String name {get;set;}

        protected abstract void add(Tree t);

        protected abstract void remove(Tree t);

        protected abstract Tree getChild(Tree t);
    }
}
