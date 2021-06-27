namespace NMolecules.Analyzers.Test.ValueObjectAnalyzerTests.SampleData
{
    using System;
    using NMolecules.DDD;

    [Entity]
    public class SomeEntity
    {
    }
    
    [ValueObject]
    public sealed class InvalidValueObject : IEquatable<InvalidValueObject>
    {
        private readonly SomeEntity entity;
        public InvalidValueObject(SomeEntity value)
        {
            Value = value;
        }
        
        public SomeEntity Value { get; }

        public SomeEntity AddToEntity(SomeEntity entity)
        {
            return new SomeEntity();
        }
        
        public bool Equals(InvalidValueObject other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Value == other.Value;
        }
        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is InvalidValueObject other && Equals(other);
        }
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}