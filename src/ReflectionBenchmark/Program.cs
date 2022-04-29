using System.Reflection;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using ImmediateReflection;

namespace ReflectionBenchmark;

internal class Program
{
    private static void Main()
    {
        var summary = BenchmarkRunner.Run<TypeBenchmark>();
    }
}

public class TestType
{
    public int Id { get; set; }

    public string Name { get; set; }
}

public class TypeBenchmark
{
    private readonly Type _type = typeof(TestType);
    private readonly ImmediateType _typeAccessor = TypeAccessor.Get<TestType>();
    private readonly FastMember.TypeAccessor _fastTypeAccessor = FastMember.TypeAccessor.Create(typeof(TestType));

    private readonly PropertyInfo _propertyInfo = typeof(TestType).GetProperty("Id");
    private readonly ImmediateProperty _propertyAccessor = TypeAccessor.Get<TestType>().GetProperty("Id")!;
    private readonly FastMember.Member _fastPropertyAccessor = FastMember.TypeAccessor.Create(typeof(TestType)).GetMembers().Single(m => m.Name == "Id");
    private readonly TestType _testInstance = new TestType {Id = 100, Name = "Test"};

    [Benchmark]
    public PropertyInfo[] GetReflectionProperties()
    {
        return _type.GetProperties();
    }

    [Benchmark]
    public ImmediateProperty[] GetImmediateReflectionProperties()
    {
        return _typeAccessor.GetProperties().ToArray();
    }

    [Benchmark]
    public FastMember.Member[] GetFastMemberProperties()
    {
        return _fastTypeAccessor.GetMembers().ToArray();
    }

    [Benchmark]
    public object ReadReflectionIntProperty()
    {
        return _propertyInfo.GetValue(_testInstance, null);
    }

    [Benchmark]
    public object ReadImmediateReflectionIntProperty()
    {
        return _propertyAccessor.GetValue(_testInstance);
    }

    [Benchmark]
    public object ReadFastIntProperty()
    {
        return _fastTypeAccessor[_testInstance, "Id"];
    }
}