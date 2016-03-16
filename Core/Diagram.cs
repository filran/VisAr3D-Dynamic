///////////////////////////////////////////////////////////
//  Diagram.cs
//  Implementation of the Class Diagram
//  Generated by Enterprise Architect
//  Created on:      15-mar-2016 14:08:33
//  Original author: Filipe
///////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;



using Core;
namespace Core {
	public abstract class Diagram : IXmlNode {

        //public Core.SoftwareEntity m_SoftwareEntity;
        //public Core.LogicalEntity m_LogicalEntity;
        public List<IXmlNode> SoftwareEntities { get; set; }

		public Diagram(){
            SoftwareEntities = new List<IXmlNode>();
		}

		~Diagram(){

		}

	}//end Diagram

}//end namespace Core