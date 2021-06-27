using System;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace NMolecules.Analyzers.ValueObjectAnalyzers
{
    public static class FieldAnalyzer
    {
        public static void AnalyzeField(SymbolAnalysisContext context, Action<ISymbol> emitImmutabilityViolation, Action<ISymbol> emitEntityViolation)
        {
            var fieldSymbol = (IFieldSymbol)context.Symbol;
            EnsureFieldIsReadonly(fieldSymbol, emitImmutabilityViolation);
            EnsureFieldIsNoEntity(fieldSymbol, emitEntityViolation);
        }

        private static void EnsureFieldIsNoEntity(IFieldSymbol fieldSymbol, Action<ISymbol> emitEntityViolation)
        {
            if (fieldSymbol.Type.IsEntity())
            {
                emitEntityViolation(fieldSymbol);
            }
        }

        private static void EnsureFieldIsReadonly(IFieldSymbol fieldSymbol, Action<ISymbol> emitImmutabilityViolation)
        {
            if (!fieldSymbol.IsReadOnly)
            {
                emitImmutabilityViolation(fieldSymbol);
            }
        }
    }
}