///////////////////////////////////////////////////////////
//  XMI.cs
//  Implementation of the Class XMI
//  Generated by Enterprise Architect
//  Created on:      07-mar-2016 12:02:20
//  Original author: hercules
///////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using UnityEngine;
using Composite;

namespace ParserXMI {
	public class XMI {

        public XmlDocument ParserXMI { get; set; }
        public List<Node> Nodes { get; set; }
        private List<Node> Classes { get; set; }

        public XMI(String url)
        {
            Nodes = new List<Node>();
            Classes = new List<Node>();

            ParserXMI = new XmlDocument();
            ParserXMI.Load(url);

            if(validationXMI())
            {
                ReadNodes(ParserXMI.DocumentElement);
            }

            //TESTS
            //Packages
            foreach(Node n in Nodes)
            {
                Debug.Log(n.Tag+" "+n.Type+" "+n.Name);
            }


        }

        private bool validationXMI()
        {
            XmlNode noderoot = this.ParserXMI.DocumentElement;
            if (noderoot.Name == "xmi:XMI")
            {
                //Console.WriteLine("It is XMI");
                return true;
            }
            else
            {
                //Console.WriteLine("It is not XMI");
                return false;
            }
        }

        private void ReadNodes(XmlNode node)
        {
            foreach(XmlNode n in node)
            {
                BuildPackage(n);
                BuildDiagram(n);
                BuildClass(n);
                ReadNodes(n);
            }
        }

        private void BuildPackage(XmlNode node)
        {
            if (node.Name == "packagedElement" && node.ParentNode.ParentNode.Name == "uml:Model")
            {
                Node n = new Node();
                n.Tag = node.Name;
                n.Type = node.Attributes["xmi:type"].Value;
                n.Id = node.Attributes["xmi:id"].Value;
                n.Name = node.Attributes["name"].Value;
                n.Visibility = node.Attributes["visibility"].Value;
                Nodes.Add(n);
            }
        }

        private void BuildDiagram(XmlNode node)
        {
            if (node.Name == "diagram")
            {
                Node diagram = new Node();
                diagram.Tag = node.Name;
                diagram.Id = node.Attributes["xmi:id"].Value;

                if(node.HasChildNodes)
                {
                    foreach(XmlNode child in node)
                    {
                        switch(child.Name)
                        {
                            case "model":
                                diagram.IdPackage = child.Attributes["package"].Value;
                                break;

                            case "properties":
                                diagram.Name = child.Attributes["name"].Value;
                                break;
                            //case "elements":
                            //    if(child.HasChildNodes)
                            //    {
                            //        foreach(XmlNode e in child)
                            //        {
                            //            Node element = new Node();
                            //            element.Tag = e.Name;

                            //            foreach(XmlNode att in e.Attributes)
                            //            {
                            //                switch(att.Name)
                            //                {
                            //                    case "geometry":
                            //                        element.Geometry = att.Value;
                            //                        break;

                            //                    case "subject":
                            //                        element.Subject = att.Value;
                            //                        break;

                            //                    case "seqno":
                            //                        element.Seqno = att.Value;
                            //                        break;

                            //                    case "style":
                            //                        element.Style = att.Value;
                            //                        break;
                            //                }
                            //            }
                            //            diagram.Add(element);
                            //        }
                            //    }
                            //    break;
                        }
                    }
                }

                foreach(Node package in Nodes)
                {
                    if (package.Type == "uml:Package")
                    {
                        if(package.Id == diagram.IdPackage)
                        {
                            package.Add(diagram);
                        }
                    }
                }                
            }
        }

        private void BuildClass(XmlNode node)
        {
            if (node.Name == "packagedElement" && node.Attributes["xmi:type"].Value == "uml:Class")
            {
                foreach (XmlNode n in node.ChildNodes)
                {
                    //<packagedElement xmi:type="uml:Class" xmi:id="EAID_AFD34DB5_253F_42f2_9819_E489243D89D6" name="ClassA" visibility="public" isAbstract="true"/>
                    Node c = new Node();
                    c.Tag = n.Name;
                    c.Type = n.Attributes["xmi:type"].Value;
                    c.Id = n.Attributes["xmi:id"].Value;
                    c.Name = n.Attributes["name"].Value;
                    c.Visibility = n.Attributes["visibility"].Value;
                    c.IsAbstract = n.Attributes["isAbstract"].Value;

                    foreach(Node diagram in Nodes)
                    {
                        if (diagram.Tag == "diagram")
                        {
                            
                        }
                    }
                }
            }
        }

		~XMI(){

		}

	}//end XMI

}//end namespace ParserXMI