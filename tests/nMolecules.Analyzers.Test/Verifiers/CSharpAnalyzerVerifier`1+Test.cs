using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Testing;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Testing;
using Microsoft.CodeAnalysis.Testing.Verifiers;

namespace NMolecules.Analyzers.Test.Verifiers
{
    public static partial class CSharpAnalyzerVerifier<TAnalyzer>
        where TAnalyzer : DiagnosticAnalyzer, new()
    {
        public class Test : CSharpAnalyzerTest<TAnalyzer, XUnitVerifier>
        {
            public Test()
            {
                ReferenceAssemblies = ReferenceAssemblies.WithAssemblies(ImmutableArray.Create("nMolecules.DDD"));
                SolutionTransforms.Add((solution, projectId) =>
                {
                    var attributes = SampleDataLoader.GetAttributes();
                    var addDocument = solution.AddDocument(DocumentId.CreateNewId(projectId, "/0/attributes.cs"), "/0/attributes.cs",
                        attributes);
                    var project = addDocument.GetProject(projectId)!;
                    return addDocument;
                });
                SolutionTransforms.Add((solution, projectId) =>
                {
                    var project = solution.GetProject(projectId)!;

                    var compilationOptions = project.CompilationOptions!;
                    compilationOptions = compilationOptions.WithSpecificDiagnosticOptions(
                        compilationOptions.SpecificDiagnosticOptions.SetItems(CSharpVerifierHelper.NullableWarnings));
                    solution = solution.WithProjectCompilationOptions(projectId, compilationOptions);

                    return solution;
                });
            }
        }
    }
}
