using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

//namespace ConsoleApplication2
//{
    class Diagram
    {
        public String Id { get; set; }
        public String Name { get; set; }
        
        public ArrayList InteractionOperator { get; set; }

        public Diagram()
        {
            this.InteractionOperator = new ArrayList();
        }

    }
//}