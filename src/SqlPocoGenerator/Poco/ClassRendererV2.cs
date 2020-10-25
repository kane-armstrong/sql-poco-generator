using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace SqlPocoGenerator.Poco
{
    public class ClassRendererV2
    {
        public string Render(ClassTemplate template)
        {
            var syntaxFactory = SyntaxFactory.CompilationUnit()
                .AddUsings(SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("System")));

            var @namespace = SyntaxFactory
                .NamespaceDeclaration(SyntaxFactory.ParseName(template.Namespace))
                .NormalizeWhitespace();

            var classDeclaration = SyntaxFactory
                .ClassDeclaration(template.TypeName)
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword));
            
            foreach (var property in template.Properties)
            {
                var propertyDeclaration = SyntaxFactory
                    .PropertyDeclaration(SyntaxFactory.ParseTypeName(property.DataType),
                        SyntaxFactory.ParseToken(property.LegalCsharpName))
                    .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                    .AddAccessorListAccessors(
                        SyntaxFactory
                            .AccessorDeclaration(SyntaxKind.GetAccessorDeclaration)
                            .WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken)),
                        SyntaxFactory
                            .AccessorDeclaration(SyntaxKind.SetAccessorDeclaration)
                            .WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken)));

                classDeclaration = classDeclaration.AddMembers(propertyDeclaration.NormalizeWhitespace());
            }
            
            @namespace = @namespace.AddMembers(classDeclaration);

            syntaxFactory = syntaxFactory.AddMembers(@namespace);
            
            return syntaxFactory.NormalizeWhitespace().ToFullString();
        }
    }
}