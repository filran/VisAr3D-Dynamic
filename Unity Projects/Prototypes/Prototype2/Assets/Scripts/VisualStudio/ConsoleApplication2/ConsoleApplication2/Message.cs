using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

//namespace ConsoleApplication2
//{
    class Message
    {
        public String Id { get; set; }
        public String Name { get; set; }
        public String Start { get; set; }
        public String End { get; set; }
        public String MessageKind { get; set; }
        public String MessageSort { get; set; }
        public String SendEvent { get; set; }
        public String ReceiveEvent { get; set; }

        public int Left { get; set; }
        public int Top { get; set; }
        public int Right { get; set; }
        public int Bottom { get; set; }
        public int Seqno { get; set; }

        public String IdSource { get; set; }
        public String IdTarget { get; set; }
        public String Label { get; set; }
        public String Privatedata1 { get; set; }
        public String Privatedata2 { get; set; }
        public String Privatedata3 { get; set; }
        public String Privatedata4 { get; set; }
        public String Privatedata5 { get; set; }
        private String Sequence_points { get; set; }

        public int PtStartX { get; set; }
        public int PtStartY { get; set; }
        public int PtEndX { get; set; }
        public int PtEndY { get; set; }
        public int Width { get; set; }

        public Message()
        {

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

                    case "messageKind":
                        this.MessageKind = att.Value;
                        break;

                    case "messageSort":
                        this.MessageSort = att.Value;
                        break;

                    case "sendEvent":
                        this.SendEvent = att.Value;
                        break;

                    case "receiveEvent":
                        this.ReceiveEvent = att.Value;
                        break; 

                    case "privatedata1":
                        this.Privatedata1 = att.Value;
                        break;

                    case "privatedata2":
                        this.Privatedata2 = att.Value;
                        break;

                    case "privatedata3":
                        this.Privatedata3 = att.Value;
                        break;

                    case "privatedata4":
                        this.Privatedata4 = att.Value;
                        break;

                    case "privatedata5":
                        this.Privatedata5 = att.Value;
                        break;

                    case "sequence_points":
                        this.Sequence_points = att.Value;
                        split();
                        break;

                    default:
                        break;
                }
            }
        }

        private void split()
        {
            char[] delimiter1 = { ';' };
            char[] delimiter2 = { '=' };

            String[] s1 = this.Sequence_points.Split(delimiter1);

            for (int i = 0; i < s1.Length - 1; i++)
            {
                String[] s2 = s1[i].Split(delimiter2);

                switch (s2[0])
                {
                    case "PtStartX":
                        this.PtStartX = int.Parse(s2[1]);
                        break;
                    case "PtStartY":
                        this.PtStartY = int.Parse(s2[1]);
                        break;
                    case "PtEndX":
                        this.PtEndX = int.Parse(s2[1]);
                        break;
                    case "PtEndY":
                        this.PtEndY = int.Parse(s2[1]);
                        break;
                }
            }

            this.Width = this.PtEndX - this.PtStartX;
        }
    }
//}