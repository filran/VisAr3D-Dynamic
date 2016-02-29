using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MenuComponent
{
    public abstract class MenuComponent
    {
        public void add(MenuComponent menuComponent)
        {
            //throw new System.NotSupportedException();
        }

        public void remove(MenuComponent menuComponent)
        {
            //throw new NotSupportedException();
        }

        //public MenuComponent getChild(int i)
        //{
        //    throw new NotSupportedException();
        //}

        public String getName()
        {
            throw new NotSupportedException();
        }

        public String getDescription()
        {
            throw new NotSupportedException();
        }

        public double getPrice()
        {
            throw new NotSupportedException();
        }

        public bool isVegeterian()
        {
            throw new NotSupportedException();
        }

        public void print()
        {
            //throw new NotSupportedException();
        }

    }
}
