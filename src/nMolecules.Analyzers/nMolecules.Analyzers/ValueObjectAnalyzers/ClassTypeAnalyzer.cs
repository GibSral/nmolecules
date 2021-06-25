﻿using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace NMolecules.Analyzers.ValueObjectAnalyzers
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class ClassTypeAnalyzer : DiagnosticAnalyzer
    {
        private const string Category = "Design";
        public const string ValueObjectsMustImplementIEquatableId = nameof(ValueObjectsMustImplementIEquatableId);
        public const string ValueObjectsMustBeSealedId = nameof(ValueObjectsMustBeSealedId);

        private static readonly DiagnosticDescriptor ValueObjectMustImplementIEquatable = new(ValueObjectsMustImplementIEquatableId,
            new LocalizableResourceString(nameof(Resources.ValueObjectMustImplementIEquatableTitle), Resources.ResourceManager, typeof(Resources)),
            new LocalizableResourceString(nameof(Resources.ValueObjectMustImplementIEquatableMessageFormat), Resources.ResourceManager, typeof(Resources)),
           Category,
            DiagnosticSeverity.Error,
            true,
            new LocalizableResourceString(nameof(Resources.ValueObjectMustImplementIEquatableDescription), Resources.ResourceManager, typeof(Resources)));

        private static readonly DiagnosticDescriptor ValueObjectMustBeSealed = new(ValueObjectsMustBeSealedId,
            new LocalizableResourceString(nameof(Resources.ValueObjectMustBeSealedTitle), Resources.ResourceManager, typeof(Resources)),
            new LocalizableResourceString(nameof(Resources.ValueObjectMustBeSealedMessageFormat), Resources.ResourceManager, typeof(Resources)),
            Category,
            DiagnosticSeverity.Error,
            true,
            new LocalizableResourceString(nameof(Resources.ValueObjectMustBeSealedDescription), Resources.ResourceManager, typeof(Resources)));

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(ValueObjectMustImplementIEquatable, ValueObjectMustBeSealed);

        public override void Initialize(AnalysisContext context)
        {
            context.EnableConcurrentExecution();
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.ReportDiagnostics);
            context.RegisterSymbolAction(AnalyzeType, SymbolKind.NamedType);
        }

        private static void AnalyzeType(SymbolAnalysisContext context)
        {
            var namedTypeSymbol = (INamedTypeSymbol)context.Symbol;
            if (namedTypeSymbol.IsValueObject())
            {
                EnsureValueObjectIsSealed(context, namedTypeSymbol);
                EnsureValueObjectImplementsIEquatable(context, namedTypeSymbol);
            }
        }

        private static void EnsureValueObjectIsSealed(SymbolAnalysisContext context, INamedTypeSymbol namedTypeSymbol)
        {
            if (!namedTypeSymbol.IsSealed)
            {
                EmitSealedViolation(context, namedTypeSymbol);
            }
        }

        private static void EnsureValueObjectImplementsIEquatable(SymbolAnalysisContext context, INamedTypeSymbol namedTypeSymbol)
        {
            var implementsIEquatable = namedTypeSymbol.AllInterfaces.Any(it =>
            {
                var implements = it.Name.Equals("IEquatable");
                implements &= it.TypeArguments.Any(tp => tp.Name.Equals(namedTypeSymbol.Name) && SymbolEqualityComparer.Default.Equals(tp.ContainingNamespace, namedTypeSymbol.ContainingNamespace));
                return implements;
            });

            if (!implementsIEquatable)
            {
                EmitIEquatableViolation(context, namedTypeSymbol);
            }
        }


        private static void EmitIEquatableViolation(SymbolAnalysisContext context, INamedTypeSymbol symbol) =>
            context.EmitViolation(symbol, ValueObjectMustImplementIEquatable, symbol.Name);

        private static void EmitSealedViolation(SymbolAnalysisContext context, INamedTypeSymbol symbol) => context.EmitViolation(symbol, ValueObjectMustBeSealed);
    }
}