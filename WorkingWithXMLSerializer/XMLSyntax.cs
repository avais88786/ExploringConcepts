using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WorkingWithXMLSerializer
{
    public class XMLSyntax
    {
        public class Root
        {
            public Root()
            {
                FirstElement = new Element();
            }

            public Element FirstElement;
        }

        public class Element
        {
            public string ElementName;
        }
    }
}
