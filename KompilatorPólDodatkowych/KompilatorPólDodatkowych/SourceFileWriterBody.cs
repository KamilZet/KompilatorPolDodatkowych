using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace KompilatorPólDodatkowych
{
    class SourceFileWriterBody : SourceFileWriter
    {
        protected override void AddHead()
        {

        }
        public override void AddBody(IList<CustomField> cfList)
        {
            
            XElement contacts = new XElement("CustomFields",new XAttribute("Version", "1.0"),
                                new XElement("Fields",
                                    from cf in cfList
                                    select new XElement("Field"
                                        , new XAttribute("Name", cf.Name)
                                        , new XAttribute("ObjectType", cf.ObjectType)
                                        , new XAttribute("LOE", cf.LOE)
                                        , new XAttribute("DataType", cf.DataType)
                                        , new XAttribute("DatabaseField", cf.DatabaseField))));
        }
    }
}
