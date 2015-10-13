using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace KompilatorPólDodatkowych
{
    public class MapReader
    {
        public MapReader()
        {

            defMapFname = Path.Combine(System.Environment.GetEnvironmentVariable("userprofile")
                                        ,@"AppData\Local\Nielsen\Spaceman\Config"
                                        ,@"mappingfile.map");            
        }
        public MapReader(string defMapFname)
        {

            this.defMapFname = defMapFname;
        }

        private List<CustomField> custFields = null;
        private string defMapFname = null;


        /*
         * Current list of custom fields read for the last time
         */
        public List<CustomField> CustomFieldsList
        {
            get
            {
                return this.custFields;
            }
        }

        public int ReadFromTxt(string filename)
        {
            if (custFields == null) custFields = new List<CustomField>();
            try
            {
                using (StreamReader sr = new StreamReader(filename))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {

                        List<string> lineAttr = line.Split(new char[] { ',' }).ToList();

                        if (Regex.Match(lineAttr[0], @"^[^\d;].*").Success)
                        {
                            custFields.Add(new CustomField()
                            {
                                Name = lineAttr[0],
                                DataType = lineAttr[2]
                            });
                        }
                        else
                        {
                            line = "OMITTED:::" + line;
                        }


                        Console.WriteLine(line);
                    }
                }

                return 0;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return 1;
            }
        }
    }
}
