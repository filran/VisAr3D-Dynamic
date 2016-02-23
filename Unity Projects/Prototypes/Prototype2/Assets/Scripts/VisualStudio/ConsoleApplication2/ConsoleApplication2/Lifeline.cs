using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

//namespace ConsoleApplication2
//{
    class Lifeline
    {
        //Attributes
        public String Id { get; set; }
        public String Name { get; set; }
        public String Visibility { get; set; }
        public String Represents { get; set; }
        
        public int Left { get; set; }
        public int Top { get; set; }
        public int Right { get; set; }
        public int Bottom { get; set; }
        public int Width { get; set; }
        public int Seqno { get; set; }
        
        private String Geometry { get; set; }

        //Messages
        public List<Message> Messages { get; set; }

        public Lifeline()
        {
            Messages = new List<Message>();
        }

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

                    case "represents":
                        this.Represents = att.Value;
                        break;

                    default:
                        break;
                }

            }
        }

        public void addMessage(Message message)
        {
            this.Messages.Add(message);
        }

        public void geometry(String g)
        {
            this.Geometry = g;
            split();
        }

        private void split()
        {
            char[] delimiter1 = { ';' };
            char[] delimiter2 = { '=' };

            String[] s1 = this.Geometry.Split(delimiter1);

            for (int i = 0; i < s1.Length - 1; i++)
            {
                String[] s2 = s1[i].Split(delimiter2);

                switch (s2[0])
                {
                    case "Left":
                        this.Left = int.Parse(s2[1]);
                        break;
                    case "Top":
                        this.Top = int.Parse(s2[1]);
                        break;
                    case "Right":
                        this.Right = int.Parse(s2[1]);
                        break;
                    case "Bottom":
                        this.Bottom = int.Parse(s2[1]);
                        break;
                }
            }

            this.Width = this.Right - this.Left;
        }
    }
//}