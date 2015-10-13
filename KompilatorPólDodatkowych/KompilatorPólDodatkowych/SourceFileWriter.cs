using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace KompilatorPólDodatkowych
{
    public class SourceFileWriter
    {
        private XDocument doc = null;
        private readonly string fName = "SysLevelCustomFields.xml";
        private DirectoryInfo archSubDir = null;
        private DirectoryInfo confDir = null;

        public SourceFileWriter()
        {
            doc = new XDocument();

            DefaultCfFilename = Path.Combine(System.Environment.GetEnvironmentVariable("userprofile")
                                        , @"AppData\Local\Nielsen\Spaceman\Config"
                                        , fName);
            
        }

        public SourceFileWriter(string defCfFilename)
        {
            doc = new XDocument();
            this.defCfFilename = defCfFilename;

        }


        private string defCfFilename = null;

        public string DefaultCfFilename
        {
            get
            {
                return defCfFilename;
            }
            private set
            {
                defCfFilename = value;
                confDir = Directory.GetParent(value);
                archSubDir = new DirectoryInfo(Path.Combine(confDir.ToString(), "CustomFieldsArch"));
            }
        }

    protected XDocument Doc
    {
        get
        {
            return this.doc;
        }
        set
        {
            doc = value;
        }
    }

    protected virtual void AddHead()
    {
        doc.Add(new XElement("CustomFields"
                    ,new XAttribute("Version","1.0")));
                    
    }

    public string ArchSubDir
    {
        get
        {
            return archSubDir.ToString();
        }
    }

    public void Save(string outDirName)
    {
        //if (!archSubDir.Exists) archSubDir.Create();
        //if (File.Exists(Path.Combine(archSubDir.ToString(),compArchFilename())))
        //File.Move(this.defCfFilename, Path.Combine(archSubDir.ToString(),compArchFilename()));
        this.doc.Save(outDirName+"\\"+fName);
    }

    public virtual void AddBody(IList<CustomField> cfList)
    {
        XElement contacts = new XElement("CustomFields",
                                new XElement("Fields",
                                    from cf in cfList
                                    select new XElement("Field"
                                        ,new XAttribute("Name", cf.Name)
                                        ,new XAttribute("ObjectType", cf.ObjectType)
                                        ,new XAttribute("LOE", cf.LOE)
                                        ,new XAttribute("DataType", cf.DataType)
                                        ,new XAttribute("DatabaseField", cf.DatabaseField))));

    }

    private string compArchFilename()
    {
        string dateStamp = DateTime.Today.ToString("yyyy_MM_dd");
        return (Path.GetFileNameWithoutExtension(this.fName) + "_" + dateStamp + Path.GetExtension(this.fName));
    }
    private string incrArchFilename(string path)
    {
        return "test";
    }
    
}

}
