namespace NMolecules.Analyzers.Test.ValueObjectAnalyzerTests.SampleData
{
    using System;
    using NMolecules.DDD;
    
    [ValueObject]
    public sealed class ValidValueObject : IEquatable<ValidValueObject>
    {
        public ValidValueObject(string value)
        {
            Value = value;
        }
        
        public string Value { get; set; }
        
        public bool Equals(ValidValueObject other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Value == other.Value;
        }
        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is ValidValueObject other && Equals(other);
        }
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}