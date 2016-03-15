using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Core;
using ParserXMI;

namespace Scripts
{
    class Program : MonoBehaviour
    {
        void Start()
        {
            TheCore core = new TheCore(@"F:\Users\Filipe\Documents\Programacao\GitHub\VisAr3D-Dynamic\EA Projects\TestEA.xmi");

            //TEST
            //PACKAGES OK
            foreach(IXmlNode n in core.TheXMI.Packages)
            {
                //Debug.Log(n.Name +" "+n.Id);
            }

            //DIAGRAMS OK
            foreach (Node n in core.TheXMI.Diagrams)
            {
                string s = n.Name + " " + n.Id +"\n";

                foreach(Node nn in n.Nodes)
                {
                    //<element geometry="Left=601;Top=50;Right=691;Bottom=370;" subject="EAID_F5EB71F5_E679_4810_A292_8B34D229D800" seqno="1" style="DUID=D1B9B029;NSL=0;BCol=-1;BFol=-1;LCol=-1;LWth=-1;fontsz=0;bold=0;black=0;italic=0;ul=0;charset=0;pitch=0;"/>
                    s += "\tgeometry: " + nn.Geometry + " | subject:" + nn.Subject + " | seqno:" + nn.Seqno + " | style:" + nn.Style +"\n";
                }

                //Debug.Log(s);
            }

            //CLASSES
            foreach (Node n in core.TheXMI.Classes)
            {
                string s = n.Type + " | " + n.Name + " | " + n.Id + " | " + n.IdPackage + "\n";

                foreach(Node nn in n.Nodes)
                {
                    //<Association xmi:id="EAID_0EFAE9AD_554E_4771_84A4_C31E2D5EAF94" start="EAID_AFD34DB5_253F_42f2_9819_E489243D89D6" end="EAID_75F51133_37DF_46d8_A58C_B6495A9DDD38"/>
                    s += "\t"+nn.Tag+" | id:"+nn.Id+" | start:"+nn.Start+" | end:"+nn.End+"\n";
                }

                //Debug.Log(s);
            }

            //RELATIONSHIP OK
            foreach (Node n in core.TheXMI.Relationships)
            {
                string s = n.Type + " | id:" + n.Id + " | start:" + n.Start + " | end:" + n.End + "\n";
                s += "\tsource:"+n.IdSource+" | target:"+n.IdTarget+"";

                //Debug.Log(s);
            }

            //TEST
            //THECORE
            Debug.Log(core.Packages.Count);
            foreach(Package p in core.Packages)
            {
                
            }
        }
    }
}
