using NMolecules.Analyzers.Test.Entities;
using NMolecules.DDD;

namespace NMolecules.Analyzers.Test.ValueObjects
{
    [ValueObject]
    public class InvalidValueType
    {
        public InvalidValueType(SomeEntity entity)
        {
            Entity = entity;
        }

        public SomeEntity Entity { get; }
    }
}
