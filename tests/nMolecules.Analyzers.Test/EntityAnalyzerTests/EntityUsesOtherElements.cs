using System.Collections.Generic;
using System.Threading.Tasks;
using NMolecules.Analyzers.EntityAnalyzers;
using NMolecules.Analyzers.Test.EntityAnalyzerTests.SampleData;
using Xunit;
using static Microsoft.CodeAnalysis.Testing.DiagnosticResult;
using VerifyCS = NMolecules.Analyzers.Test.Verifiers.CSharpAnalyzerVerifier<
    NMolecules.Analyzers.ValueObjectAnalyzers.ValueObjectAnalyzer>;

namespace NMolecules.Analyzers.Test.EntityAnalyzerTests
{
    public class EntityUsesOtherElements
    {
        private const int EntityFieldLineNumber = 14;
        private const int CtorLineNumber = 15;
        private const int PropertyLineNumber = 20;
        private const int MethodLineNumber = 22;
        private const int EntityInMethodBodyLineNumber = 24;

        [Fact]
        public async Task Analyze_WithEntityUsesRepository_EmitsCompilerError()
        {
            var entity = GenerateClass("Repository");
            var repositoryAsField = CompilerError(Rules.EntitiesMustNotUseRepositoriesId).WithSpan(14, 1, 14, 5);
            await VerifyCS.VerifyAnalyzerAsync(entity, repositoryAsField);
        }

        private static string GenerateClass(string type)
        {
            var invalidUsageTemplate = new InvalidUsageTemplate
            {
                Session = new Dictionary<string, object> {{"type", type}, {"name", type.ToLowerInvariant()}}
            };
            return invalidUsageTemplate.TransformText();
        }
    }
}