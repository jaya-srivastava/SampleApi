using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sample.Models;
using System.Text.RegularExpressions;

namespace Sample.Models
{
    public class TemplateMetaData
    {
        public Template Template { get; set; }
       
        public WordTemplateMetaData WordTemplateMetaData { get; set; }

        public LookupData LookupData { get; set; }

        internal List<string> ParseVariables(string templateText)
        {
            List<string> varList = new List<string>();
            Regex reg = new Regex(@"\${Object\d{1}}");
            MatchCollection coll = reg.Matches(templateText);

            foreach (Match item in coll)
	        {
               if(! varList.Contains(item.Value))
		        varList.Add(item.Value);
	        }
            
            return varList;
        }
    }

    public class LookupData
    {
        public List<String> Names { get; set; }

        public List<String> SimpleObject { get; set; }

        public List<ComplexObject> ComplexObject { get; set; }

    }

    public class ComplexObject
    {
        //type like Animal, Fruites etc
        public String Type { get; set; }
        public List<string> Options { get; set; }

    }

    public class WordTemplateMetaData
    {
       public string Topic { get; set; }
       public string SubTopic { get; set; }
    }

    public class Template
    {
        public List<string> TextList { get; set; }

        public string Formulae { get; set; }
    }
}
