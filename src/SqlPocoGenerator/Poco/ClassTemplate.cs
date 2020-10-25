using System.Collections.Generic;
using SqlPocoGenerator.Schema.Models;

namespace SqlPocoGenerator.Poco
{
    public class ClassTemplate
    {
        public string Namespace { get; set; }
        public string TypeName { get; set; }
        public IList<Column> Properties { get; set; }
    }
}