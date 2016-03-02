using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

//namespace ConsoleApplication2
//{
    class Class : Diagram
    {
        public String Visibility { get; set; }
        public List<Attribute> Attributes = new List<Attribute>();
        public List<Method> Methods = new List<Method>();

		public void addAttribute(Dictionary<String, String> attributes)
		{
			foreach (var att in attributes)
			{
				switch (att.Key)
				{
				case "xmi:id":
					this.Id = att.Value;
					break;
					
				case "name":
					this.Name = att.Value;
					break;
					
				case "visibility":
					this.Visibility = att.Value;
					break;
					
				default:
					break;
				}
				
			}
		}

    }
//}
