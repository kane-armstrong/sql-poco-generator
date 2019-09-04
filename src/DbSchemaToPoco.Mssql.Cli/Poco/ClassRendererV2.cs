using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace DbSchemaToPoco.Mssql.Cli.Poco
{
    public class ClassRendererV2
    {
        public string Render(ClassTemplate template)
        {
            var @namespace = SyntaxFactory
                .NamespaceDeclaration(SyntaxFactory.ParseName(template.Namespace))
                .NormalizeWhitespace()
                .AddUsings(SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("System")));

            var classDeclaration = SyntaxFactory
                .ClassDeclaration(template.TypeName)
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword));
            
            foreach (var property in template.Properties)
            {
                var propertyDeclaration = SyntaxFactory
                    .PropertyDeclaration(SyntaxFactory.ParseName(property.DataType), property.LegalCsharpName)
                    .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                    .AddAccessorListAccessors(
                        SyntaxFactory
                            .AccessorDeclaration(SyntaxKind.GetAccessorDeclaration)
                            .WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken)),
                        SyntaxFactory
                            .AccessorDeclaration(SyntaxKind.SetAccessorDeclaration)
                            .WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken)));

                classDeclaration = classDeclaration.AddMembers(propertyDeclaration);
            }

            @namespace = @namespace.AddMembers(classDeclaration);

            return @namespace.NormalizeWhitespace().ToFullString();
        }
    }
}