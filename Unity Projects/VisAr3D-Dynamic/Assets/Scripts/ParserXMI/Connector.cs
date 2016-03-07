using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.ParserXMI
{
    class Connector
    {
        public String Idref { get; set; }
        public List<Connector> Source { get; set; }
        public List<Connector> Target { get; set; }



        public Connector()
        {
            this.Source = new List<Connector>();
            this.Target = new List<Connector>();
        }

    }
}
