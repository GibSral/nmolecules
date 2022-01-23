namespace NMolecules.Analyzers.Test.ValueObjectAnalyzerTests.SampleData
{
    using System;
    using NMolecules.DDD;

    [Service]
    public class SomeService
    {
    }
    
    [ValueObject]
    public sealed class InvalidValueObject : IEquatable<InvalidValueObject>
    {
        private readonly SomeService service;
        public InvalidValueObject(SomeService value)
        {
            Value = value;
        }
        
        public SomeService Value { get; }

        public SomeService SomeMethod(SomeService service)
        {
            var someService = new SomeService();
            return someService;
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