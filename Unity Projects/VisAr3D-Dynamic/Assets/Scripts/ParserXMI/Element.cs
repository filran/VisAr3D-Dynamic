using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParserXMI
{
    class Element
    {
        public String Geometry { get; set; }
        public String Subject { get; set; }
        public String Seqno { get; set; }
        public String Style { get; set; }

        public String Id { get; set; }
        public String Idref { get; set; }
        public String Type { get; set; }
        public String Name { get; set; }
        public String Scope { get; set; }

        public String IdPackage { get; set; }
        public String isAbstract { get; set; }
        public List<Link> Links { get; set; }

        public Element()
        {
            this.Links = new List<Link>();
        }


        //public String name { get; set; }

    }
}
