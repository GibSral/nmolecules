using System;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace NMolecules.Analyzers.ValueObjectAnalyzers
{
    public static class PropertyAnalyzer
    {
        public static void AnalyzeProperty(SymbolAnalysisContext context, Action<ISymbol> emitEntityViolation, Action<ISymbol> emitImmutabilityViolation)
        {
            var propertySymbol = (IPropertySymbol)context.Symbol;
            var classType = propertySymbol.ContainingType;
            if (classType.IsValueObject())
            {
                EnsureThatPropertyIsReadonly(propertySymbol, emitImmutabilityViolation);
                EnsureThatPropertyIsNotOfAnEntityType(propertySymbol, emitEntityViolation);
            }
        }

        private static void EnsureThatPropertyIsNotOfAnEntityType(IPropertySymbol propertySymbol, Action<ISymbol> emitEntityViolation)
        {
            if (propertySymbol.IsEntity())
            {
                emitEntityViolation(propertySymbol);
            }
        }

        private static void EnsureThatPropertyIsReadonly(IPropertySymbol propertySymbol, Action<ISymbol> emitImmutabilityViolation)
        {
            if (!propertySymbol.IsReadOnly)
            {
                emitImmutabilityViolation(propertySymbol);
            }
        }
    }
}