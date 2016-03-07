using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParserXMI
{
    class Link
    {
        public String Id { get; set; }
        public String Tag { get; set; } //Association, Generalization etc
        public String Start { get; set;}
        public String End { get; set; }
    }
}
