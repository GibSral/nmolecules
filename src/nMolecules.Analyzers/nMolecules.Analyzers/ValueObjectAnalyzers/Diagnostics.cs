using System;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace NMolecules.Analyzers.ValueObjectAnalyzers
{
    public static class Diagnostics
    {
        public const string ValueObjectsMustImplementIEquatableId = nameof(ValueObjectsMustImplementIEquatableId);
        public const string ValueObjectsMustBeSealedId = nameof(ValueObjectsMustBeSealedId);
        public const string NoEntitiesInValueObjectsId = nameof(NoEntitiesInValueObjectsId);
        public const string NoServicesInValueObjectsId = nameof(NoServicesInValueObjectsId);
        public const string NoFactoriesInValueObjectsId = nameof(NoFactoriesInValueObjectsId);
        public const string ValueObjectsMustBeImmutableId = nameof(ValueObjectsMustBeImmutableId);
        private const string Category = "Design";

        public static readonly DiagnosticDescriptor ValueObjectMustNotUseEntityRule = new(NoEntitiesInValueObjectsId,
            new LocalizableResourceString(nameof(Resources.ValueObjectUsesEntityTitle), Resources.ResourceManager,
                typeof(Resources)),
            new LocalizableResourceString(nameof(Resources.ValueObjectUsesEntityMessageFormat),
                Resources.ResourceManager, typeof(Resources)), Category,
            DiagnosticSeverity.Error,
            true,
            new LocalizableResourceString(nameof(Resources.ValueObjectUsesEntityDescription), Resources.ResourceManager,
                typeof(Resources)));

        public static readonly DiagnosticDescriptor ValueObjectMustNotUseServiceRule = new(NoServicesInValueObjectsId,
            new LocalizableResourceString(nameof(Resources.ValueObjectUsesServiceTitle), Resources.ResourceManager,
                typeof(Resources)),
            new LocalizableResourceString(nameof(Resources.ValueObjectUsesServiceMessageFormat),
                Resources.ResourceManager, typeof(Resources)), Category,
            DiagnosticSeverity.Error,
            true,
            new LocalizableResourceString(nameof(Resources.ValueObjectUsesServiceDescription),
                Resources.ResourceManager,
                typeof(Resources)));

        public static readonly DiagnosticDescriptor ValueObjectMustNotUseFactoryRule = new(NoFactoriesInValueObjectsId,
            new LocalizableResourceString(nameof(Resources.ValueObjectUsesFactoryTitle), Resources.ResourceManager,
                typeof(Resources)),
            new LocalizableResourceString(nameof(Resources.ValueObjectUsesFactoryMessageFormat),
                Resources.ResourceManager, typeof(Resources)), Category,
            DiagnosticSeverity.Error,
            true,
            new LocalizableResourceString(nameof(Resources.ValueObjectUsesFactoryDescription),
                Resources.ResourceManager,
                typeof(Resources)));

        public static readonly DiagnosticDescriptor ValueObjectMustBeImmutable = new(ValueObjectsMustBeImmutableId,
            new LocalizableResourceString(nameof(Resources.ValueObjectMustBeImmutableTitle), Resources.ResourceManager,
                typeof(Resources)),
            new LocalizableResourceString(nameof(Resources.ValueObjectMustBeImmutableMessageFormat),
                Resources.ResourceManager, typeof(Resources)), Category,
            DiagnosticSeverity.Error,
            true,
            new LocalizableResourceString(nameof(Resources.ValueObjectMustBeImmutableDescription),
                Resources.ResourceManager, typeof(Resources)));


        public static readonly DiagnosticDescriptor ValueObjectMustImplementIEquatable = new(
            ValueObjectsMustImplementIEquatableId,
            new LocalizableResourceString(nameof(Resources.ValueObjectMustImplementIEquatableTitle),
                Resources.ResourceManager, typeof(Resources)),
            new LocalizableResourceString(nameof(Resources.ValueObjectMustImplementIEquatableMessageFormat),
                Resources.ResourceManager, typeof(Resources)),
            Category,
            DiagnosticSeverity.Error,
            true,
            new LocalizableResourceString(nameof(Resources.ValueObjectMustImplementIEquatableDescription),
                Resources.ResourceManager, typeof(Resources)));

        public static readonly DiagnosticDescriptor ValueObjectMustBeSealed = new(ValueObjectsMustBeSealedId,
            new LocalizableResourceString(nameof(Resources.ValueObjectMustBeSealedTitle), Resources.ResourceManager,
                typeof(Resources)),
            new LocalizableResourceString(nameof(Resources.ValueObjectMustBeSealedMessageFormat),
                Resources.ResourceManager, typeof(Resources)),
            Category,
            DiagnosticSeverity.Error,
            true,
            new LocalizableResourceString(nameof(Resources.ValueObjectMustBeSealedDescription),
                Resources.ResourceManager, typeof(Resources)));

        public static Diagnostic ViolatesImmutability(this ISymbol symbol)
        {
            return symbol.Diagnostic(ValueObjectMustBeImmutable);
        }

        public static Diagnostic ViolatesEntityUsage(this ISymbol symbol)
        {
            return symbol.Diagnostic(ValueObjectMustNotUseEntityRule);
        }

        public static Diagnostic ViolatesServiceUsage(this ISymbol symbol)
        {
            return symbol.Diagnostic(ValueObjectMustNotUseServiceRule);
        }

        public static Diagnostic ViolatesFactoryUsage(this ISymbol symbol)
        {
            return symbol.Diagnostic(ValueObjectMustNotUseFactoryRule);
        }

        public static Diagnostic DoesNotImplementIEquatable(this ISymbol symbol)
        {
            return symbol.Diagnostic(ValueObjectMustImplementIEquatable);
        }

        public static Diagnostic IsNotSealed(this ISymbol symbol)
        {
            return symbol.Diagnostic(ValueObjectMustBeSealed);
        }

        public static void EnsureTypeIsAllowed(SymbolAnalysisContext context, ISymbol symbol, ITypeSymbol type)
        {
            EnsureTypeIsAllowed(context.ReportDiagnostic, symbol, type);
        }


        public static void EnsureTypeIsAllowed(SyntaxNodeAnalysisContext context, ISymbol symbol, ITypeSymbol type)
        {
            EnsureTypeIsAllowed(context.ReportDiagnostic, symbol, type);
        }

        private static void EnsureTypeIsAllowed(Action<Diagnostic> reportDiagnostic, ISymbol symbol, ITypeSymbol type)
        {
            if (type.IsEntity())
                reportDiagnostic(symbol.ViolatesEntityUsage());

            if (type.IsService())
                reportDiagnostic(symbol.ViolatesServiceUsage());

            if (type.IsFactory())
                reportDiagnostic(symbol.ViolatesFactoryUsage());
        }
    }
}