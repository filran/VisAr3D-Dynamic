using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuComponent
{
    public class Menu : MenuComponent
    {
        ArrayList menuComponents = new ArrayList();
        String name;
        String description;

        public Menu(String name, String description)
        {
            this.name = name;
            this.description = description;
        }

        public void add(MenuComponent menuComponent)
        {
            menuComponent.add(menuComponent);
        }

        public void remove(MenuComponent menuComponent)
        {
            menuComponent.remove(menuComponent);
        }

        public MenuComponent getChild(int i)
        {
            return (MenuComponent)menuComponents[i];
        }

        public String getName()
        {
            return name;
        }

        public String getDescription()
        {
            return description;
        }

        public void print()
        {
            Console.WriteLine("\n" + getName());
            Console.WriteLine(",     "+getDescription());
            Console.WriteLine("-------------------------");

            IEnumerator iterator = menuComponents.GetEnumerator();
            while(iterator.MoveNext())
            {
                MenuComponent menuComponent = (MenuComponent)iterator;
                menuComponent.print();
            }
        }
    }
}
