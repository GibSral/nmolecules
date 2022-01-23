using System.Collections.Generic;
using System.Threading.Tasks;
using NMolecules.Analyzers.Test.ValueObjectAnalyzerTests.SampleData;
using NMolecules.Analyzers.ValueObjectAnalyzers;
using Xunit;
using static Microsoft.CodeAnalysis.Testing.DiagnosticResult;
using VerifyCS = NMolecules.Analyzers.Test.Verifiers.CSharpAnalyzerVerifier<
    NMolecules.Analyzers.ValueObjectAnalyzers.ValueObjectAnalyzer>;

namespace NMolecules.Analyzers.Test.ValueObjectAnalyzerTests
{
    public class ValueObjectUsesOtherElements
    {
        [Fact]
        public async Task Analyze_WithValueObjectUsesEntity_EmitsCompilerError()
        {
            var testCode = GenerateClass("Entity");
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

        [Fact]
        public async Task Analyze_WithValueObjectUsesService_EmitsCompilerError()
        {
            var testCode = GenerateClass("Service");
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
                .WithSpan(methodLineNumber, 28, methodLineNumber, 38);
            var entityAsParameterInMethod = CompilerError(Diagnostics.NoServicesInValueObjectsId)
                .WithSpan(methodLineNumber, 51, methodLineNumber, 58);
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

        [Fact]
        public async Task Analyze_WithValueObjectUsesFactory_EmitsCompilerError()
        {
            var testCode = GenerateClass("Factory");
            const int entityFieldLineNumber = 14;
            const int ctorLineNumber = 15;
            const int propertyLineNumber = 20;
            const int methodLineNumber = 22;
            const int entityInMethodBodyLineNumber = 24;

            var entityAsField = CompilerError(Diagnostics.NoFactoriesInValueObjectsId)
                .WithSpan(entityFieldLineNumber, 38, entityFieldLineNumber, 45);
            var entityAsParameterInCtor = CompilerError(Diagnostics.NoFactoriesInValueObjectsId)
                .WithSpan(ctorLineNumber, 47, ctorLineNumber, 52);
            var entityAsProperty = CompilerError(Diagnostics.NoFactoriesInValueObjectsId)
                .WithSpan(propertyLineNumber, 28, propertyLineNumber, 33);
            var entityAsReturnValue = CompilerError(Diagnostics.NoFactoriesInValueObjectsId)
                .WithSpan(methodLineNumber, 28, methodLineNumber, 38);
            var entityAsParameterInMethod = CompilerError(Diagnostics.NoFactoriesInValueObjectsId)
                .WithSpan(methodLineNumber, 51, methodLineNumber, 58);
            var entityUsedInMethodBody = CompilerError(Diagnostics.NoFactoriesInValueObjectsId)
                .WithSpan(entityInMethodBodyLineNumber, 17, entityInMethodBodyLineNumber, 28);
            await VerifyCS.VerifyAnalyzerAsync(testCode,
                entityAsField,
                entityAsParameterInCtor,
                entityAsProperty,
                entityAsReturnValue,
                entityAsParameterInMethod,
                entityUsedInMethodBody);
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