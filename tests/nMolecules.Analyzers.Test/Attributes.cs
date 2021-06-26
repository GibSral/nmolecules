using System;
namespace NMolecules.DDD
{
    [AttributeUsage(AttributeTargets.Class)]
    [Entity]
    public class AggregateRootAttribute : EntityAttribute
    {
    }

    [AttributeUsage(
        AttributeTargets.Assembly |
        AttributeTargets.Module)]
    public class BoundedContextAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class EntityAttribute : Attribute
    {
    }

    [AttributeUsage(
        AttributeTargets.Class |
        AttributeTargets.Interface)]
    public class FactoryAttribute : Attribute
    {
    }

    [AttributeUsage(
        AttributeTargets.Assembly |
        AttributeTargets.Module)]
    public class ModuleAttribute : Attribute
    {
    }

    [AttributeUsage(
        AttributeTargets.Class |
        AttributeTargets.Interface)]
    public class RepositoryAttribute : Attribute
    {
    }

    [AttributeUsage(
        AttributeTargets.Class |
        AttributeTargets.Interface)]
    public class ServiceAttribute : Attribute
    {
    }

    [AttributeUsage(
        AttributeTargets.Class |
        AttributeTargets.Struct)]
    public class ValueObjectAttribute : Attribute
    {
    }
}
