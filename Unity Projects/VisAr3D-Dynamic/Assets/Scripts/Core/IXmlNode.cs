///////////////////////////////////////////////////////////
//  IXmlNode.cs
//  Implementation of the Class IXmlNode
//  Generated by Enterprise Architect
//  Created on:      15-mar-2016 08:28:13
//  Original author: Filipe
///////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ParserXMI;


namespace Core {
	public abstract class IXmlNode {

        //XML atributtes
        public string Tag { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Id { get; set; }
        public string Visibility { get; set; }
        public string Represents { get; set; }
        public string IdPackage { get; set; }

        public string Geometry { get; set; }
        public float Left { get; set; }
        public float Top { get; set; }
        public float Right { get; set; }
        public float Bottom { get; set; }

        public string Subject { get; set; }
        public string Seqno { get; set; }
        public string Style { get; set; }
        public string IsAbstract { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public string IdSource { get; set; }
        public string IdTarget { get; set; }
        public string EA_Type { get; set; } //aggregation, association etc
        public string Aggregation { get; set; } //none, composite, shared etc

        //messageKind messageSort sendEvent receiveEvent
        public string MessageKind { get; set; }
        public string MessageSort { get; set; }
        public string SendEvent { get; set; }
        public string ReceiveEvent { get; set; }
        public string Label { get; set; }
        //"PtStartX=118;PtStartY=-147;PtEndX=283;PtEndY=-147;" />
        public string Sequence_Points { get; set; }
        public float PtStartX { get; set; }
        public float PtStartY { get; set; }
        public float PtEndX { get; set; }
        public float PtEndY { get; set; }
              

        public List<IXmlNode> ChildNodes { get; set; }

		public IXmlNode(){
            ChildNodes = new List<IXmlNode>();
		}

        public virtual void Add(IXmlNode node)
        {
            ChildNodes.Add(node);
        }

        public IXmlNode FindById(List<IXmlNode> list ,  string id)
        {
            IXmlNode r = new Package();
            foreach(IXmlNode l in list)
            {
                if(l.Id.Equals(id))
                {
                    r = l;
                }
            }
            return r;
        }

        //public IXmlNode FindById(List<IXmlNode> list , int seqno)
        //{
        //    foreach(IXmlNode node in list)
        //    {
        //        if(node.Seqno == seqno)
        //        {

        //        }
        //    }

        //    return new SequenceDiagram();
        //}

		~IXmlNode(){

		}

	}//end IXmlNode

}//end namespace Core