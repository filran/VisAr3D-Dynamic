using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuComponent
{
    public class MenuItem : MenuComponent
    {
        String name;
        String description;
        bool vegetarian;
        double price;

        public MenuItem(String name, String description, bool vegetarian, double price)
        {
            this.name = name;
            this.description = description;
            this.vegetarian = vegetarian;
            this.price = price;
        }

        new public String getName()
        {
            return name;
        }

        new public String getDescription()
        {
            return description;
        }

        new public double getPrice()
        {
            return price;
        }

        new public bool isVegeterian()
        {
            return vegetarian;
        }

        new public void print()
        {
            Console.WriteLine("   " + getName());
            if(isVegeterian())
            {
                Console.WriteLine("(v)");
            }
            Console.WriteLine(",   " + getPrice());
            Console.WriteLine("        --  " + getDescription());
        }
    }
}
