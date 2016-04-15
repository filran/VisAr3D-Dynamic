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
using Core;

namespace ParserXMI {
	public class XMI {

        private XmlDocument ParserXMI { get; set; }
        public List<IXmlNode> Packages { get; private set; }
        public List<IXmlNode> Diagrams { get; private set; }
        public List<IXmlNode> Classes { get; private set; }
        public List<IXmlNode> Relationships { get; private set; }
        
        public XMI(string url)
        {
            ParserXMI = new XmlDocument();
            ParserXMI.Load(url);

            if(validationXMI())
            {
                Packages = new List<IXmlNode>();
                Diagrams = new List<IXmlNode>();
                Classes = new List<IXmlNode>();
                Relationships = new List<IXmlNode>();

                ReadNodes(ParserXMI.DocumentElement);
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
                BuildClassElement(n);

                BuildRelationshipPackagedElement(n);
                BuildRelationshipLink(n);
                BuildRelationshipConnector(n);

                ReadNodes(n);
            }
        }

        private void AddAttributes(XmlNode node , IXmlNode n)
        {
            foreach(XmlNode att in node.Attributes)
            {
                switch(att.Name)
                {
                    case "xmi:type":
                        n.Type = att.Value;
                        break;

                    case "type":
                        n.Type = att.Value;
                        break;

                    case "xmi:id":
                        n.Id = att.Value;
                        break;

                    case "xmi:idref":
                        n.Id = att.Value;
                        break;

                    case "name":
                        n.Name = att.Value;
                        break;

                    case "visibility":
                        n.Visibility = att.Value;
                        break;

                    case "isAbstract":
                        n.IsAbstract = att.Value;
                        break;

                    case "package":
                        n.IdPackage = att.Value;
                        break;

                    case "geometry":
                        n.Geometry = att.Value;
                        BreakGeometry(att.Value, n);
                        break;

                    case "subject":
                        n.Subject = att.Value;
                        break;

                    case "seqno":
                        n.Seqno = att.Value;
                        break;

                    case "style":
                        n.Style = att.Value;
                        break;

                    case "start":
                        n.Start = att.Value;
                        break;

                    case "end":
                        n.End = att.Value;
                        break;
                }
            }
        }

        private void BreakGeometry(string geometry , IXmlNode n)
        {
            char[] char1 = { ';' };
            char[] char2 = { '=' };
            string[] geo1 = geometry.Split(char1);

            foreach(string g in geo1)
            {
                //Debug.Log(g);
                string[] geo2 = g.Split(char2);

                if(geo2.Length > 1)
                {
                    Debug.Log(geo2[0] + " " + geo2[1]);

                    switch (geo2[0])
                    {
                        case "Left":
                            Debug.Log("LLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLL");
                            n.Left = geo2[1];
                            break;

                        case "Top":
                            n.Top = geo2[1];
                            break;

                        case "Right":
                            n.Right = geo2[1];
                            break;

                        case "Bottom":
                            n.Bottom = geo2[1];
                            break;
                    
                    }
                }
            }


        }


        private void BuildPackage(XmlNode node)
        {
            if (node.Name == "packagedElement" && node.ParentNode.ParentNode.Name == "uml:Model")
            {
                IXmlNode n = new Package();
                AddAttributes(node, n);
                Packages.Add(n);
            }
        }   


        private void BuildDiagram(XmlNode node)
        {
            if(node.Name == "diagram")
            {
                IXmlNode n = new Node();
                AddAttributes(node, n);

                foreach(XmlNode subnode in node.ChildNodes)
                {
                    switch(subnode.Name)
                    {
                        case "model":
                            AddAttributes(subnode, n);
                            break;

                        case "properties":
                            AddAttributes(subnode, n);
                            break;

                        case "elements":
                            foreach(XmlNode element in subnode.ChildNodes)
                            {
                                Node e = new Node();
                                AddAttributes(element, e);
                                n.ChildNodes.Add(e);
                            }
                            break;
                    }
                }
                Diagrams.Add(n);
            }
        }

        private void BuildClass(XmlNode node)
        {
            if (node.Name == "packagedElement" && node.Attributes["xmi:type"].Value == "uml:Class" )
            {
                IXmlNode n = new Class();
                AddAttributes(node, n);
                foreach (XmlNode subnode in node.ChildNodes)
                {
                    switch (subnode.Name)
                    {
                        case "model":
                            AddAttributes(subnode, n);
                            break;

                        case "properties":
                            AddAttributes(subnode, n);
                            break;
                    }
                }
                Classes.Add(n);  
            }    
        }

        private void BuildClassElement(XmlNode node)
        {
            if (node.Name == "element" && node.ParentNode.Name == "elements" && node.ParentNode.ParentNode.Name == "xmi:Extension")
            {
                foreach(IXmlNode n in Classes)
                {
                    if(node.Attributes["xmi:idref"].Value == n.Id)
                    {
                        AddAttributes(node,n);
                        foreach(XmlNode subnode in node.ChildNodes)
                        {
                            switch (subnode.Name)
                            {
                                case "model":
                                    AddAttributes(subnode, n);
                                    break;

                                case "links":
                                    foreach(XmlNode link in subnode.ChildNodes)
                                    {
                                        IXmlNode l = new Relationship();
                                        l.Tag = link.Name;
                                        AddAttributes(link, l);
                                        n.ChildNodes.Add(l);
                                    }
                                    break;
                            }
                        }
                    }
                }
            }


            if(node.Name == "element" && node.ParentNode.Name == "elements" && node.ParentNode.ParentNode.Name == "diagram")
            {
                foreach (IXmlNode n in Classes)
                {
                    if(node.Attributes["subject"].Value == n.Id)
                    {
                        AddAttributes(node , n);
                    }
                }
            }

        }


        private void BuildRelationshipPackagedElement(XmlNode node)
        {
            if (node.Name == "packagedElement" )
            {
                if (node.Attributes["xmi:type"].Value == "uml:Realization" || node.Attributes["xmi:type"].Value == "uml:Association")
                {
                    IXmlNode n = new Relationship();
                    n.Tag = n.Name;
                    AddAttributes(node, n);
                    Relationships.Add(n);
                }  
            }
        }

        private void BuildRelationshipLink(XmlNode node)
        {
            if (node.Name == "links" && node.ParentNode.Name == "element")
            {
                foreach(XmlNode subnode in node)
                {
                    foreach(IXmlNode r in Relationships )
                    {
                        if (subnode.Attributes["xmi:id"].Value == r.Id)
                        {
                            AddAttributes(subnode,r);
                        }
                    }
                }
            }
        }

            private void AddGeneralizationToRelationships(XmlNode node)
        {
            if (node.Name == "connector")
            {
                foreach (XmlNode subnode in node.ChildNodes)
                {
                    if (subnode.Name == "properties")
                    {
                        if (subnode.Attributes["ea_type"].Value == "Generalization")
                        {
                            IXmlNode r = new Relationship();
                            r.Name = "Generalization";
                            AddAttributes(node, r);
                            AddAttributes(subnode, r);
                            Relationships.Add(r);
                        }
                    }
                }
            }
        }

        private void BuildRelationshipConnector(XmlNode node)
        {
            AddGeneralizationToRelationships(node);

            if (node.Name == "connector")
            {
                foreach(IXmlNode n in Relationships)
                {
                    if(node.Attributes["xmi:idref"].Value == n.Id)
                    {
                        foreach(XmlNode subnode in node.ChildNodes)
                        {
                            switch(subnode.Name)
                            {
                                case "source":
                                    n.IdSource = subnode.Attributes["xmi:idref"].Value;
                                    break;

                                case "target":
                                    n.IdTarget = subnode.Attributes["xmi:idref"].Value;
                                    n.Aggregation = subnode.ChildNodes.Item(2).Attributes["aggregation"].Value;
                                    break;

                                case "properties":
                                    n.EA_Type = subnode.Attributes["ea_type"].Value;
                                    break;
                            }
                        }
                    }
                }
            }
        }

		~XMI(){

		}

	}//end XMI

}//end namespace ParserXMI