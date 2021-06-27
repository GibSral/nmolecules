using System.Threading.Tasks;
using NMolecules.Analyzers.ValueObjectAnalyzers;
using Xunit;
using static Microsoft.CodeAnalysis.Testing.DiagnosticResult;
using VerifyCS = NMolecules.Analyzers.Test.Verifiers.CSharpAnalyzerVerifier<NMolecules.Analyzers.ValueObjectAnalyzers.ValueObjectAnalyzer>;

namespace NMolecules.Analyzers.Test.ValueObjectAnalyzerTests
{
    public class ValueObjectUsesEntity
    {
        [Fact]
        public async Task AnalyzeEntityUsage_WithValueObjectUsesEntity_EmitsCompilerError()
        {
            var (testCode, offset) = SampleDataLoader.LoadFromNamespaceOf<ValueObjectUsesEntity>("ValueObjectUsesEntity.cs");
            var entityFieldLineNumber = offset + 14;
            var ctorLineNumber = offset + 15;
            var propertyLineNumber = offset + 20;
            var methodLineNumber = offset + 22;

            var entityAsField = CompilerError(ValueObjectAnalyzer.NoEntitiesInValueObjectsId).WithSpan(entityFieldLineNumber, 37, entityFieldLineNumber, 43);
            var entityAsParameterInCtor = CompilerError(ValueObjectAnalyzer.NoEntitiesInValueObjectsId).WithSpan(ctorLineNumber, 46, ctorLineNumber, 51);
            var entityAsProperty = CompilerError(ValueObjectAnalyzer.NoEntitiesInValueObjectsId).WithSpan(propertyLineNumber, 27, propertyLineNumber, 32);
            var entityAsReturnValue = CompilerError(ValueObjectAnalyzer.NoEntitiesInValueObjectsId).WithSpan(methodLineNumber, 27, methodLineNumber, 38);
            var entityAsParameterInMethod = CompilerError(ValueObjectAnalyzer.NoEntitiesInValueObjectsId)
                .WithSpan(methodLineNumber, 50, methodLineNumber, 56);
            await VerifyCS.VerifyAnalyzerAsync(testCode, entityAsField, entityAsParameterInCtor, entityAsProperty, entityAsReturnValue, entityAsParameterInMethod);
        }
    }
}