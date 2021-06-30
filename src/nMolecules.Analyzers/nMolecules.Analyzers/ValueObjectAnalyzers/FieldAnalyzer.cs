using System;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace NMolecules.Analyzers.ValueObjectAnalyzers
{
    public static class FieldAnalyzer
    {
        public static void AnalyzeField(SymbolAnalysisContext context)
        {
            var fieldSymbol = (IFieldSymbol)context.Symbol;
            EnsureFieldIsReadonly(context, fieldSymbol);
            EnsureFieldIsNoEntity(context, fieldSymbol);
        }

        private static void EnsureFieldIsNoEntity(SymbolAnalysisContext context, IFieldSymbol fieldSymbol)
        {
            if (fieldSymbol.Type.IsEntity())
            {
                context.ReportDiagnostic(fieldSymbol.ViolatesEntityUsage());
            }
        }

        private static void EnsureFieldIsReadonly(SymbolAnalysisContext context, IFieldSymbol fieldSymbol)
        {
            if (!fieldSymbol.IsReadOnly)
            {
                context.ReportDiagnostic(fieldSymbol.ViolatesImmutability());
            }
        }
    }
}