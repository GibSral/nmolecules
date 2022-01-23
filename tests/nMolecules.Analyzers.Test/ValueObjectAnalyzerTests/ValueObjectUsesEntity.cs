using System.Threading.Tasks;
using NMolecules.Analyzers.ValueObjectAnalyzers;
using Xunit;
using static Microsoft.CodeAnalysis.Testing.DiagnosticResult;
using VerifyCS = NMolecules.Analyzers.Test.Verifiers.CSharpAnalyzerVerifier<
    NMolecules.Analyzers.ValueObjectAnalyzers.ValueObjectAnalyzer>;

namespace NMolecules.Analyzers.Test.ValueObjectAnalyzerTests
{
    public class ValueObjectUsesEntity
    {
        [Fact]
        public async Task AnalyzeEntityUsage_WithValueObjectUsesEntity_EmitsCompilerError()
        {
            var testCode = SampleDataLoader.LoadFromNamespaceOf<ValueObjectUsesEntity>("ValueObjectUsesEntity.cs");
            const int entityFieldLineNumber = 14;
            const int ctorLineNumber = 15;
            const int propertyLineNumber = 20;
            const int methodLineNumber = 22;
            const int entityInMethodBodyLineNumber = 24;

            var entityAsField = CompilerError(Diagnostics.NoEntitiesInValueObjectsId)
                .WithSpan(entityFieldLineNumber, 37, entityFieldLineNumber, 43);
            var entityAsParameterInCtor = CompilerError(Diagnostics.NoEntitiesInValueObjectsId)
                .WithSpan(ctorLineNumber, 46, ctorLineNumber, 51);
            var entityAsProperty = CompilerError(Diagnostics.NoEntitiesInValueObjectsId)
                .WithSpan(propertyLineNumber, 27, propertyLineNumber, 32);
            var entityAsReturnValue = CompilerError(Diagnostics.NoEntitiesInValueObjectsId)
                .WithSpan(methodLineNumber, 27, methodLineNumber, 37);
            var entityAsParameterInMethod = CompilerError(Diagnostics.NoEntitiesInValueObjectsId)
                .WithSpan(methodLineNumber, 49, methodLineNumber, 55);
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