using System.Threading.Tasks;
using NMolecules.Analyzers.ValueObjectAnalyzers;
using Xunit;
using static Microsoft.CodeAnalysis.Testing.DiagnosticResult;
using VerifyCS = NMolecules.Analyzers.Test.Verifiers.CSharpAnalyzerVerifier<
    NMolecules.Analyzers.ValueObjectAnalyzers.ValueObjectAnalyzer>;

namespace NMolecules.Analyzers.Test.ValueObjectAnalyzerTests
{
    public class ValueObjectUsersService
    {
        [Fact]
        public async Task AnalyzeEntityUsage_WithValueObjectUsesEntity_EmitsCompilerError()
        {
            var testCode = SampleDataLoader.LoadFromNamespaceOf<ValueObjectUsesEntity>("ValueObjectUsesService.cs");
            const int entityFieldLineNumber = 14;
            const int ctorLineNumber = 15;
            const int propertyLineNumber = 20;
            const int methodLineNumber = 22;
            const int entityInMethodBodyLineNumber = 24;

            var entityAsField = CompilerError(Diagnostics.NoServicesInValueObjectsId)
                .WithSpan(entityFieldLineNumber, 38, entityFieldLineNumber, 45);
            var entityAsParameterInCtor = CompilerError(Diagnostics.NoServicesInValueObjectsId)
                .WithSpan(ctorLineNumber, 47, ctorLineNumber, 52);
            var entityAsProperty = CompilerError(Diagnostics.NoServicesInValueObjectsId)
                .WithSpan(propertyLineNumber, 28, propertyLineNumber, 33);
            var entityAsReturnValue = CompilerError(Diagnostics.NoServicesInValueObjectsId)
                .WithSpan(methodLineNumber, 28, methodLineNumber, 50);
            var entityAsParameterInMethod = CompilerError(Diagnostics.NoServicesInValueObjectsId)
                .WithSpan(methodLineNumber, 63, methodLineNumber, 70);
            var entityUsedInMethodBody = CompilerError(Diagnostics.NoServicesInValueObjectsId)
                .WithSpan(entityInMethodBodyLineNumber, 17, entityInMethodBodyLineNumber, 28);
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