using System.Threading.Tasks;
using NMolecules.Analyzers.ValueObjectAnalyzers;
using Xunit;
using static Microsoft.CodeAnalysis.Testing.DiagnosticResult;
using VerifyCS = NMolecules.Analyzers.Test.Verifiers.CSharpAnalyzerVerifier<NMolecules.Analyzers.ValueObjectAnalyzers.ValueObjectAnalyzer>;

namespace NMolecules.Analyzers.Test.ValueObjectAnalyzerTests
{
    public class Immutablity
    {
        [Fact]
        public async Task AnalyzeImmutability_WithValueObjectIsImmutable_IsValid()
        {
            var (testCode, _) = SampleDataLoader.LoadFromNamespaceOf<Immutablity>("ValidValueObject.cs");

            await VerifyCS.VerifyAnalyzerAsync(testCode, EmptyDiagnosticResults);
        }
        
        [Fact]
        public async Task AnalyzeImmutability_WithPropertyHasPublicSetter_EmitsCompilerError()
        {
            var (testCode, offset) = SampleDataLoader.LoadFromNamespaceOf<Immutablity>("ValueObjectWithPublicPropertyGetter.cs");
            var lineNumber = offset + 14;

            var expectedCompilerError = CompilerError(Diagnostics.ValueObjectsMustBeImmutableId).WithSpan(lineNumber, 23, lineNumber, 28);
            await VerifyCS.VerifyAnalyzerAsync(testCode, expectedCompilerError);
        }

        [Fact]
        public async Task AnalyzeImmutability_WithFieldIsNotReadonly_EmitsCompilerError()
        {
            var (testCode, offset) = SampleDataLoader.LoadFromNamespaceOf<Immutablity>("ValueObjectWithFiledNotReadonly.cs");
            var lineNumber = offset + 9;

            var expectedCompilerError = CompilerError(Diagnostics.ValueObjectsMustBeImmutableId).WithSpan(lineNumber, 24, lineNumber, 29);
            await VerifyCS.VerifyAnalyzerAsync(testCode, expectedCompilerError);
        }
    }
}
