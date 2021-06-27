﻿using System.Threading.Tasks;
using NMolecules.Analyzers.ValueObjectAnalyzers;
using Xunit;
using static Microsoft.CodeAnalysis.Testing.DiagnosticResult;
using VerifyCS = NMolecules.Analyzers.Test.Verifiers.CSharpAnalyzerVerifier<NMolecules.Analyzers.ValueObjectAnalyzers.ValueObjectAnalyzer>;

namespace NMolecules.Analyzers.Test.ValueObjectAnalyzerTests
{
    public class ClassRequirements
    {

        [Fact]
        public async Task AnalyzeClass_WithValueObjectIsNotSealed_EmitsCompilerError()
        {
            var (testCode, offset) = SampleDataLoader.LoadFromNamespaceOf<ClassRequirements>("ValueObjectNotSealed.cs");
            var lineNumber = offset + 7;

            var expectedCompilerError = CompilerError(ClassTypeAnalyzer.ValueObjectsMustBeSealedId).WithSpan(lineNumber, 18, lineNumber, 38);
            await VerifyCS.VerifyAnalyzerAsync(testCode, expectedCompilerError);
        }
    }
}