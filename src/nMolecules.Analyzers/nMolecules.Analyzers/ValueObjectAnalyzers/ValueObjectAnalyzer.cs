using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;
using static NMolecules.Analyzers.ValueObjectAnalyzers.PropertyAnalyzer;
using static NMolecules.Analyzers.ValueObjectAnalyzers.MethodAnalyzer;
using static NMolecules.Analyzers.ValueObjectAnalyzers.FieldAnalyzer;
using static NMolecules.Analyzers.ValueObjectAnalyzers.ClassSymbolAnalyzer;
using static NMolecules.Analyzers.ValueObjectAnalyzers.Diagnostics;

namespace NMolecules.Analyzers.ValueObjectAnalyzers
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class ValueObjectAnalyzer : DiagnosticAnalyzer
    {
        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics
            => ImmutableArray.Create(ValueObjectMustNotUseEntityRule,
                ValueObjectMustNotUseServiceRule,
                ValueObjectMustNotUseFactoryRule,
                ValueObjectMustNotUseRepositoryRule,
                ValueObjectMustNotUseAggregateRootRule,
                ValueObjectMustBeImmutable,
                ValueObjectMustImplementIEquatable, ValueObjectMustBeSealed);

        public override void Initialize(AnalysisContext context)
        {
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze |
                                                   GeneratedCodeAnalysisFlags.ReportDiagnostics);
            context.EnableConcurrentExecution();
            context.RegisterSymbolActionForValueObject(AnalyzeMethod, SymbolKind.Method);
            context.RegisterSymbolActionForValueObject(AnalyzeProperty, SymbolKind.Property);
            context.RegisterSymbolActionForValueObject(AnalyzeType, SymbolKind.NamedType);
            context.RegisterSymbolActionForValueObject(AnalyzeField, SymbolKind.Field);
            context.RegisterSyntaxNodeActionForValueObject(AnalyzeDeclarations, SyntaxKind.LocalDeclarationStatement);
        }
    }
}