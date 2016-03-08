using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Composite
{
    class Node<T> : INode<T>
    {
        public string Name { get; set; }
        public Dictionary<string, string> Attributes { get; set; }
        public List<INode<T>> Nodes { get; set; }
        public Node()
        {
            Attributes = new Dictionary<string, string>();
            Nodes = new List<INode<T>>();
        }

        public void Add(INode<T> node)
        {
            Nodes.Add(node);
        }

        public string Print(int depth)
        {
            string s = "";

            s += new String(' ',depth) + Name+" ";
            //foreach (KeyValuePair<string, string> att in Attributes)
            //{
            //    s += att.Key + ":" + att.Value + " ";
            //}
            s += "\n";

            foreach(INode<T> n in Nodes)
            {
                s += n.Print(depth + 2);
            }

            return s;
        }

        public string Find(string s)
        {
            string ss = "";
            foreach(INode<string> n in Nodes)
            {
                if(n.Name.Equals(s))
                {
                    ss = n.Name;
                }
            }
            return ss;
        }
    }
}
