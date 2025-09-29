using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DocXML2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (XmlWriter writer = XmlWriter.Create("books.xml"))
            {
                string pi = "type=\"text/xml\"href=\"books.xsl\"";
                writer.WriteProcessingInstruction("xml-stylesheet", pi);
                writer.WriteDocType("catalog", null, null, "<!ENTITY h \"hardcore\")>");
                writer.WriteStartElement("book");
                writer.WriteAttributeString("ISBN", "983112312");
                writer.WriteAttributeString("yearrpublished", "2002");
                writer.WriteElementString("author", "Mahesh Chand");
                writer.WriteElementString("title", "visual C# Programming");
                writer.WriteElementString("price", "44.95");
                writer.WriteEndElement();
                writer.Flush();
                Console.ReadLine();

            }
        }
    }
}
