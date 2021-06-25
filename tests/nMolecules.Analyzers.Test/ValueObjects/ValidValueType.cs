using NMolecules.DDD;

namespace NMolecules.Analyzers.Test.ValueObjects
{
    [ValueObject]
    public class ValidValueType
    {
        public ValidValueType(string value)
        {
            Value = value;
        }

        public string Value { get; }
    }
}
