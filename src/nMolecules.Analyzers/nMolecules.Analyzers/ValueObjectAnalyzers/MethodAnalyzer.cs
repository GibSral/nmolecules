using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace NMolecules.Analyzers.ValueObjectAnalyzers
{
    public static class MethodAnalyzer
    {
        public static void AnalyzeMethod(SymbolAnalysisContext context)
        {
            var method = (IMethodSymbol) context.Symbol;
            if (IsProperty(method)) return;

            EnsureThatEntitiesAreNotUsedAsParameters(context, method);
            EnsureThatEntityIsNotUsedAsReturnValue(context, method);
        }

        private static bool IsProperty(IMethodSymbol method)
        {
            return method.MethodKind == MethodKind.PropertyGet || method.MethodKind == MethodKind.PropertySet;
        }

        private static void EnsureThatEntityIsNotUsedAsReturnValue(SymbolAnalysisContext context, IMethodSymbol method)
        {
            if (!method.ReturnsVoid)
                if (method.ReturnType.IsEntity())
                    context.ReportDiagnostic(method.ViolatesEntityUsage());
        }

        private static void EnsureThatEntitiesAreNotUsedAsParameters(SymbolAnalysisContext context,
            IMethodSymbol method)
        {
            foreach (var parameter in method.Parameters)
            {
                var parameterType = parameter.Type;
                if (parameterType.IsEntity()) context.ReportDiagnostic(parameter.ViolatesEntityUsage());
            }
        }

        public static void AnalyzeDeclarations(SyntaxNodeAnalysisContext context)
        {
            var localDeclaration = (LocalDeclarationStatementSyntax) context.Node;
            var variable = localDeclaration.Declaration.Variables.Single();
            var declaredSymbol = (ILocalSymbol) context.SemanticModel.GetDeclaredSymbol(variable)!;
            if (declaredSymbol.Type.IsEntity()) context.ReportDiagnostic(declaredSymbol.ViolatesEntityUsage());
        }
    }
}