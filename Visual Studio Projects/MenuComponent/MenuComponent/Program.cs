using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuComponent
{
    class Program
    {
        static void Main(string[] args)
        {
            MenuComponent pancakeHouseMenu = new Menu("PANCAKE OUSE MENU", "Breakfat");
            MenuComponent dinerMenu = new Menu("DINER MENU" , "Lunch");
            MenuComponent cafeMenu = new Menu("CAFÉ MENU", "Diner");
            MenuComponent dessertMenu = new Menu("DESSERT MENU", "Dinner");
            
            MenuComponent allMenus = new Menu("ALL MENUS", "ALL menuns combined");

            allMenus.add(pancakeHouseMenu);
            allMenus.add(dinerMenu);
            allMenus.add(cafeMenu);

            dinerMenu.add(new MenuItem("Pasta", "Spaghetti with Marinara Sauce, and a slice of sourdough bread", true, 3.89));
            dinerMenu.add(dessertMenu);
            dessertMenu.add(new MenuItem("Apple Pie","Apple pie with a flakey curst, topped with vanilla ice cream",true,1.59));

            Waitress waitress = new Waitress(allMenus);
            waitress.printMenu();
        }
    }
}
