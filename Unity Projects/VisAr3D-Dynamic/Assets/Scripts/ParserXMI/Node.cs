using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParserXMI
{
    public class Node
    {
        //XML atributtes
        public List<Node> Nodes { get; set; }
        public string Tag { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Id { get; set; }
        public string Visibility { get; set; }
        public string IdPackage { get; set; }
        public string Geometry { get; set; }
        public string Subject { get; set; }
        public string Seqno { get; set; }
        public string Style { get; set; }
        public string IsAbstract { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
       
        public Node()
        {
            Nodes = new List<Node>();
        }

        public void Add(Node n)
        {
            Nodes.Add(n);
        }
        
    }
}
