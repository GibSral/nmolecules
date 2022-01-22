using System.Threading.Tasks;
using NMolecules.Analyzers.ValueObjectAnalyzers;
using Xunit;
using static Microsoft.CodeAnalysis.Testing.DiagnosticResult;
using VerifyCS =
    NMolecules.Analyzers.Test.Verifiers.CSharpAnalyzerVerifier<
        NMolecules.Analyzers.ValueObjectAnalyzers.ValueObjectAnalyzer>;

namespace NMolecules.Analyzers.Test.ValueObjectAnalyzerTests
{
    public class ValueObjectUsesEntity
    {
        [Fact]
        public async Task AnalyzeEntityUsage_WithValueObjectUsesEntity_EmitsCompilerError()
        {
            var (testCode, offset) =
                SampleDataLoader.LoadFromNamespaceOf<ValueObjectUsesEntity>("ValueObjectUsesEntity.cs");
            var entityFieldLineNumber = offset + 14;
            var ctorLineNumber = offset + 15;
            var propertyLineNumber = offset + 20;
            var methodLineNumber = offset + 22;
            var entityInMethodBodyLineNumber = offset + 24;

            var entityAsField = CompilerError(Diagnostics.NoEntitiesInValueObjectsId)
                .WithSpan(entityFieldLineNumber, 37, entityFieldLineNumber, 43);
            var entityAsParameterInCtor = CompilerError(Diagnostics.NoEntitiesInValueObjectsId)
                .WithSpan(ctorLineNumber, 46, ctorLineNumber, 51);
            var entityAsProperty = CompilerError(Diagnostics.NoEntitiesInValueObjectsId)
                .WithSpan(propertyLineNumber, 27, propertyLineNumber, 32);
            var entityAsReturnValue = CompilerError(Diagnostics.NoEntitiesInValueObjectsId)
                .WithSpan(methodLineNumber, 27, methodLineNumber, 38);
            var entityAsParameterInMethod = CompilerError(Diagnostics.NoEntitiesInValueObjectsId)
                .WithSpan(methodLineNumber, 50, methodLineNumber, 56);
            var entityUsedInMethodBody = CompilerError(Diagnostics.NoEntitiesInValueObjectsId)
                .WithSpan(entityInMethodBodyLineNumber, 17, entityInMethodBodyLineNumber, 27);
            await VerifyCS.VerifyAnalyzerAsync(testCode,
                entityAsField,
                entityAsParameterInCtor,
                entityAsProperty,
                entityAsReturnValue,
                entityAsParameterInMethod,
                entityUsedInMethodBody);
        }
    }
}