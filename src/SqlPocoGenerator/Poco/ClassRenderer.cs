using System.Text;

namespace SqlPocoGenerator.Poco
{
    public class ClassRenderer
    {
        private const string Tab = "\t";
        
        public string Render(ClassTemplate template)
        {
            var builder = new StringBuilder();

            // TODO can we do this with Roslyn instead if thats possible
            
            builder.AppendLine("using System;");
            
            // TODO if columns contains type then include the types using statement if necessary
            
            builder.AppendLine();
            builder.AppendLine($"namespace {template.Namespace}");
            builder.AppendLine("{");
            builder.AppendLine($"{Tab}public class {template.TypeName}");
            builder.AppendLine(Tab + "{");

            foreach (var property in template.Properties)
            {
                builder.AppendLine($"{Tab}{Tab}public {property.DataType} {property.LegalCsharpName} " + "{ get; set; }");
            }
            
            builder.AppendLine(Tab + "}");
            builder.AppendLine("}");

            return builder.ToString();
        }
    }
}