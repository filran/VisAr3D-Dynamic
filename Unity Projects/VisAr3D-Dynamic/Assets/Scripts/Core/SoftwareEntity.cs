///////////////////////////////////////////////////////////
//  SoftwareEntity.cs
//  Implementation of the Class SoftwareEntity
//  Generated by Enterprise Architect
//  Created on:      14-mar-2016 21:48:46
//  Original author: Filipe
///////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;



using Core;
namespace Core {
	public abstract class SoftwareEntity : IXmlNode {

		public Core.Method m_Method;
		public Core.Attribute m_Attribute;

		public SoftwareEntity(){

		}

		~SoftwareEntity(){

		}

	}//end SoftwareEntity

}//end namespace Core