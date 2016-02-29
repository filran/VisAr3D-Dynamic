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

        new public void add(MenuComponent menuComponent)
        {
            menuComponent.add(menuComponent);
        }

        new public void remove(MenuComponent menuComponent)
        {
            menuComponent.remove(menuComponent);
        }

        //public MenuComponent getChild(int i)
        //{
        //    return (MenuComponent)menuComponents[i];
        //}

        new public String getName()
        {
            return name;
        }

        new public String getDescription()
        {
            return description;
        }

        new public void print()
        {
            Console.WriteLine("\n" + getName());
            Console.WriteLine(",     "+getDescription());
            Console.WriteLine("-------------------------");

            while(menuComponents.Count > 0)
            {
                foreach(MenuComponent m in menuComponents)
                {
                    m.print();
                }
            }
        }
    }
}
