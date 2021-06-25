using System.Threading.Tasks;
using Xunit;
using VerifyCS = NMolecules.Analyzers.Test.Verifiers.CSharpAnalyzerVerifier<NMolecules.Analyzers.ValueObjectAnalyzers.ValueObjectAnalyzer>;

namespace NMolecules.Analyzers.Test
{
    public class UnitTest 
    {
        //No diagnostics expected to show up
        [Fact]
        public async Task Test1()
        {
            var test = @"";

            await VerifyCS.VerifyAnalyzerAsync(test);
        }
    }
}