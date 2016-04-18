///////////////////////////////////////////////////////////
//  SoftwareEntity.cs
//  Implementation of the Class SoftwareEntity
//  Generated by Enterprise Architect
//  Created on:      15-mar-2016 08:28:14
//  Original author: Filipe
///////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;



using Core;
namespace Core {
	public abstract class SoftwareEntity : IXmlNode {

        //public Core.Method m_Method;
        //public Core.Attribute m_Attribute;
        public Dictionary<IXmlNode, IXmlNode> Relationships { get; private set; }

		public SoftwareEntity(){
            Relationships = new Dictionary<IXmlNode, IXmlNode>();
		}
                
        public void AddRelationshipWith(IXmlNode relationship , IXmlNode softwareentity)
        {
            Relationships.Add(relationship, softwareentity);
        }

		~SoftwareEntity(){

		}

	}//end SoftwareEntity

}//end namespace Core