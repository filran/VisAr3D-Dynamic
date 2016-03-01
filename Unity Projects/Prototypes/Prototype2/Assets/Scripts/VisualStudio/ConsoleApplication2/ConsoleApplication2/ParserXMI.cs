//NOTES 
// Task: string.Split() in the Message
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Xml;

//namespace ConsoleApplication2
//{
    class ParserXMI
    {

        private String url { get; set; }
        private XmlDocument parserxmi { get; set; }
        public Diagram Diagram { get; set; }
        public ArrayList Messages { get; set; }
        public ArrayList Lifelines { get; set; }
        public ArrayList Class { get; set; }
        public List<Sequence> SequenceDiagrams { get; set; }
        public List<Class> ClassDiagrams { get; set; }

        public ParserXMI(String url){
            this.url = url;
            this.parserxmi = new XmlDocument();
            this.parserxmi.Load(this.url);
            this.Diagram = new Diagram();
            this.SequenceDiagrams = new List<Sequence>();            
            this.ClassDiagrams = new List<Class>();   
            this.Messages = new ArrayList();
            this.Lifelines = new ArrayList();
            this.Class = new ArrayList();

            if (this.validationXMI())
            {
                this.readnodes(this.parserxmi.DocumentElement);
                this.linkingLifelineAndMessage();

                foreach (Sequence s in this.SequenceDiagrams)
                {
                    s.orderLifeline();
                    s.orderMessage();
                }
            }   

            //CLASS
            foreach(Class c in this.Class)
            {
                Console.WriteLine(c.Name);
            }



            //TEST OPERATORS
            //foreach (InteractionOperator i in this.Diagram.InteractionOperator)
            //{
            //    //Console.WriteLine(i.Id+" "+i.Operator);
            //}

            //TEST SEQUENCE DIAGRAMS
            //foreach (Sequence s in this.SequenceDiagrams)
            //{
            //    Console.WriteLine(s.Name +" - "+s.Id);

            //    foreach (Lifeline l in s.Lifelines)
            //    {
            //        Console.WriteLine(l.Seqno + " " +l.Name + "\tid:" + l.Id + "\tVisibility:" + l.Visibility + "\tRepresents:" + l.Represents + "\tLeft:" + l.Left + "\tRight:" + l.Right + "\tTop:" + l.Top + "\tBottom:" + l.Bottom);
                    
            //        foreach (Message m in l.Messages)
            //        {
            //            Console.WriteLine("\t" + m.Name + "\tid:" + m.Id + "\tStart:" + m.Start + "\tEnd:" + m.End + "\tMessageKind:" + m.MessageKind + "\tMessageSort:" + m.MessageSort + "\tSendEvent:" + m.SendEvent + "\tReceiveEvent:" + m.ReceiveEvent + "\tLeft:" + m.Left + "\tRight:" + m.Right + "\tTop:" + m.Top + "\tBottom:" + m.Bottom + "\tSeqno:" + m.Seqno + "\tPrivatedata1:" + m.Privatedata1 + "\tPrivatedata2:" + m.Privatedata2 + "\tPrivatedata3:" + m.Privatedata3 + "\tPrivatedata4:" + m.Privatedata4 + "\tPrivatedata5:" + m.Privatedata5 + "\tPtStartX:" + m.PtStartX + "\tPtStartY:" + m.PtStartY + "\tPtEndX:" + m.PtEndX + "\tPtEndY:" + m.PtEndY + "\tSource:" + m.IdSource + "\tTarget:" + m.IdTarget);
            //            //Console.WriteLine("\t" + m.Seqno + " " + m.Id);
            //        }                    
                
            //    }
            //}
        }

        private bool validationXMI()
        {
            XmlNode noderoot = this.parserxmi.DocumentElement;
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

        //MAIN METHOD!!!!!!!!!!!!!!!!!!
        //for now, this method is reading only tags (OK)
        //It needs to read yours attributes (OK)
        //save lifeline (OK)
        //save message (OK)
        //save link between message and lifeline (OK)
        private void readnodes(XmlNode tag)
        {

            foreach (XmlNode child in tag)
            {
                //save diagrams
                if (child.Name == "diagram")
                {
                    foreach (XmlNode n in child.ChildNodes)
                    {
                        if (n.Name == "properties")
                        {
                            if (n.Attributes["type"].Value == "Sequence")
                            {
                                Sequence s = new Sequence();
                                s.Id = child.Attributes["xmi:id"].Value;
                                s.Name = n.Attributes["name"].Value;
                                this.SequenceDiagrams.Add(s);
                            }

                            if(n.Attributes["type"].Value == "Logical") //Class
                            {
                                Class c = new Class();
                                c.Id = child.Attributes["xmi:id"].Value;
                                c.Name = n.Attributes["name"].Value;
                                this.ClassDiagrams.Add(c);
                            }
                        }
                    }
                }

                //save messagems
                if (child.Name == "message")
                {
                    Message message = new Message();
                    message.addAttribute(readattributes(child));
                    this.Messages.Add(message);
                }

                //save messages' attribute
                if (child.Name == "connector")
                {
                    foreach (Message m in Messages)
                    {
                        if (m.Id == child.Attributes["xmi:idref"].Value)
                        {
                            foreach (XmlNode n in child.ChildNodes)
                            {
                                if (n.Name == "source")
                                {
                                    m.IdSource = n.Attributes["xmi:idref"].Value;
                                }

                                if (n.Name == "target")
                                {
                                    m.IdTarget = n.Attributes["xmi:idref"].Value;
                                }

                                if (n.Name == "appearance")
                                {
                                    m.Seqno = Int32.Parse(n.Attributes["seqno"].Value);
                                }

                                if (n.Name == "labels")
                                {
                                    foreach (XmlNode nn in n.Attributes)
                                    {
                                        if (nn.Name == "mt")
                                        {
                                            m.Label = n.Attributes["mt"].Value;
                                        }
                                    }
                                }

                                if (n.Name == "extendedProperties")
                                {
                                    m.addAttribute(readattributes(n));
                                }
                            }
                        }
                    }
                }

                //save messages' attribute
                //if (child.Name == "Sequence")
                //{
                //    foreach (XmlNode n in child.Attributes)
                //    {
                //        if (n.Name == "start" || n.Name == "end")
                //        {
                //            foreach (Message m in this.Messages)
                //            {
                //                if (child.Attributes["xmi:id"].Value == m.Id)
                //                {
                //                    m.Start = child.Attributes["start"].Value;
                //                    m.End = child.Attributes["end"].Value;
                //                }
                //            }
                //        }
                //    }
                //}

                //save Interaction
                if (child.Name == "fragment")
                {
                    foreach (XmlNode n in child.Attributes)
                    {
                        if (n.Name == "interactionOperator")
                        {
                            InteractionOperator i = new InteractionOperator();
                            i.Id = child.Attributes["xmi:id"].Value;
                            i.Visibility = child.Attributes["visibility"].Value;
                            i.Operator = child.Attributes["interactionOperator"].Value;
                            this.Diagram.InteractionOperator.Add(i);
                        }
                    }
                }

                //save lifelines
                if (child.Name == "lifeline")
                {
                    //String idlififeline = child.Attributes["xmi:id"].Value;
                    Lifeline lifeline = new Lifeline();
                    lifeline.addAttribute(readattributes(child));
                    //Console.WriteLine("Lifeline saved!");
                    //this.Diagram.addLifeline(lifeline);
                    this.Lifelines.Add(lifeline);
                    
                }

                //save class
                if (child.Name == "packagedElement")
                {
                    if (child.Attributes["xmi:type"].Value == "uml:Class")
                    {
                        foreach(XmlNode a in child.Attributes)
                        {
                            if(a.Name == "name")
                            {
                                Class c = new Class();
                                c.addAttribute(readattributes(child));
                                this.Class.Add(c);
                            }
                        }
                    }

                    if (child.Attributes["xmi:type"].Value == "uml:Interface")
                    {
                        foreach(XmlNode a in child.Attributes)
                        {
                            if(a.Name == "name")
                            {
                                Interface i = new Interface();
                                i.addAttribute(readattributes(child));
                                this.Class.Add(i);
                            }
                        }
                    }
                }


                //save lifeline's attribute
                if (child.Name == "element" )
                {
                    foreach (XmlNode att in child.Attributes)
                    {
                        if (att.Name == "subject")
                        {
                            foreach (Lifeline l in this.Lifelines)
                            {
                                if (l.Id == att.Value)
                                {
                                    //l.Geometry = child.Attributes["geometry"].Value;
                                    l.geometry(child.Attributes["geometry"].Value);
                                    l.Seqno = Int32.Parse(child.Attributes["seqno"].Value);
                                }
                            }
                        }
                    }
                }

                if (child.Name == "element" && child.ParentNode.ParentNode.Name == "diagram")
                {
                    foreach (XmlNode n in child.ParentNode.ParentNode.ParentNode)
                    {
                        //Console.WriteLine(n.Name +" - "+n.Attributes["xmi:id"].Value);
                        foreach (Sequence s in this.SequenceDiagrams)
                        {
                            foreach (Lifeline l in this.Lifelines)
                            {
                                if (s.Id == n.Attributes["xmi:id"].Value)
                                {
                                    if (child.Attributes["subject"].Value == l.Id)
                                    {
                                        s.addLifeline(l);
                                    }
                                }
                            }
                        }
                    }
                }

                //linking Lifeline and Message
                //if (child.Name == "Sequence")
                //{
                //    foreach (Message m in Messages)
                //    {
                //        if (m.Id == child.Attributes["xmi:id"].Value)
                //        {
                //            //Console.WriteLine("Achou???");
                //            foreach (Lifeline l in this.Diagram.Lifelines)
                //            {
                //                if (child.Attributes["start"].Value == l.Id)
                //                {
                //                    //Console.WriteLine("Achou???");
                //                    l.addMessage(m);
                //                }
                //            }
                //        }
                //    }
                //}
                                

                //if (child.Name == "Sequence")
                //{
                //    foreach(Lifeline l in this.Diagram.Lifelines){
                //        if (l.Id == child.Attributes["start"].Value)
                //        {
                //            Message message = new Message();
                //            message.addAttribute("start", child.Attributes["start"].Value);
                //        }
                //    }
                //}

                //Console.Write(tabs + "<" + child.Name);
                //foreach(XmlNode att in child.Attributes ){
                //    Console.Write(" " + att.Name + "=" + att.Value);
                //}
                //Console.Write("/>");
                //Console.WriteLine("</" + child.Name + "/>");

                readnodes(child);
            }
        }

        //read and return all attributes
        private Dictionary<String,String> readattributes(XmlNode child)
        {
            Dictionary<String, String> attributes = new Dictionary<string, string>();
            foreach (XmlNode att in child.Attributes)
            {
                attributes.Add(att.Name, att.Value);
            }
            return attributes;
        }

        private void linkingLifelineAndMessage()
        {
            foreach (Lifeline l in this.Lifelines)
            {
                foreach (Message m in this.Messages)
                {
                    if (l.Id == m.IdSource)
                    {
                        l.addMessage(m);
                    }
                }
            }
        }
    }
//}
