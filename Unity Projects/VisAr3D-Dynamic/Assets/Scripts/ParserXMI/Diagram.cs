using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParserXMI
{
    class Diagram
    {
        public String Type { get; set; }
        public String Id { get; set; }
        public String Name { get; set; }
        public List<Element> Elements {get;set;}

        public Diagram()
        {
            this.Elements = new List<Element>();
        }
    }
}
