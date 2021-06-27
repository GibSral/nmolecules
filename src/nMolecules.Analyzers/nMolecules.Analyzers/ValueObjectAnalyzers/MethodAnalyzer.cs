﻿using System;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace NMolecules.Analyzers.ValueObjectAnalyzers
{
    public static class MethodAnalyzer
    {
        public static void AnalyzeMethod(SymbolAnalysisContext context, Action<ISymbol> emitEntityViolation)
        {
            var method = (IMethodSymbol)context.Symbol;
            if (IsProperty(method))
            {
                return;
            }

            EnsureThatEntitiesAreNotUsedAsParameters(method, emitEntityViolation);
            EnsureThatEntityIsNotUsedAsReturnValue(method, emitEntityViolation);
        }

        private static bool IsProperty(IMethodSymbol method) => method.MethodKind == MethodKind.PropertyGet || method.MethodKind == MethodKind.PropertySet;

        private static void EnsureThatEntityIsNotUsedAsReturnValue(IMethodSymbol method, Action<ISymbol> emitEntityViolation)
        {
            if (!method.ReturnsVoid)
            {
                if (method.ReturnType.IsEntity())
                {
                    emitEntityViolation(method);
                }
            }
        }

        private static void EnsureThatEntitiesAreNotUsedAsParameters(IMethodSymbol method, Action<ISymbol> emitEntityViolation)
        {
            foreach (var parameter in method.Parameters)
            {
                var parameterType = parameter.Type;
                if (parameterType.IsEntity())
                {
                    emitEntityViolation(parameter);
                }
            }
        }
    }
}