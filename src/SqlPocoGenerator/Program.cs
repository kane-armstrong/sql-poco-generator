﻿using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using SqlPocoGenerator.Poco;
using SqlPocoGenerator.Schema;

namespace SqlPocoGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            RunProgram().GetAwaiter().GetResult();
        }

        static async Task RunProgram()
        {
            Console.WriteLine("--------Class generator--------");
            Console.WriteLine("I can generate C# POCOs from tables in a MSSQL database");
            Console.WriteLine("Example connection string: Persist Security Info=False;User ID=MY_SQL_USER;Password=MY_SQL_PASSWORD;Server=localhost,1433; Initial Catalog=MY_CATALOG_NAME");
            Console.Write("Enter connection string: ");
            var cs = Console.ReadLine();
            Console.Write("Enter namespace: ");
            var namespaceName = Console.ReadLine();
            Console.Write("Enter output path: ");
            var outputPath = Console.ReadLine();

            var explorer = new SqlServerSchemaExplorer(cs);
            var tables = await explorer.GetTablesAsync(CancellationToken.None);
            var renderer = new ClassRenderer();
            //            var renderer = new ClassRendererV2();
            foreach (var table in tables)
            {
                Console.WriteLine("Class:");
                var content = renderer.Render(new ClassTemplate
                {
                    Namespace = namespaceName,
                    Properties = table.Columns,
                    TypeName = table.Name.LegalCsharpName
                });
                Console.WriteLine(content);
                var fileName = Path.Join(outputPath, $"{table.Name.LegalCsharpName}.cs");
                Console.WriteLine($"Saving it to disk, see {fileName}");
                File.WriteAllLines(fileName, content.Split(Environment.NewLine));
            }

            Console.WriteLine("Done.");

            Console.ReadKey();
        }
    }
}