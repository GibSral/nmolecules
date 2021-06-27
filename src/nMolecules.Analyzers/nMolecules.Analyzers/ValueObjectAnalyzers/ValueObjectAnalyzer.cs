using System;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using static NMolecules.Analyzers.ValueObjectAnalyzers.PropertyAnalyzer;
using static NMolecules.Analyzers.ValueObjectAnalyzers.MethodAnalyzer;
using static NMolecules.Analyzers.ValueObjectAnalyzers.FieldAnalyzer;
using static NMolecules.Analyzers.ValueObjectAnalyzers.ClassTypeAnalyzer;

namespace NMolecules.Analyzers.ValueObjectAnalyzers
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class ValueObjectAnalyzer : DiagnosticAnalyzer
    {
        public const string NoEntitiesInValueObjectsId = nameof(NoEntitiesInValueObjectsId);
        public const string ValueObjectsMustBeImmutableId = nameof(ValueObjectsMustBeImmutableId);
        private const string Category = "Design";

        private static readonly DiagnosticDescriptor ValueObjectMustNotUseEntityRule = new(NoEntitiesInValueObjectsId,
            new LocalizableResourceString(nameof(Resources.ValueObjectUsesEntityTitle), Resources.ResourceManager, typeof(Resources)),
            new LocalizableResourceString(nameof(Resources.ValueObjectUsesEntityMessageFormat), Resources.ResourceManager, typeof(Resources)),
            Category,
            DiagnosticSeverity.Error,
            true,
            new LocalizableResourceString(nameof(Resources.ValueObjectUsesEntityDescription), Resources.ResourceManager, typeof(Resources)));

        private static readonly DiagnosticDescriptor ValueObjectMustBeImmutable = new(ValueObjectsMustBeImmutableId,
            new LocalizableResourceString(nameof(Resources.ValueObjectMustBeImmutableTitle), Resources.ResourceManager, typeof(Resources)),
            new LocalizableResourceString(nameof(Resources.ValueObjectMustBeImmutableMessageFormat), Resources.ResourceManager, typeof(Resources)),
            Category,
            DiagnosticSeverity.Error,
            true,
            new LocalizableResourceString(nameof(Resources.ValueObjectMustBeImmutableDescription), Resources.ResourceManager, typeof(Resources)));


        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics
            => ImmutableArray.Create(ValueObjectMustNotUseEntityRule, ValueObjectMustBeImmutable, ValueObjectMustImplementIEquatable, ValueObjectMustBeSealed);

        public override void Initialize(AnalysisContext context)
        {
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.ReportDiagnostics);
            context.EnableConcurrentExecution();
            context.RegisterSymbolActionForValueObject(analysisContext => AnalyzeMethod(analysisContext, symbol => EmitEntityViolation(analysisContext, symbol)),
                SymbolKind.Method);

            context.RegisterSymbolActionForValueObject(
                    analysisContext => AnalyzeProperty(analysisContext,
                        symbol => EmitEntityViolation(analysisContext, symbol),
                        symbol => EmitImmutabilityViolation(analysisContext, symbol)),
                SymbolKind.Property);
            context.RegisterSymbolActionForValueObject(AnalyzeType, SymbolKind.NamedType);

            context.RegisterSymbolActionForValueObject(
                    analysisContext => AnalyzeField(analysisContext, symbol => EmitImmutabilityViolation(analysisContext, symbol)),
                SymbolKind.Field);
        }

        private static void EmitImmutabilityViolation(SymbolAnalysisContext context, ISymbol symbol)
        {
            EmitViolation(context, symbol, ValueObjectMustBeImmutable);
        }

        private static void EmitEntityViolation(SymbolAnalysisContext context, ISymbol symbol)
        {
            EmitViolation(context, symbol, ValueObjectMustNotUseEntityRule);
        }

        private static void EmitViolation(SymbolAnalysisContext context, ISymbol symbol, DiagnosticDescriptor descriptor, params object[] parameters)
        {
            var diagnostic = Diagnostic.Create(descriptor, symbol.Locations[0], parameters);
            context.ReportDiagnostic(diagnostic);
        }
    }
}