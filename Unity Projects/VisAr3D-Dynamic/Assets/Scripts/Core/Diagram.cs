///////////////////////////////////////////////////////////
//  Diagram.cs
//  Implementation of the Class Diagram
//  Generated by Enterprise Architect
//  Created on:      07-mar-2016 11:56:11
//  Original author: Filipe
///////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;



using Core;
namespace Core {
	public abstract class Diagram : IXmlNode {

		public Core.SoftwareEntity m_SoftwareEntity;
		public Core.LogicalEntity m_LogicalEntity;

		public Diagram(){

		}

		~Diagram(){

		}

	}//end Diagram

}//end namespace Core